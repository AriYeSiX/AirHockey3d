using System;
using DeviceInfos;
using Firebase.RemoteConfig;
using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    private const string LINK_KEY = "Link key";
    private const string FIREBASE_URL_KEY = "GetUrl";
    
    private DeviceInfo _deviceInfo = new DeviceInfo();
    private string urlPath;
    
    [SerializeField] private FireBaseService _fireBaseService;

    public event Action<string> OnOpenWebView;
    public event Action OnOpenPlug;
    
    private void Start()
    {
        _fireBaseService ??= FindObjectOfType<FireBaseService>();
        Subscribe();
    }

    private void OnDestroy() =>
        Unsubscribe();

    private void Subscribe() =>
        _fireBaseService.OnFireBaseInit += SetTargetLink;
    
    private void Unsubscribe() =>
        _fireBaseService.OnFireBaseInit -= SetTargetLink;

    private void SetTargetLink()
    {
        urlPath = PlayerPrefs.GetString(LINK_KEY);

        if (string.IsNullOrWhiteSpace(urlPath))
        {
            LoadFireBase();
        }
        else
        {
            OnOpenWebView?.Invoke(urlPath);
        }        
    }

    private void LoadFireBase()
    {
        if (string.IsNullOrWhiteSpace(GetUrl()) ||  _deviceInfo.IsGoogleDevice() || !_deviceInfo.GetSimState())
        {
            OnOpenPlug?.Invoke();
        }
        else
        {
            PlayerPrefs.SetString(LINK_KEY, urlPath);
            OnOpenWebView?.Invoke(urlPath);
        }
    }

    private string GetUrl()
    {
        urlPath = FirebaseRemoteConfig.DefaultInstance.GetValue(FIREBASE_URL_KEY).StringValue;
        return urlPath;
    }
}