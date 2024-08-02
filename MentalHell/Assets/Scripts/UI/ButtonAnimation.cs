using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonAnimation : MonoBehaviour
{
    public TMP_FontAsset newFont; // Die neue Schriftart, die bei Pointer Enter verwendet werden soll
    public TMP_FontAsset oldFont; // Die alte Schriftart, die bei Pointer Exit verwendet werden soll
    //public Image backgroundSprite; // Das Hintergrund-Sprite, das bei Pointer Enter aktiviert und bei Pointer Exit deaktiviert wird
    private TMP_Text buttonText;

    void Start()
    {
        // Holen Sie sich die TMP_Text-Komponente des Buttons
        buttonText = GetComponentInChildren<TMP_Text>();
        if (buttonText == null)
        {
            Debug.LogError("TMP_Text component not found. Make sure the script is attached to a GameObject with a TMP_Text child.");
        }

        // Deaktiviere das Hintergrund-Sprite initial
        //if (backgroundSprite != null)
        //{
        //    backgroundSprite.enabled = false;
        //}
        //else
        //{
        //    Debug.LogError("Background sprite not set. Please assign a background sprite in the inspector.");
        //}
    }

    public void OnPointerEnter()
    {
        // Wechseln zur neuen Schriftart
        if (buttonText != null && newFont != null)
        {
            buttonText.font = newFont;
        }

        // Aktivieren des Hintergrund-Sprites
        //if (backgroundSprite != null)
        //{
        //    backgroundSprite.enabled = true;
        //}
    }

    public void OnPointerExit()
    {
        // Wechseln zur alten Schriftart
        if (buttonText != null && oldFont != null)
        {
            buttonText.font = oldFont;
        }

        // Deaktivieren des Hintergrund-Sprites
        //if (backgroundSprite != null)
        //{
        //    backgroundSprite.enabled = false;
        //}
    }
}
