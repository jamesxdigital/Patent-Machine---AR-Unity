using System.Collections;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private float _angle;
    [SerializeField] private float _radius;
    [SerializeField] private bool _up;
    [SerializeField] private Vector3 _axis;

    private float _lastRadius;
    private int _lerpDuration = 6;

    [SerializeField]
    private bool _paralized;

    private void Awake()
    {
        if (_up)
        {
            _axis = Vector3.up;
        }
    }

    private void OnEnable()
    {
        float endValue = _angle * 15;
        //StartCoroutine(LerpSpeed(_angle, endValue));
    }

    // Update is called once per frame
    void Update()
    {
        if (_paralized)
        {
            return;
        }
        transform.RotateAround(_target.position, _axis, _angle * Time.deltaTime);
        //transform.Rotate(Vector3.right, _angle * 2 * Time.deltaTime);
    }
    //
    // private void ApplyRadius()
    // {
    //     // Get the direction vector from the pivot to your current object position
    //     var direction = (transform.position - _target.transform.position).normalized;
    //
    //     // set your object in the same direction but at distance of radius
    //     transform.position = _target.position + direction * _radius;
    //
    //     _lastRadius = _radius;
    // }
    
    private IEnumerator LerpSpeed(float startValue, float endValue)
    {
        float timeElapsed = 0;
        while (timeElapsed < _lerpDuration)
        {
            _angle = Mathf.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _angle = endValue;
    }
}
