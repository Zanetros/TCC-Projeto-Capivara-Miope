using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public float soundEffectVolume;
    public float sountrackVolume;
    public string[] x;

    public AudioMixer aMSFX;
    public AudioMixer aMS;
    public Save_And_Load_Options sALO;

    public Slider sfxSlider;
    public Slider mSlider;

    public AudioSource musicAudioS;
    public AudioSource sfxAudioS;
    public AudioClip jump;
    public AudioClip getBook;
    public AudioClip getWood;
    public AudioClip buildPlat;
    public AudioClip attachRope;
    public AudioClip putBookBack;
    public AudioClip victory;
    public AudioClip defeat;

    public void LoadSoundOptions(float music, float sfx, string[] newX)
    {
        if (newX != null && newX.Length > 0)
        {
            x = newX;
        }
        
        if (sfxSlider != null)
        {
            sfxSlider.value = sfx;
        }
        if (mSlider != null)
        {
            mSlider.value = music;
        }

        if (aMS != null)
        {
            aMS.SetFloat("Master Volume", music);
        }
        if (aMSFX != null)
        {
           aMSFX.SetFloat("Master Volume", sfx);
        }
        SetAllSounds(music, sfx);
    }
    
    public void SetAllSounds(float musicV, float sfxV)
    {
        sountrackVolume = musicV;
        soundEffectVolume = sfxV;
        aMS.SetFloat("Master Volume", musicV);
        aMSFX.SetFloat("Master Volume", sfxV);
    }

    public void SetSFXVolume(float volume)
    {
        soundEffectVolume = volume;
        sALO.sFX = volume;
        if (aMSFX != null)
        {
            aMSFX.SetFloat("Master Volume", volume);
        }
        sALO.UpdateSave();
    }

    public void SetSoundtrackVolume(float volume)
    {
        sountrackVolume = volume;
        sALO.music = volume;
        if (aMS != null)
        {
            aMS.SetFloat("Master Volume", volume);
        }
        sALO.UpdateSave();
    }

    public void PlaySfx(string name)
    {
        if (sfxAudioS != null)
        {
            if (name == getBook.name)
            {
                sfxAudioS.PlayOneShot(getBook);
            }
            else if (name == jump.name)
            {
                sfxAudioS.PlayOneShot(jump);
            }
            else if (name == putBookBack.name)
            {
                sfxAudioS.PlayOneShot(putBookBack);
            }
            else if (name == getWood.name)
            {
                sfxAudioS.PlayOneShot(getWood);
            }
            else if (name == buildPlat.name)
            {
                sfxAudioS.PlayOneShot(buildPlat);
            }
            else if (name == attachRope.name)
            {
                sfxAudioS.PlayOneShot(attachRope);
            }
            else if (name == victory.name)
            {
                sfxAudioS.PlayOneShot(victory);
            }
            else if (name == defeat.name)
            {
                sfxAudioS.PlayOneShot(defeat);
            }
        }
    }
}