using UnityEngine;

public class SpecificBillboard : Billboard
{
	void Start()
	{
		_camera = Camera.main.transform;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		transform.forward = _camera.right;
	}
}
