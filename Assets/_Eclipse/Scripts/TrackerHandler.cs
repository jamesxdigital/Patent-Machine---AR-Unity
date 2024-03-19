using System.Collections.Generic;
using UnityEngine;
using WebARFoundation;

public class TrackerHandler : MonoBehaviour
{
    [SerializeField] private MindARImageTrackingManager _trackingManager;
    [SerializeField] private GameObject[] _onSeenObjects;
    [SerializeField] private GameObject _onSeenAlwaysObjects;
    [SerializeField] private List<SelfRotate> _selfRotates;
    [SerializeField] private StateManager _stateManager;
    [SerializeField] private GameObject _scene;
    [SerializeField] private Transform[] _trackers;
    [SerializeField] private LastCanvasTrigger _lastCanvasTrigger;
    private bool _seenForFirstTime;
    private int _currentTargetIndex = -1;

    private bool _lastStep;

    private void Awake()
    {
        _lastCanvasTrigger.Activated += OnLastCanvasEnabled;
        _trackingManager.onTargetFoundEvent += OnTargetFound;
        _trackingManager.onTargetLostEvent += OnTargetLost;
    }
    private void OnTargetLost(int targetindex)
    {
        OnUnseen();
    }
    private void OnTargetFound(int targetindex)
    {
        if (_currentTargetIndex != targetindex)
        {
            _currentTargetIndex = targetindex;
            _scene.transform.SetParent(_trackers[targetindex], false);
            _scene.SetActive(true);
        }
        OnSeen();
        OnSeenAlways();
    }
    
    
    
    private void OnLastCanvasEnabled()
    {
        _trackingManager.onTargetFoundEvent -= OnTargetFound;
        _trackingManager.onTargetLostEvent -= OnTargetLost;
        _lastStep = true;
        OnSeenAlways();
    }

    private void OnSeen()
    {
        if (_seenForFirstTime)
        {
            return;
        }
        
        for (int i = 0; i < _onSeenObjects.Length; i++)
        {
            _onSeenObjects[i].SetActive(true);
        }
        _seenForFirstTime = true;
        _stateManager.Do();
    }

    private void OnSeenAlways()
    {
        _onSeenAlwaysObjects.SetActive(false);

        if (_lastStep)
        {
            return;
        }
        _scene.SetActive(true);
        foreach (var selfRotate in _selfRotates)
        {
            selfRotate.enabled = true;
        }
    }

    private void OnUnseen()
    {
        _scene.SetActive(false);
        foreach (var selfRotate in _selfRotates)
        {
            selfRotate.enabled = false;
        }
        if (_lastStep)
        {
            return;
        }
        
        _onSeenAlwaysObjects.SetActive(true);
    }
}
