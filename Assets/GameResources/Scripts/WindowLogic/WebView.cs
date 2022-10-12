using UnityEngine;

public class WebView : MonoBehaviour
{
    private const string FIREBASE_URL_KEY = "GetUrl";
    
    private InAppBrowser.DisplayOptions options;
    
    [SerializeField] private InAppBrowserBridge InAppBrowserBridge;
    [SerializeField] private PageSwitcher _pageSwitcher;

    private void Start()
    {
        SetOptions();
        Subscribe();
    }

    private void OnDestroy() => 
        Unsubscribe();
    
    private void Subscribe()
    {
        InAppBrowserBridge.onAndroidBackButtonPressed.AddListener(OnClickBack);
        _pageSwitcher.OnOpenWebView += OpenCustomPage;
    }

    private void Unsubscribe()
    {
        InAppBrowserBridge.onAndroidBackButtonPressed.RemoveListener(OnClickBack);
        _pageSwitcher.OnOpenWebView -= OpenCustomPage;
    }
    
    private void OnClickBack() =>
        InAppBrowser.GoBack();

    public void OnClearCacheClicked() =>
        InAppBrowser.ClearCache();

    private void SetOptions()
    {
        options.displayURLAsPageTitle = false;
        options.hidesTopBar = true;
        options.androidBackButtonCustomBehaviour = true;
    }
    
    private void OpenCustomPage(string url) =>
        InAppBrowser.OpenURL(url, options);
}