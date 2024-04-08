using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloseButton : MonoBehaviour
{
    Button _button;
    UIScreen _screen;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _screen = GetComponentInParent<UIScreen>();

        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        UIManager.Instance.CloseUI(_screen);
    }
}
