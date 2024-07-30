using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GraphicSettings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreen;

    private Resolution[] resolutions;

    void Start()
    {
        // Quality Settings Dropdown
        qualityDropdown.ClearOptions();
        List<string> qualityOptions = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(qualityOptions);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // Resolution Settings Dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Load saved settings
        LoadSettings();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualitySettingPreference", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionIndex);
        PlayerPrefs.Save();
    }

        public void SetFullscreen(int fullscreenIndex)
    {
        Resolution resolution = resolutions[fullscreenIndex];
        PlayerPrefs.SetInt("FullscreenPreference", fullscreenIndex);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
        {
            int qualityIndex = PlayerPrefs.GetInt("QualitySettingPreference");
            qualityDropdown.value = qualityIndex;
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionPreference");
            resolutionDropdown.value = resolutionIndex;
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
