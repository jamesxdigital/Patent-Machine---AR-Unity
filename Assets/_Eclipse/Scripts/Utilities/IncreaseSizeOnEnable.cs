using System.Collections;
using UnityEngine;

public class IncreaseSizeOnEnable : MonoBehaviour
{
    [SerializeField] private float _endsize = 1;
    [SerializeField] private int _lerpDuration = 5;
    private float _initialSize;

    private void OnEnable()
    {
        StartCoroutine(Lerp(0, _endsize));
    }

    private IEnumerator Lerp(float startValue, float endValue)
    {
        float timeElapsed = 0;
        while (timeElapsed < _lerpDuration)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one * endValue;
    }
}
