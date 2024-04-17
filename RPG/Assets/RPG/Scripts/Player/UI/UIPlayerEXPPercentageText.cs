using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIPlayerEXPPercentageText : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerManager ui = PlayerManager.Instance;
        if (ui)
        {
            ui.AddEXPPercentageText(this);
        }

        if (PlayerController.Instance != null)
        {
            PlayerStat stat = PlayerController.Instance.Stat;
            SetText(stat.EXPPercent);
        }
    }

    private void OnDisable()
    {
        PlayerManager ui = PlayerManager.Instance;
        if (ui)
        {
            ui.RemoveEXPPercentageText(this);
        }
    }

    public void SetText(float value)
    {     
        _text.text = $"EXP {Math.Round(value * 100, 2):F}%";
    }
}
