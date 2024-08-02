using UnityEngine;
using TMPro;

public class DisplayBuildVersion : MonoBehaviour
{
    public TextMeshProUGUI versionText; // Das TextMeshPro-Textfeld zur Anzeige der Versionsnummer

    void Start()
    {
        // Setze den Text des TextMeshPro-Elements auf die aktuelle Versionsnummer
        versionText.text = Application.version;
    }
}

