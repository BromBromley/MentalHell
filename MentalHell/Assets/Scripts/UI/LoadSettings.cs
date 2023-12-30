using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{   
    // create instance for singleton check and not destroying instance on load
    public static LoadSettings instance;

    // write player pref keys as constants (yes this is a cheap copy paste idc)
    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string QUALITY_KEY = "qualityIndex";
    public const string FULLSCREEN_KEY = "fullscreenBool";
    public const string RESOLUTION_KEY = "resolutionIndex";

    // get UiMainMenu from ScriptMasterEmpty (put onto the empty because I dont want to come up with a good logic for everything)
    private Ui_MainMenu mainMenu;

    // get playerprefs script to access load funcs
    private PlayerPrefsX playerPrefsScript;

    // prevent object from getting destroyed on scene loading
    void Awake() {
        
        if (instance == null){
            instance = this; // set the Instance to this object
            DontDestroyOnLoad(gameObject); // maintain Empty through new scene loading              
        }
        else {
            Destroy(gameObject);
        }
    }

    //Some problem with instancs in combination with the UiMainMenu script no clue why, stuff gets reset when back in main menu for now

    void Start() {
        mainMenu = this.GetComponent<Ui_MainMenu>();
        playerPrefsScript = this.GetComponent<PlayerPrefsX>();
        loadPlayerPrfs();
    }



    // get all player prefs and write them or defaults if there are none

    // the comma number thing is the default if there is no entry for the setting
    void loadPlayerPrfs(){
        
        // Resolution
        int resolution = playerPrefsScript.GetPlayerPrefsInt(RESOLUTION_KEY);
        mainMenu.Start();
        mainMenu.SetResolution(resolution);
        
        // Quality
        int quality = playerPrefsScript.GetPlayerPrefsInt(QUALITY_KEY);
        mainMenu.SetQuality(quality);

        // Fullscreen
        int fullscreenNumber = playerPrefsScript.GetPlayerPrefsInt(FULLSCREEN_KEY);
        bool fullscreenBool = false;
        // convert int to bool
        if (fullscreenNumber == 1) {fullscreenBool = true;}
        mainMenu.SetFullscreen(fullscreenBool);

        // Master Volume
        float master = playerPrefsScript.GetPlayerPrefsFloat(MASTER_KEY);
        mainMenu.SetVolumeMaster(master);

        // Music Volume
        float music = playerPrefsScript.GetPlayerPrefsFloat(MUSIC_KEY);
        mainMenu.SetVolumeMusic(music);

        // SFX volume
        float sfx = playerPrefsScript.GetPlayerPrefsFloat(SFX_KEY);
        mainMenu.SetVolumeSFX(sfx);

        
    }


}
