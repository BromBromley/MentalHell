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


    // get Sliders
    // private GameObject MasterVolume;
    // private Slider SliderMasterVolume;

    // private GameObject MusicVolume;
    // private GameObject SFXVolume;

    // get the Audio Mixer to adjust the Volume
    public AudioMixer audioMixer;

    // declare resolution variables to get and adjust the resolution for the player
    public TMP_Dropdown resolutionDropdown;
    public Resolution[] resolutions;

    public void Start()
    {

        // get the playerprefs script
        playerPrefs = GameObject.FindWithTag("MasterScriptEmpty");
        playerPrefsScript = playerPrefs.GetComponent<PlayerPrefsX>();  

        // get the slider objects
        // MasterVolume = GameObject.FindWithTag("MasterVolumeSlider");
        // SliderMasterVolume = MasterVolume.GetComponent<Slider>();
        // Debug.Log(SliderMasterVolume.ToString());
    
        // Get possible Resolutions
        GetResolutions();

    }


    /*
      
        Change Scenes

    */
    


    // Start the Next Scene in the List (The Main Game Scene)
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    public void GetResolutions()
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
        //playerPrefsScript.SetSavePrefsInt(RESOLUTION_KEY, resolutionIndex);

        //Debug.Log("Writing: " + resolutionIndex.ToString());

        PlayerPrefs.SetFloat(RESOLUTION_KEY, resolutionIndex);
        PlayerPrefs.Save();
        
    }

    // adjust the Quality of the game
    public void SetQuality(int qualityIndex)
    {

        // set quality setting (dropdown list has to be equal to list of quality settings in editor)
        QualitySettings.SetQualityLevel(qualityIndex);

        // save quality settings
        //playerPrefsScript.SetSavePrefsInt(QUALITY_KEY, qualityIndex);
        PlayerPrefs.SetInt(QUALITY_KEY, qualityIndex);
        PlayerPrefs.Save();


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
        //playerPrefsScript.SetSavePrefsInt(FULLSCREEN_KEY, fullscreenNumber);
        
        PlayerPrefs.SetInt(FULLSCREEN_KEY, fullscreenNumber);
        PlayerPrefs.Save();

    }


    // Volume
    
    // Adjust the Game Volume with a Slider
    public void SetVolumeMaster(float volume)
    {

        // set Master Volume
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        
        // save master volume
        //playerPrefsScript.SetSavePrefsFloat(MASTER_KEY, volume);

        PlayerPrefs.SetFloat(MASTER_KEY, volume);
        PlayerPrefs.Save();

        //SliderMasterVolume.value = 1;

    }
    // Adjust the Music Volume with a Slider
    public void SetVolumeMusic(float volume)
    {

        // set music volume
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        
        // save music volume
        //playerPrefsScript.SetSavePrefsFloat(MUSIC_KEY, volume);

        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
        PlayerPrefs.Save();

    }
    // Adjust the SFX Volume with a Slider
    public void SetVolumeSFX(float volume)
    {

        // set sfx Volume
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);

        // save sfx volume
        //playerPrefsScript.SetSavePrefsFloat(SFX_KEY, volume);

        PlayerPrefs.SetFloat(SFX_KEY, volume);
        PlayerPrefs.Save();        

    }

    // This Method has to get called when the options button is pressed, otherwise the player settings wont get displayed
    public void displayPlayerPrefsUi(){
        GameObject.FindObjectOfType<LoadSettings>().displayPlayerPrefs();
    }
    public void displayPlayerPrefsNoResolution(){
        GameObject.FindObjectOfType<LoadSettings>().displayPlayerPrefsNoResolution();
    }

    // Play Click Sound
    public void PlayButtonClick(){
        Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxMisc;
        GameObject.FindObjectOfType<AudioManager>().PlayOnce("Metal_Click_01", Soundarray);
    }
    
}
