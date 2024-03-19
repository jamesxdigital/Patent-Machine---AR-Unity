using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _eclipses;
    
    [SerializeField] private MoveTowards _watermelon;
    [SerializeField] private MoveTowards _mint;
    [SerializeField] private Flash _flash;
    [SerializeField] private SelfRotate _mintHelper;
    [SerializeField] private SelfRotate _watermelonHelper;

    private int _completionCount;

    //public Action OnLastCanvasEnabled;
    private int finished;

    private void Awake()
    {
        _mintHelper.OnFinished += OnFinishedOrbiting;
        _watermelonHelper.OnFinished += OnFinishedOrbiting;
    }
    private void OnFinishedOrbiting()
    {
        finished++;

        if (finished == 2)
        {
            _flash.DoFlash();
        }
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        foreach (var eclipse in _eclipses)
        {
            eclipse.SetActive(false);
        }
    }

    public void RestartScene()
    {
    }

    public void Do()
    {
        StartCoroutine(PopSpheresAfterSeconds());
    }

    private IEnumerator PopSpheresAfterSeconds()
    {
        _watermelon.gameObject.SetActive(true);
        _mint.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        //Show Watermelon & Mentol
        StartCoroutine(ActivateWithDelay());
    }

    private IEnumerator ActivateWithDelay()
    {
        yield return new WaitForSeconds(1f);
        _watermelon.enabled = false;
        _mint.enabled = false;

        _watermelon.OnCompletion += OnCompletion;
        yield return new WaitForSeconds(0.2f);
        _watermelon.enabled = true;
        _mint.enabled = true;
    }
    
    private void OnCompletion()
    {
        if (_completionCount == 0)
        {
            _watermelon.OnCompletion -= OnCompletion;
            foreach (var eclipse in _eclipses)
            {
                eclipse.SetActive(true);
            }
            StartCoroutine(InitiateRotation());
        }
    }

    private IEnumerator InitiateRotation()
    {
        yield return new WaitForSeconds(1f);
        _watermelonHelper.enabled = true;
        _mintHelper.enabled = true;
        
        _watermelonHelper.SetEnabled();
        _mintHelper.SetEnabled();
    }
}
