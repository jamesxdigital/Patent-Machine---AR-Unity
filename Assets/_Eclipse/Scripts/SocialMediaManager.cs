using MarksAssets.LaunchURLWebGL;
using UnityEngine;

public class SocialMediaManager : MonoBehaviour
{
    [SerializeField] private LaunchURLWebGL _launchURL;
    
    private const string _whatsappiOS = "http://wa.me/584242018496";
    private const string _whatsappAndroid = "https://wa.me/584242018496";

    private const string _iosSpotify = "http://open.spotify.com/user/31auleybbpgjquptc22bhwdfmi2m?si=ae55200939a94c48";
    private const string _androidSpotify = "https://open.spotify.com/user/31auleybbpgjquptc22bhwdfmi2m?si=ae55200939a94c48";
    
    private const string _iosYoutube = "https://www.youtube.com/playlist?list=PLGoxPgglhH5Vw1OZVJxcHXvHBRONr-bk3";
    private const string _androidYoutube = "https://www.youtube.com/playlist?list=PLGoxPgglhH5Vw1OZVJxcHXvHBRONr-bk3";
    
    public void OpenSpotify()
    {
#if UNITY_IPHONE
        _launchURL.launchURLBlank(_iosSpotify);
#else
	    _launchURL.launchURLBlank(_androidSpotify);
#endif
    }

    public void OpenYoutube()
    {
#if UNITY_IPHONE
        _launchURL.launchURLBlank(_iosYoutube);
#else
        _launchURL.launchURLBlank(_androidYoutube);
#endif
    }

    public void OpenWhatsApp()
    {
#if UNITY_IPHONE
        _launchURL.launchURLBlank(_whatsappiOS);
#else
        _launchURL.launchURLBlank(_whatsappAndroid);
#endif
    }
}
