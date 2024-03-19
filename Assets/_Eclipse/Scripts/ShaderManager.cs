using System.Collections;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    private float _lerpDuration = 2f;

    private static readonly int ShaderDissolve = Shader.PropertyToID("_ShaderDissolve");

    private void Awake()
    {
        Shader.SetGlobalFloat(ShaderDissolve, 1.2f);
    }

    public void Create()
    {
        StartCoroutine(DissolveLerp(1.2f, 0f));
    }
    
    private IEnumerator DissolveLerp(float startValue, float endValue)
    {
        float timeElapsed = 0;
        float shaderValue;
        while (timeElapsed < _lerpDuration)
        {
            shaderValue = Mathf.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            Shader.SetGlobalFloat(ShaderDissolve, shaderValue);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Shader.SetGlobalFloat(ShaderDissolve, endValue);
    }
}
