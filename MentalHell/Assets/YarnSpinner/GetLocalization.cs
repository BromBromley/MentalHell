using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLocalization : MonoBehaviour
{
    [SerializeField] private GameObject dialogueRunner;

    void Awake ()
    {
        dialogueRunner = GameObject.Find("Dialogue_System");
    }

    private string[] languageCodes = {
        "en",
        "de - DE",
    };

    public void SwitchCodes(int languageIndex)
    {
        string language = languageCodes[languageIndex];
        //dialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = language;
    }
}
