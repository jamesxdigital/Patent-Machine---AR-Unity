using System.Collections;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject _lastCanvas;
    [SerializeField] private GameObject _preview;
    public void ActivateLastCanvas()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.1f);
        _lastCanvas.SetActive(true);
        _preview.SetActive(false);
        gameObject.SetActive(false);
    }
}
