using System;
using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public Action OnFlash;
    [SerializeField] private CanvasGroup _flashback;
    private readonly float _lerpDuration = 0.25f;
    private readonly float _flashDuration = 0.5f;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public void DoFlash()
    {
        _flashback.alpha = 1;
        StartCoroutine(LerpSize(0, 12));
    }

    private void FadeOut()
    {
        StartCoroutine(Lerp(1, 0));
    }

    private IEnumerator Lerp(int startValue, int endValue)
    {
        yield return new WaitForEndOfFrame();
        float timeElapsed = 0;
        while (timeElapsed < _lerpDuration)
        {
            _flashback.alpha = Mathf.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _flashback.alpha = endValue;
    }
    
    private IEnumerator LerpSize(int startValue, int endValue)
    {
        float timeElapsed = 0;
        while (timeElapsed < _flashDuration)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(startValue, endValue, timeElapsed / _flashDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one * endValue;
        OnFlash?.Invoke();

        FadeOut();
    }
}
