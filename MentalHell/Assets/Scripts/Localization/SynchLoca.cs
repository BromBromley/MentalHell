using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;   

//used to synchronize the language settings between the loca options and yarnspinner
//this sucks and i hate it
public class SynchLoca : MonoBehaviour
{
    [SerializeField] private GameObject DialogueRunner;
    [SerializeField] public string ActiveLanguage;

    void Awake()
    {
        ActiveLanguage = FindObjectOfType<LocalizationManager>().ActiveLanguage;
        if (ActiveLanguage == "English")
        {
            DialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "en";
        }
        if (ActiveLanguage == "Deutsch")
        {
            DialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "de";
        }
    }
    public void ChangeLanguage(string language)
    {
        if (language == "English")
        {
            DialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "en";
        }
        if (language == "Deutsch")
        {
            DialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "de";
        }
    }
}
