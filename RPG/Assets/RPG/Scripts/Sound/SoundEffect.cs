using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour
{
    AudioSource _audioSource;

    public void Initialize(AudioMixerGroup audioMixerGroup)
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    public void Play(AudioClip clip, Vector3 position)
    {
        transform.position = position;

        _audioSource.clip = clip;
        _audioSource.Play();

        Invoke(nameof(Release), clip.length);
    }

    void Release()
    {
        SoundManager.Instance.ReleaseEffect(this);
    }
}
