using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public void Awake()
    {
        instance = this;
    }

    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeToSwitch;

    [SerializeField] AudioClip playOnStart;

    public void Start()
    {
        Play(playOnStart, true);
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if (musicToPlay == null) { return; }
        
        if (interrupt == true)
        {
            audioSource.volume = 1f;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }

        else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothMusic());
        }
        
    }

    AudioClip switchTo;
    float volume;
    IEnumerator SmoothMusic()
    {
        volume = 1f;
        while(volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if (volume <0f)
            {
                volume = 0f;
            }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }

    public void Pause_UnpauseMusic(bool state)
    {
        if (state)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
    
}
