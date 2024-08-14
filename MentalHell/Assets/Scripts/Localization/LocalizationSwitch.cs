using UnityEngine;
using TMPro;
using System.Collections.Generic;

/*
    Change Localization Language depending on the Language chosen
*/

public class LocalizationSwitch : MonoBehaviour
{
    
    // string array with a list of languages for the load localization script
    private string[] languages = {
        "English",
        "Deutsch",
    };

    public void localizationSwitch(int languageIndex)
    {
        // get correct string and call loadLocalization
        string language = languages[languageIndex];
        AddLocalization[] addLocalizationScripts = FindObjectsOfType<AddLocalization>();
        for (int i = 0; i < addLocalizationScripts.Length; i++)
        {
            addLocalizationScripts[i].loadLocalization(language);
        }

        // write language in variable to Localization Manager
        FindObjectOfType<LocalizationManager>().ActiveLanguage = language;
    }
}
