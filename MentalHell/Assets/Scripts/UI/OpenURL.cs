using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenLink()
    {
        Application.OpenURL("https://zeltoss.itch.io/mental-hell");
        Debug.Log("Opened Mental Hell Website");
    }
}
