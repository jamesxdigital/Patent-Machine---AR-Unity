using System;
using UnityEngine;

public class LastCanvasTrigger : MonoBehaviour
{
	public Action Activated;
	private void OnEnable()
	{
		Activated?.Invoke();
	}

}
