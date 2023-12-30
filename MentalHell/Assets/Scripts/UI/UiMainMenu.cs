using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


/*
    Changing Scenes and Ui Menues Functionality
*/


public class Ui_MainMenu : MonoBehaviour
{

    // write player pref keys as constants
    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string QUALITY_KEY = "qualityIndex";
    public const string FULLSCREEN_KEY = "fullscreenBool";
    public const string RESOLUTION_KEY = "resolutionIndex";

    // get playerprefs script to access save functions
    private GameObject playerPrefs;
    private PlayerPrefsX playerPrefsScript;


    // get the Audio Mixer to adjust the Volume
    public AudioMixer audioMixer;

    // declare resolution variables to get and adjust the resolution for the player
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;


    void Awake() {
        playerPrefs = GameObject.FindWithTag("MasterScriptEmpty");
        playerPrefsScript = playerPrefs.GetComponent<PlayerPrefsX>();  
    }


    /*
      
        Change Scenes

    */
    


    // Start the Next Scene in the List (The Main Game Scene)
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Start the Main Menu Scene
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }


    /*

        Set and Save Options

    */


    // Graphics

    // Get possible Resolutions
    public void Start()
    {

        resolutions = Screen.resolutions;

        // Clear all Options to be Save
        resolutionDropdown.ClearOptions();

        // make list out of possible Resolutions (convert array to string)
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;


        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add list to dropdown Ui elemnt
        resolutionDropdown.AddOptions(options);

        // set Resolution to current
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
    // Actually Change the Screens Resolution now getting called by the dropdown itself
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        // save newly set resolution
        // prob not directly save it but rather have a prompt to save it when confirmed again but meh
        playerPrefsScript.SetSavePrefsInt(RESOLUTION_KEY, resolutionIndex);
    }

    // adjust the Quality of the game
    public void SetQuality(int qualityIndex)
    {

        // set quality setting (dropdown list has to be equal to list of quality settings in editor)
        QualitySettings.SetQualityLevel(qualityIndex);

        // save quality settings
        playerPrefsScript.SetSavePrefsInt(QUALITY_KEY, qualityIndex);

    }

    // set the game to fullscren or windowed
    public void SetFullscreen (bool isFullscreen)
    {

        // set fullscreen
        Screen.fullScreen = isFullscreen;

        // convert bool to int
        int fullscreenNumber;
        if (isFullscreen == true)
        {
            fullscreenNumber = 1;
        }
        else 
        {
            fullscreenNumber = 0;
        }

        // save fullscreen setting
        playerPrefsScript.SetSavePrefsInt(FULLSCREEN_KEY, fullscreenNumber);

    }


    // Volume
    
    // Adjust the Game Volume with a Slider
    public void SetVolumeMaster(float volume)
    {

        // set Master Volume
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        
        // save master volume
        playerPrefsScript.SetSavePrefsFloat(MASTER_KEY, volume);

    }
    // Adjust the Music Volume with a Slider
    public void SetVolumeMusic(float volume)
    {

        // set music volume
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        
        // save music volume
        playerPrefsScript.SetSavePrefsFloat(MUSIC_KEY, volume);

    }
    // Adjust the SFX Volume with a Slider
    public void SetVolumeSFX(float volume)
    {

        // set sfx Volume
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);

        // save sfx volume
        playerPrefsScript.SetSavePrefsFloat(SFX_KEY, volume);

    }
    
}
