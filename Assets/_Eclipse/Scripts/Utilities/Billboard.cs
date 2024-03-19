using UnityEngine;

public class Billboard : MonoBehaviour
{
    protected Transform _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = _camera.forward;
    }
}
