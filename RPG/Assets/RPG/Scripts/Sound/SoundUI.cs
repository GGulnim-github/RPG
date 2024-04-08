using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundUI : MonoBehaviour
{
    AudioSource _audioSource;

    public void Initialize(AudioMixerGroup audioMixerGroup)
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.outputAudioMixerGroup = audioMixerGroup;
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.reverbZoneMix = 0f;
    }

    public void Play(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
