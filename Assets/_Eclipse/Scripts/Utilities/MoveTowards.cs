using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public Action OnCompletion;
    [SerializeField] private List<Billboard> _onCompleteActivate;
    
    [SerializeField] private List<Transform> _waypoints;
    private Vector3[] _waypointPositions;

    [SerializeField] private IncreaseSize _increaseSize;

    [SerializeField] private Transform _onCompletionParent;
    private bool _onEnableOnce;

    private IEnumerator _coroutine;

    private void Awake()
    {
        _waypointPositions = new Vector3[_waypoints.Count];
        for (int i = 0; i < _waypoints.Count; i++)
        {
            Transform waypoint = _waypoints[i];
            _waypointPositions[i] = waypoint.localPosition;
        }
        
        foreach (var billboard in _onCompleteActivate)
        {
            billboard.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (_onEnableOnce)
        {
            if (_coroutine != null)
            {
                StartCoroutine(_coroutine);
                _increaseSize.ChangeSize();
            }
        }
        
        //First time on Enable
        if (!_onEnableOnce)
        {
            _coroutine = Lerp(0);
            StartCoroutine(_coroutine);
            _increaseSize.ChangeSize();
        }
        _onEnableOnce = true;
    }

    private void OnDisable()
    {
        transform.localPosition = _waypointPositions[1];
    }

    private IEnumerator OnCompletionRoutine()
    {
        foreach (var billboard in _onCompleteActivate)
        {
            billboard.enabled = true;
        }
        
        yield return new WaitForSeconds(2);
        
        OnCompletion?.Invoke();
        _coroutine = null;

        if (_onCompletionParent != null)
        {
            transform.SetParent(_onCompletionParent, true);
        }
    }
    
    IEnumerator Lerp(int index, bool next = true)
    {
        float timeElapsed = 0;
        float lerpDuration = 1.5f;
        var pos = transform.localPosition;
        while (timeElapsed < lerpDuration)
        {
            transform.localPosition = Vector3.Lerp(pos, _waypointPositions[index], timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = _waypointPositions[index];

        if (next)
        {
            _coroutine = Lerp(1, false);
            StartCoroutine(_coroutine);
        }
        else
        {
            _coroutine = OnCompletionRoutine();
            StartCoroutine(_coroutine);
        }
    }
}
