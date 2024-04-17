using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerLevelText : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UIPlayerManager ui = UIPlayerManager.Instance;


        if (ui)
        {
            ui.AddLevelText(this);
        }

        if (PlayerController.Instance != null)
        {
            PlayerStat stat = PlayerController.Instance.Stat;
            SetText(stat.Level);
        }
    }

    private void OnDisable()
    {
        UIPlayerManager ui = UIPlayerManager.Instance;
        if (ui)
        {
            ui.RemoveLevelText(this);
        }
    }

    public void SetText(uint level)
    {
        if (level == DataManager.Instance.MaxLevel)
        {
            _text.text = $"Lv. MAX";
        }
        else
        {
            _text.text = $"Lv. {level}";
        }
    }
}
