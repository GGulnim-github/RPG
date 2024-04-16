using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStatSlider : MonoBehaviour
{
    public PlayerStatType stateType;

    public TextMeshProUGUI currentValueText;
    public TextMeshProUGUI maxValueText;

    Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (PlayerController.Instance != null)
        {
            PlayerStat stat = PlayerController.Instance.Stat;
            switch (stateType)
            {
                case PlayerStatType.HP:
                    SetMaxValue(stat.MaxHP);
                    SetValue(stat.CurrentHP);
                    break;
                case PlayerStatType.MP:
                    SetMaxValue(stat.MaxMP);
                    SetValue(stat.CurrentMP);
                    break;
            }
        }
    }

    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
        maxValueText.text = value.ToString();
    }

    public void SetValue(float value)
    {
        _slider.value = value;
        currentValueText.text = value.ToString();
    }
}
