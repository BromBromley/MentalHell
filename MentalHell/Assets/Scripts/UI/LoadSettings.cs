using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class LoadSettings : MonoBehaviour
{   
    // create instance for singleton check and not destroying instance on load
    //public static LoadSettings instance;

    // write player pref keys as constants (yes this is a cheap copy paste idc)
    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string QUALITY_KEY = "qualityIndex";
    public const string FULLSCREEN_KEY = "fullscreenBool";
    public const string RESOLUTION_KEY = "resolutionIndex";

    // get UiMainMenu from ScriptMasterEmpty (put onto the empty because I dont want to come up with a good logic for everything)
    private GameObject mainMenuObject;
    private Ui_MainMenu mainMenu;

    // get playerprefs script to access load funcs
    private PlayerPrefsX playerPrefsScript;

    private int resolution, quality, fullscreenNumber;
    private float master, music, sfx;
    private bool fullscreenBool;

   
    // //Some problem with instancs in combination with the UiMainMenu script no clue why, stuff gets reset when back in main menu for now
    // void Start() {

    //     // load the settings saved in the playerprefs to the actual game
    //     //loadPlayerPrefs();

    // }



    // get all player prefs and write them or defaults if there are none

    // the comma number thing is the default if there is no entry for the setting
    public void loadPlayerPrefs(){

        // get the Script UiMainMenu which includes all the settings
        mainMenuObject = GameObject.FindWithTag("MainMenu");
        mainMenu = mainMenuObject.GetComponent<Ui_MainMenu>();

        // get the playerpref script to get the settings
        playerPrefsScript = this.GetComponent<PlayerPrefsX>();
        
        // Resolution
        resolution = playerPrefsScript.GetPlayerPrefsInt(RESOLUTION_KEY);
        mainMenu.GetResolutions();
        mainMenu.SetResolution(resolution);
        
        // Quality
        quality = playerPrefsScript.GetPlayerPrefsInt(QUALITY_KEY);
        mainMenu.SetQuality(quality);

        // Fullscreen
        int fullscreenNumber = playerPrefsScript.GetPlayerPrefsInt(FULLSCREEN_KEY);
        fullscreenBool = false;
        // convert int to bool
        if (fullscreenNumber == 1) {fullscreenBool = true;}
        mainMenu.SetFullscreen(fullscreenBool);

        // Master Volume
        master = playerPrefsScript.GetPlayerPrefsFloat(MASTER_KEY);
        mainMenu.SetVolumeMaster(master);

        // Music Volume
        music = playerPrefsScript.GetPlayerPrefsFloat(MUSIC_KEY);
        mainMenu.SetVolumeMusic(music);

        // SFX volume
        sfx = playerPrefsScript.GetPlayerPrefsFloat(SFX_KEY);
        mainMenu.SetVolumeSFX(sfx);

                
    }


    public void displayPlayerPrefs(){

        GameObject.FindGameObjectWithTag("MasterVolumeSlider").GetComponent<Slider>().value = master;
        GameObject.FindGameObjectWithTag("SoundtrackVolumeSlider").GetComponent<Slider>().value = music;
        GameObject.FindGameObjectWithTag("SFXVolumeSlider").GetComponent<Slider>().value = sfx;

        GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().value = resolution;
        GameObject.FindGameObjectWithTag("Quality").GetComponent<TMP_Dropdown>().value = quality;
        bool test = GameObject.FindGameObjectWithTag("Fullscreen").GetComponent<Toggle>().isOn;
        if (fullscreenBool != test)
        {
            GameObject.FindGameObjectWithTag("Fullscreen").GetComponent<Toggle>().isOn = fullscreenBool;
        }
        

    }

}
