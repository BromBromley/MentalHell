using UnityEngine;
using UnityEngine.UI;

/*

    Functions to save Player preferences aka options settings to the system
    can only save int, string, float

*/


public class PlayerPrefsX : MonoBehaviour
{

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
