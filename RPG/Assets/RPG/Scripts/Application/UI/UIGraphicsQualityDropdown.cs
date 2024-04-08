using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TMP_Dropdown))]
public class UIGraphicsQualityDropdown : MonoBehaviour
{
    TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        _dropdown.ClearOptions();

        foreach (ApplicationGraphicsQuality graphicsQuality in Enum.GetValues(typeof(ApplicationGraphicsQuality)))
        {
            string str = string.Empty;
            switch (graphicsQuality)
            {
                case ApplicationGraphicsQuality.Low:
                    str = "낮음";
                    break;
                case ApplicationGraphicsQuality.Medium:
                    str = "중간";
                    break;
                case ApplicationGraphicsQuality.High:
                    str = "높음";
                    break;
            }
            _dropdown.options.Add(new TMP_Dropdown.OptionData(str));
        }

        _dropdown.onValueChanged.AddListener(OnvalueChanged);
    }

    private void OnEnable()
    {
        _dropdown.SetValueWithoutNotify((int)ApplicationManager.Instance.GraphicsQuality);
    }

    void OnvalueChanged(int value)
    {
        ApplicationGraphicsQuality graphicsQuality = (ApplicationGraphicsQuality)value;
        ApplicationManager.Instance.GraphicsQuality = graphicsQuality;
    }
}
