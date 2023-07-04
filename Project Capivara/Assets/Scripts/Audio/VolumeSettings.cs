using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFX_Slider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolumeMaster();
        }

        else
        {
            SetMasterVolume();
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolumeMusic();
        }

        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolumeSFX();
        }

        else
        {
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume)* 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void LoadVolumeMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void LoadVolumeMaster()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");

        SetMasterVolume();
    }

    public void SetSFXVolume()
    {
        float volume = SFX_Slider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolumeSFX()
    {
        SFX_Slider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetSFXVolume();
    }
}
