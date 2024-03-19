using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("Eclipse Image Tracker");
    }
}
