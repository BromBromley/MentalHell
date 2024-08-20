using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;   

//used to synchronize the language settings between the loca options and yarnspinner
//this sucks and i hate it
public class SynchLoca : MonoBehaviour
{
    [SerializeField] private GameObject LanguageOptions;
    [SerializeField] private GameObject DialogueRunner;
    [SerializeField] private string ActiveLanguage;

    // Update is called once per frame
    void Update()
    {
        ActiveLanguage = LanguageOptions.GetComponent<LocalizationSwitch>().language;
        if (ActiveLanguage == "English")
        {
            DialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "en";
        }
        if (ActiveLanguage == "Deutsch")
        {
            DialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "de";
        }
    }
}
