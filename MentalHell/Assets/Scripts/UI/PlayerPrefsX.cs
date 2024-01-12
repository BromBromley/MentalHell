using UnityEngine;
using UnityEngine.UI;

/*

    Functions to save Player preferences aka options settings to the system
    can only save int, string, float

*/


public class PlayerPrefsX : MonoBehaviour
{

    public static PlayerPrefsX PlayerPrefsInstance;

    // create singleton of object so it doesnt get destroyed
    private void Awake (){
        if (PlayerPrefsInstance == null){
            PlayerPrefsInstance = this; // set the Audiomanager Instance to this object
            DontDestroyOnLoad(gameObject); // maintain AudioManager Empty through new scene loading
        }
        else {
            Destroy(gameObject);
        }
    }


    // int values

    // Save the players user settings
    public void SetSavePrefsInt(string savedata, int savevalue)
    {
        PlayerPrefs.SetInt(savedata, savevalue);
        PlayerPrefs.Save();
    }
    
    // Get the Player's Preferences
    public int GetPlayerPrefsInt(string savedata)
    {
        int savevalue = PlayerPrefs.GetInt(savedata, 1);
        return savevalue;
    }


    // float values

    // Save the players user settings
    public void SetSavePrefsFloat(string savedata, float savevalue)
    {
        PlayerPrefs.SetFloat(savedata, savevalue);
        PlayerPrefs.Save();
    }
    
    // Get the Player's Preferences
    public float GetPlayerPrefsFloat(string savedata)
    {
        float savevalue = PlayerPrefs.GetFloat(savedata, 1f);
        return savevalue;
    }

}
