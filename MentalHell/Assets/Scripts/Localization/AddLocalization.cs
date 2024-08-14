using System;
using UnityEngine;
using System.Reflection;
using TMPro;

public class AddLocalization : MonoBehaviour
{

    public string LocalizationName = "default_localization_name";

    private TextMeshProUGUI toWriteTextTo;
    private string localization;
    private Localization[] LoreTexts;
    
    void OnEnable()
    {
        string language = FindObjectOfType<LocalizationManager>().ActiveLanguage;
        loadLocalization(language);
    }
    public void loadLocalization(string language)
    {

        // get the array with all the Localization depending on language
        if (language == "English")
        {
            LoreTexts = FindObjectOfType<LocalizationManager>().TextsEnglish;
            //Debug.Log("English");
        }
        if (language == "Deutsch")
        {
            LoreTexts = FindObjectOfType<LocalizationManager>().TextsDeutsch;
            //Debug.Log("Deutsch");
        }
        
        // get the text component on this element 
        toWriteTextTo = this.GetComponent<TextMeshProUGUI>();

        // find Localization by name
        Localization localizationValues = Array.Find(LoreTexts, x => x.name == LocalizationName);

        if (localizationValues == null)
        {
            toWriteTextTo.text = LocalizationName;
            return;
        }

        // write the text 
        toWriteTextTo.text = localizationValues.text;

        // write the font values
        toWriteTextTo.font = localizationValues.font_asset;
        toWriteTextTo.fontSize = localizationValues.font_size;
        toWriteTextTo.fontStyle = localizationValues.font_style;
        toWriteTextTo.color = localizationValues.font_color;

        // write allignment values
        toWriteTextTo.verticalAlignment = localizationValues.vertical_alignment;
        toWriteTextTo.horizontalAlignment = localizationValues.horizontal_alignment;
    }

}
