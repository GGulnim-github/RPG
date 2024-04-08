using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class SoundManager : Manager<SoundManager>
{
    float _masterVloume;
    public float MasterVolume
    {
        get { return _masterVloume; }
        set
        {
            SetMasterVolume(value);
        }
    }

    float _bgmVolume;
    public float BGMVloume
    {
        get { return _bgmVolume; }
        set
        {
            SetBGMVolume(value);
        }
    }

    float _sfxVolume;
    public float SFXVolume
    {
        get { return _sfxVolume; }
        set
        {
            SetSFXVolume(value);
        }
    }

    AudioMixer _audioMixer;
    
    AudioMixerGroup _bgmAudioMixerGroup;
    AudioMixerGroup _sfxAudioMixerGroup;

    SoundBGM _soundBGM;
    SoundUI _soundUI;

    IObjectPool<SoundEffect> _effectPool;

    public override void Initialize()
    {
        if (_audioMixer == null)
        {
            _audioMixer = Resources.Load<AudioMixer>("Sound/AudioMixer/AudioMixer");
        }

        _bgmAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/BGM")[0];
        _sfxAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/SFX")[0];

        GameObject bgmGameObject = new GameObject("Sound BGM");
        bgmGameObject.transform.SetParent(transform);
        _soundBGM = bgmGameObject.AddComponent<SoundBGM>();
        _soundBGM.Initialize(_bgmAudioMixerGroup);

        GameObject uiGameObject = new GameObject("Sound UI");
        uiGameObject.transform.SetParent(transform);
        _soundUI = uiGameObject.AddComponent<SoundUI>();
        _soundUI.Initialize(_sfxAudioMixerGroup);

        _effectPool = new ObjectPool<SoundEffect>(CreateEffect, OnGetEffect, OnReleaseEffect, OnClearEffect);

        MasterVolume = 1f;
        BGMVloume = 1f;
        SFXVolume = 1f;
    }

    #region Play&Stop
    public void PlayBGM(string clipName)
    {
        _soundBGM.Play(GetClip(SoundType.BGM, clipName));
    }

    public void StopBGM()
    {
        _soundBGM.Stop();
    }

    public void PlayUI(string clipName)
    {
        _soundUI.Play(GetClip(SoundType.UI, clipName));
    }

    public void PlayEffect(string clipName, Vector3 position)
    {
        SoundEffect soundEffect = _effectPool.Get();
        soundEffect.Play(GetClip(SoundType.Effect, clipName), position);
    }

    public void ReleaseEffect(SoundEffect effect)
    {
        _effectPool.Release(effect);
    }
    #endregion

    #region Setting
    void SetMasterVolume(float value)
    {
        _masterVloume = value;
        _audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }

    void SetBGMVolume(float value)
    {
        _bgmVolume = value;
        _audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        _sfxVolume = value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }
    #endregion

    #region SFX Pool
    SoundEffect CreateEffect()
    {
        GameObject gameObject = new("Sound Effect");
        SoundEffect sfx = gameObject.AddComponent<SoundEffect>();
        sfx.Initialize(_sfxAudioMixerGroup);
        return sfx;
    }

    void OnGetEffect(SoundEffect sfx)
    {
        sfx.gameObject.SetActive(true);
    }

    void OnReleaseEffect(SoundEffect sfx)
    {
        sfx.gameObject.SetActive(false);
    }

    void OnClearEffect(SoundEffect sfx)
    {
        Destroy(sfx.gameObject);
    }
    #endregion

    #region Common
    AudioClip GetClip(SoundType type, string clipName)
    {
        return Resources.Load<AudioClip>($"Sound/{type}/{clipName}"); 
    }
    #endregion
}