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

    public const string QUALITY_KEY = "qualityIndex";
    public const string FULLSCREEN_KEY = "fullscreenBool";
    public const string RESOLUTION_KEY = "resolutionIndex";

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
    HashSet<string> addedOptions = new HashSet<string>();
    int currentResolutionIndex = 0;
    for (int i = 0; i < resolutions.Length; i++)
    {
        float refreshRate = (float)resolutions[i].refreshRateRatio.numerator / resolutions[i].refreshRateRatio.denominator;
        int roundedRefreshRate = Mathf.RoundToInt(refreshRate);

        // Only add resolutions with 60Hz or 120Hz refresh rates
        if (roundedRefreshRate == 60 || roundedRefreshRate == 120)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + roundedRefreshRate + "Hz";

            if (!addedOptions.Contains(option))
            {
                addedOptions.Add(option);
                resolutionOptions.Add(option);

                // Check if this resolution matches the current screen resolution
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height &&
                    resolutions[i].refreshRateRatio.numerator == Screen.currentResolution.refreshRateRatio.numerator &&
                    resolutions[i].refreshRateRatio.denominator == Screen.currentResolution.refreshRateRatio.denominator)
                {
                    currentResolutionIndex = resolutionOptions.Count - 1; // Update index of the currently selected resolution
                }
            }
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
        PlayerPrefs.SetInt(QUALITY_KEY, qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(RESOLUTION_KEY, resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // set fullscreen Setting
        Screen.fullScreen = isFullscreen;

        // convert bool to int
        int fullscreenNumber;
        if (isFullscreen){fullscreenNumber = 1;}
        else {fullscreenNumber = 0;}
        
        PlayerPrefs.SetInt(FULLSCREEN_KEY, fullscreenNumber);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey(QUALITY_KEY))
        {
            int qualityIndex = PlayerPrefs.GetInt(QUALITY_KEY);
            qualityDropdown.value = qualityIndex;
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        if (PlayerPrefs.HasKey(RESOLUTION_KEY))
        {
            int resolutionIndex = PlayerPrefs.GetInt(RESOLUTION_KEY);
            resolutionDropdown.value = resolutionIndex;
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
