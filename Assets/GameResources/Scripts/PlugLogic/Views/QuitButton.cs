using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Close game button
/// </summary>
[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        Subscribe();
    }

    private void OnDestroy() =>
        Unsubscribe();

    private void Subscribe() =>
        _button.onClick.AddListener(Quit);

    private void Unsubscribe() =>
        _button.onClick.RemoveListener(Quit);

    private void Quit() =>
        Application.Quit();
}
