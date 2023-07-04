using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixerGroup SFX_mixer;
    [SerializeField] private AudioSource audioSourceSFX;

    [Header("AudioClips")]
    public AudioClip arar;
    public AudioClip dialogo;
    public AudioClip cortar;


    public void Awake()
    {
        instance = this;
    }


    public void Start()
    {

    }

    public void Play(AudioClip audioClip)
    {
        audioSourceSFX.PlayOneShot(audioClip);
    }

    public void Stop(AudioClip audioClip)
    {
        audioSourceSFX.Stop();
    }
}
