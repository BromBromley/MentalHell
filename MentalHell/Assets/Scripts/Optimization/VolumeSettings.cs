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
        if (PlayerPrefs.HasKey(MUSIC_KEY) && PlayerPrefs.HasKey(SFX_KEY) && PlayerPrefs.HasKey(MASTER_KEY))
        {
            LoadVolume();
        }
        else
        {
            // Set default values to 50% (0.5)
            MasterSlider.value = 0.5f;
            MusicSlider.value = 0.5f;
            SFXSlider.value = 0.5f;

            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        myMixer.SetFloat(MASTER_KEY, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MASTER_KEY, volume);
    }

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        myMixer.SetFloat(MUSIC_KEY, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat(SFX_KEY, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }

    private void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat(MASTER_KEY);
        MusicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY);
        SFXSlider.value = PlayerPrefs.GetFloat(SFX_KEY);

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
