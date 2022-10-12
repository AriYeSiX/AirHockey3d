using UnityEngine;
using UnityEngine.Serialization;

public class PageSwitcherView : MonoBehaviour
{
    [SerializeField] private PageSwitcher _pageSwitcher;
    [SerializeField] private GameObject loaderWindow;
    [SerializeField] private GameObject plugWindow;
    [SerializeField] private GameObject plugLogic;
    
    private void Start() =>
        Subscribe();

    private void OnDestroy() =>
        Unsubscribe();

    private void Subscribe() =>
        _pageSwitcher.OnOpenPlug += OpenPlugEvent;

    private void Unsubscribe() =>
        _pageSwitcher.OnOpenPlug -= OpenPlugEvent;

    private void OpenPlugEvent()
    {
        loaderWindow.SetActive(false);
        plugWindow.SetActive(true);
        plugLogic.SetActive(true);
    }
}