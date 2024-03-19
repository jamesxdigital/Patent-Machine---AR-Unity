using System.Collections.Generic;
using UnityEngine;

public class LastCanvasController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _onExplosionDeactivateObjs;
    [SerializeField] private List<GameObject> _onExplosionActivateObjs;
    [SerializeField] private Flash _flash;

    private void Awake()
    {
        _flash.OnFlash += DisableAll;
        _flash.gameObject.SetActive(true);
    }

    private void DisableAll()
    {
        foreach (var gameObj in _onExplosionDeactivateObjs)
        {
            gameObj.SetActive(false);
        }
        
        Activate();
    }
    
    private void Activate()
    {
        foreach (var gameObj in _onExplosionActivateObjs)
        {
            gameObj.SetActive(true);
        }
    }
}
