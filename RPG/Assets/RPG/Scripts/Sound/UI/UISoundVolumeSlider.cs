using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISoundVolumeSlider : MonoBehaviour
{
    public SoundVolume soundType;
    public TextMeshProUGUI valueText;

    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = 0.001f;
        _slider.maxValue = 1;
        //_slider.maxValue = SoundManager.Instance.MAX_VALUE;
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnEnable()
    {
        float volume = 1;
        switch (soundType)
        {
            case SoundVolume.Master:
                volume = SoundManager.Instance.MasterVolume;
                break;
            case SoundVolume.BGM:
                volume = SoundManager.Instance.BGMVloume;
                break;
            case SoundVolume.SFX:
                volume = SoundManager.Instance.SFXVolume;
                break;
        }
        _slider.SetValueWithoutNotify(volume);

        if (valueText != null)
        {
            valueText.text = GetString(volume).ToString();
        }
    }

    void OnValueChanged(float value)
    {
        switch (soundType)
        {
            case SoundVolume.Master:
                SoundManager.Instance.MasterVolume = value;
                break;
            case SoundVolume.BGM:
                SoundManager.Instance.BGMVloume = value;
                break;
            case SoundVolume.SFX:
                SoundManager.Instance.SFXVolume = value;
                break;
        }

        if (valueText != null)
        {
            valueText.text = GetString(value).ToString();
        }
    }

    int GetString(float volume)
    {
        return (int)(volume * 100);
    }

}
