using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonSFXFix : MonoBehaviour
{
    public void PlayClickSFX()
    {
        FindObjectOfType<AudioManager>().PlayClickSFX();
    }
    public void PlayHoverSFX()
    {
        FindObjectOfType<AudioManager>().PlayHoverSFX();
    }
}
