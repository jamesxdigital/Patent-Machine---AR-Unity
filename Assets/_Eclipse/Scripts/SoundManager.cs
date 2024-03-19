using System.Collections;
using UnityEngine;
using WebARFoundation;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private GameObject _splash;
	[SerializeField] private GameObject _tracking;
	[SerializeField] private MindARImageTrackingManager _ar;
	[SerializeField] private AudioSource _audioSource;
	private string _deviceName;
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		
		_ar.OnARActivated += OnARActivated;
	}
	private void OnARActivated()
	{
		_audioSource.enabled = true;
	}

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(1.5f);
		_splash.SetActive(false);
		_tracking.SetActive(true);
	}
}