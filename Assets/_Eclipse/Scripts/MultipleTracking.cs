using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Zappar
{
    public class MultipleTracking : ZapparTrackingTarget, ICameraListener
    {
        public enum PlaneOrientation
        {
            Flat,
            Vertical
        }

        public IntPtr? ImageTrackerPtr { get; private set; } = null;

        private bool m_hasInitialized = false;
        private bool m_isMirrored = false;
        private bool m_isPaused = false;

        public string[] Target;

        [SerializeField]
        public PlaneOrientation Orientation = PlaneOrientation.Flat;

        [HideInInspector]
        public GameObject PreviewImageObject = null;

        public UnityEvent OnSeenEvent;
        public UnityEvent OnNotSeenEvent;
        private bool m_isVisible = false;
        private const int TrackIndx = 0;
        private bool _finallyFound;

        private void Start()
        {
            if (ZapparCamera.Instance != null)
                ZapparCamera.Instance.RegisterCameraListener(this, true);

            if (ZapparCamera.Instance.CameraSourceInitialized && !m_hasInitialized)
            {
                OnMirroringUpdate(ZapparCamera.Instance.MirrorCamera);
                OnZapparCameraPaused(ZapparCamera.Instance.CameraSourcePaused);
                OnZapparInitialized(ZapparCamera.Instance.GetPipeline);
            }
        }

        public void OnZapparInitialized(IntPtr pipeline)
        {
            if (!gameObject.activeInHierarchy)
            {
                Debug.Log("Could not start LoadZPTTarget Coroutine as gameobject is inactive.");
                return;
            }
            m_hasInitialized = true;
            ImageTrackerPtr = Z.ImageTrackerCreate(pipeline);

            StartCoroutine(RoundRobin());
            // string filename = Target[0];
            // StartCoroutine(Z.LoadZPTTarget(filename, TargetDataAvailableCallback));
        }

        public void OnZapparCameraPaused(bool pause) { m_isPaused = pause; }

        public void OnMirroringUpdate(bool mirrored)
        {
            m_isMirrored = mirrored;
        }

        void UpdateTargetPose()
        {
            Matrix4x4 cameraPose = ZapparCamera.Instance.CameraPose;
            Matrix4x4 imagePose = Z.ImageTrackerAnchorPose(ImageTrackerPtr.Value, TrackIndx, cameraPose, m_isMirrored);
            Matrix4x4 targetPose = Z.ConvertToUnityPose(imagePose);
            transform.localPosition = Z.GetPosition(targetPose);

            // Offset rotations based on dropdown provided by inspector properties
            Quaternion rotation = Orientation == PlaneOrientation.Flat ? Z.GetRotation(targetPose) * Quaternion.Euler(Vector3.left * 90) : Z.GetRotation(targetPose);
            transform.localRotation = rotation;

            transform.localScale = Z.GetScale(targetPose);
        }

        private void Update()
        {
            if (!m_hasInitialized || ImageTrackerPtr == null || m_isPaused)
            {
                return;
            }

            if (Z.ImageTrackerAnchorCount(ImageTrackerPtr.Value) > TrackIndx)
            {
                if (!m_isVisible)
                {
                    m_isVisible = true;
                    OnSeenEvent?.Invoke();
                    _finallyFound = true;
                    StopAllCoroutines();
                }
                UpdateTargetPose();
            }
            else
            {
                if (m_isVisible)
                {
                    m_isVisible = false;
                    OnNotSeenEvent?.Invoke();
                }
            }
        }

        private IEnumerator RoundRobin()
        {
            int i = 0;
            while (!_finallyFound)
            {
                yield return new WaitForSeconds(1.5f);
                if (_finallyFound)
                {
                    break;
                }
                string filename = Target[i];
                StartCoroutine(Z.LoadZPTTarget(filename, TargetDataAvailableCallback));
                i++;
                if (i > Target.Length - 1)
                {
                    i = 0;
                }
            }
        }

        private void TargetDataAvailableCallback(byte[] data)
        {
            Z.ImageTrackerTargetLoadFromMemory(ImageTrackerPtr.Value, data);
        }

        private void OnDestroy()
        {
            if (m_hasInitialized)
            {
                if (ImageTrackerPtr != null)
                {
                    Z.ImageTrackerDestroy(ImageTrackerPtr.Value);
                    ImageTrackerPtr = null;
                }
            }
            if (ZapparCamera.Instance != null)
                ZapparCamera.Instance.RegisterCameraListener(this, false);
        }

        public override Matrix4x4 AnchorPoseCameraRelative()
        {
            if (Z.ImageTrackerAnchorCount(ImageTrackerPtr.Value) > TrackIndx)
            {
                return Z.ImageTrackerAnchorPoseCameraRelative(ImageTrackerPtr.Value, TrackIndx, m_isMirrored);
            }
            return Matrix4x4.identity;
        }

    }
}