using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    public Dictionary<PlayerStatType, List<UIPlayerStatSlider>> statSlider;
    
    public List<UIPlayerLevelText> levelTexts;
    
    public UIPlayerEXPSlider expSlider;
    public List<UIPlayerEXPPercentageText> expPercentageText;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public override void Initialize()
    {
        statSlider = new();
        foreach (PlayerStatType type in Enum.GetValues(typeof(PlayerStatType)))
        {
            statSlider.Add(type, new List<UIPlayerStatSlider>());
        }

        levelTexts = new();
        expPercentageText = new();
    }

    #region StatSlider
    public void AddStatSlider(PlayerStatType type, UIPlayerStatSlider slider)
    {
        List<UIPlayerStatSlider> list = statSlider[type];
        if (list.Contains(slider) == false)
        {
            list.Add(slider);
        }
    }

    public void RemoveStatSlider(PlayerStatType type, UIPlayerStatSlider slider)
    {
        List<UIPlayerStatSlider> list = statSlider[type];
        if (list.Contains(slider) == true)
        {
            list.Remove(slider);
        }
    }

    public void UpdateMaxStat(PlayerStatType type, float value)
    {
        List<UIPlayerStatSlider> list = statSlider[type];
        foreach (var slider in list)
        {
            slider.SetMaxValue(value);
        }
    }

    public void UpdateCurrentStat(PlayerStatType type, float value)
    {
        List<UIPlayerStatSlider> list = statSlider[type];
        foreach (var slider in list)
        {
            slider.SetValue(value);
        }
    }
    #endregion

    #region LevelText
    public void AddLevelText(UIPlayerLevelText text)
    {
        if (levelTexts.Contains(text) == false)
        {
            levelTexts.Add(text);
        }
    }

    public void RemoveLevelText(UIPlayerLevelText text)
    {
        if (levelTexts.Contains(text) == true)
        {
            levelTexts.Remove(text);
        }
    }

    public void UpdateLevelText(uint level)
    {
        foreach (var text in levelTexts)
        {
            text.SetText(level);
        }
    }
    #endregion

    #region ExpSlider
    public void AddExpSlider(UIPlayerEXPSlider slider)
    {
        expSlider = slider;
    }

    public void RemoveEXPSlier()
    {
        expSlider = null;
    }

    public void UpdateMaxEXP(float value)
    {
        if (expSlider != null)
        {
            expSlider.SetMaxValue(value);
        }
    }

    public void UpdateCurrentEXP(float value)
    {
        if (expSlider != null)
        {
            expSlider.SetValue(value);
        }
    }
    #endregion

    #region EXPPercentText
    public void AddEXPPercentageText(UIPlayerEXPPercentageText text)
    {
        if (expPercentageText.Contains(text) == false)
        {
            expPercentageText.Add(text);
        }
    }

    public void RemoveEXPPercentageText(UIPlayerEXPPercentageText text)
    {
        if (expPercentageText.Contains(text) == true)
        {
            expPercentageText.Remove(text);
        }
    }

    public void UpdateEXPPercentageText(float value)
    {
        foreach (var text in expPercentageText)
        {
            text.SetText(value);
        }
    }
    #endregion
}
