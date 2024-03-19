using System;
using System.Collections;
using UnityEngine;

public class IncreaseSize : MonoBehaviour
{
    [SerializeField] private float _endsize = 0;
    [SerializeField] private int _lerpDuration = 5;
    private float _initialSize;

    [SerializeField] private bool _excludeZ;
    
    private void Awake()
    {
        _initialSize = transform.localScale.x;
    }

    public void ChangeSize()
    {
        if (Math.Abs(transform.localScale.x - _endsize) < 0.01f)
        {
            return;
        }
        
        if (!_excludeZ)
        {
            StartCoroutine(Lerp(transform.localScale.x, _endsize));
        }
        else
        {
            StartCoroutine(LerpExcludingZ(1, _endsize));
        }
    }

    private IEnumerator Lerp(float startValue, float endValue)
    {
        yield return new WaitForSeconds(1);
        float timeElapsed = 0;
        while (timeElapsed < _lerpDuration)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one * endValue;
    }
    
    private IEnumerator LerpExcludingZ(float startValue, float endValue)
    {
        yield return new WaitForSeconds(4);
        float timeElapsed = 0;
        while (timeElapsed < _lerpDuration)
        {
            var scale = Vector3.one * Mathf.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            scale.z = 1;
            transform.localScale = scale;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one * endValue;
    }
}
