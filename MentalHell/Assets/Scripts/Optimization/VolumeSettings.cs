using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            SetMasterVolume();
        }
    }

    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        myMixer.SetFloat(MASTER_KEY, Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat(MASTER_KEY, volume);
    }

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        myMixer.SetFloat(MUSIC_KEY, Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat(SFX_KEY, Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }

    private void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY);
        SFXSlider.value = PlayerPrefs.GetFloat(SFX_KEY);
        MasterSlider.value = PlayerPrefs.GetFloat(MASTER_KEY);

        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }
}
