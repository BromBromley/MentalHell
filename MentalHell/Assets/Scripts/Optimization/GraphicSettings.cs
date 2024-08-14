using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GraphicSettings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown languageDropdown;
    public Toggle fullScreen;
    public Button leftArrowQuality;
    public Button rightArrowQuality;
    public Button leftArrowResolution;
    public Button rightArrowResolution;
    public Button leftArrowLanguage;
    public Button rightArrowLanguage;

    private Resolution[] resolutions;
    private int currentQualityIndex;
    private int currentResolutionIndex;
    private int currentLanguageIndex;

    public const string QUALITY_KEY = "qualityIndex";
    public const string FULLSCREEN_KEY = "fullscreenBool";
    public const string RESOLUTION_KEY = "resolutionIndex";
    public const string LANGUAGE_KEY = "languageIndex";

    private List<string> languages = new List<string> { "English", "German" };

    void Start()
    {
        // Quality Settings Dropdown
        qualityDropdown.ClearOptions();
        List<string> qualityOptions = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(qualityOptions);

        currentQualityIndex = PlayerPrefs.GetInt(QUALITY_KEY, QualitySettings.GetQualityLevel());
        qualityDropdown.value = currentQualityIndex;
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        leftArrowQuality.onClick.AddListener(PreviousQualityOption);
        rightArrowQuality.onClick.AddListener(NextQualityOption);

        // Resolution Settings Dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        foreach (Resolution resolution in resolutions)
        {
            resolutionOptions.Add(resolution.width + " x " + resolution.height);
        }
        resolutionDropdown.AddOptions(resolutionOptions);

        currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_KEY, resolutions.Length - 1);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        leftArrowResolution.onClick.AddListener(PreviousResolutionOption);
        rightArrowResolution.onClick.AddListener(NextResolutionOption);

        // Language Settings Dropdown
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(languages);

        currentLanguageIndex = PlayerPrefs.GetInt(LANGUAGE_KEY, 0);
        languageDropdown.value = currentLanguageIndex;
        languageDropdown.onValueChanged.AddListener(SetLanguage);

        leftArrowLanguage.onClick.AddListener(PreviousLanguageOption);
        rightArrowLanguage.onClick.AddListener(NextLanguageOption);

        // Fullscreen Toggle
        fullScreen.isOn = PlayerPrefs.GetInt(FULLSCREEN_KEY, Screen.fullScreen ? 1 : 0) == 1;
        fullScreen.onValueChanged.AddListener(SetFullScreen);
    }

    // Quality Settings Management
    void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(QUALITY_KEY, qualityIndex);
    }

    void PreviousQualityOption()
    {
        currentQualityIndex = (currentQualityIndex - 1 + qualityDropdown.options.Count) % qualityDropdown.options.Count;
        UpdateQualityDropdown();
    }

    void NextQualityOption()
    {
        currentQualityIndex = (currentQualityIndex + 1) % qualityDropdown.options.Count;
        UpdateQualityDropdown();
    }

    void UpdateQualityDropdown()
    {
        qualityDropdown.value = currentQualityIndex;
        SetQuality(currentQualityIndex);
    }

    // Resolution Settings Management
    void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(RESOLUTION_KEY, resolutionIndex);
    }

    void PreviousResolutionOption()
    {
        currentResolutionIndex = (currentResolutionIndex - 1 + resolutionDropdown.options.Count) % resolutionDropdown.options.Count;
        UpdateResolutionDropdown();
    }

    void NextResolutionOption()
    {
        currentResolutionIndex = (currentResolutionIndex + 1) % resolutionDropdown.options.Count;
        UpdateResolutionDropdown();
    }

    void UpdateResolutionDropdown()
    {
        resolutionDropdown.value = currentResolutionIndex;
        SetResolution(currentResolutionIndex);
    }

    // Language Settings Management
    void SetLanguage(int languageIndex)
    {
        PlayerPrefs.SetInt(LANGUAGE_KEY, languageIndex);
    }

    void PreviousLanguageOption()
    {
        currentLanguageIndex = (currentLanguageIndex - 1 + languageDropdown.options.Count) % languageDropdown.options.Count;
        UpdateLanguageDropdown();
    }

    void NextLanguageOption()
    {
        currentLanguageIndex = (currentLanguageIndex + 1) % languageDropdown.options.Count;
        UpdateLanguageDropdown();
    }

    void UpdateLanguageDropdown()
    {
        languageDropdown.value = currentLanguageIndex;
        SetLanguage(currentLanguageIndex);
    }

    // Fullscreen Toggle Management
    void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullScreen ? 1 : 0);
    }
}
