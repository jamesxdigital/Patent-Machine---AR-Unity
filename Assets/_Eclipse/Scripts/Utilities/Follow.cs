using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    void LateUpdate()
    {
        transform.position = _target.position;
    }
}
