using System;
using Unity.VisualScripting;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _stoppingAngle;
    [SerializeField] private float _aroundAngle;

    private bool _enabled;
    private bool _passedAlready;
    private bool _aroundIt;
    private float _tolerance = 0.03f;
    public Action OnFinished;

    private void Awake()
    {
#if UNITY_EDITOR
        _rotationSpeed = _rotationSpeed / 5;
#endif
    }
    
    private void OnEnable()
    {
        SetEnabled();
    }

    public void SetEnabled()
    {
        _enabled = true;
    }
    
    private void Update()
    {
        if (!_enabled)
        {
            return;
        }
        
        transform.Rotate(0, 0, _rotationSpeed, Space.Self);

        if (Math.Abs(transform.localRotation.z - _stoppingAngle) < _tolerance)
        {
            if (_aroundIt)
            {
                OnFinished?.Invoke();
                enabled = false;
            }
        }
        
        if (transform.localRotation.z > _aroundAngle)
        {
            _aroundIt = true;
        }
    }
    
    private void OnDisable()
    {
        Debug.Log("Z: " + transform.localRotation.z);
    }
}