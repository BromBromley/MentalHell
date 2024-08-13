using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Add Any Localizations in the Inspector with this Scrpt on an Empty Gameobject

    Note: <br> is a line break like in HTML for TMPro

*/

public class LocalizationManager : MonoBehaviour
{

    public Localization[] LoreTexts;
    public Localization[] TextsEnglish;
    public Localization[] TextsDeutsch;
 
    // make LocalizationManager Accessable from everywhere
    public static LocalizationManager LocalizationManagerInstance;

    // create singleton of object so it doesnt get destroyed
    private void Awake (){
        if (LocalizationManagerInstance == null){
            LocalizationManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

}
