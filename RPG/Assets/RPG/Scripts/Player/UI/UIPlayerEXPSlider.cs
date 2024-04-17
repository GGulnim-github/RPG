using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerEXPSlider : MonoBehaviour
{
    Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        PlayerManager ui = PlayerManager.Instance;
        if (ui)
        {
            ui.AddExpSlider(this);
        }

        if (PlayerController.Instance != null)
        {
            PlayerStat stat = PlayerController.Instance.Stat;
            SetMaxValue(stat.MaxEXP);
            SetValue(stat.CurrentEXP);
        }
    }

    private void OnDisable()
    {
        PlayerManager ui = PlayerManager.Instance;
        if (ui)
        {
            ui.RemoveEXPSlier();
        }
    }

    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }
}
