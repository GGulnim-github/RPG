using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonsterHUD : HUD
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nameText;
    public Transform damageTransform;
    public Slider hpSlider;

    public void Initialize(uint level, string name, uint hp)
    {
        levelText.text = $"Lv. {level}";
        nameText.text = name;
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }
}
