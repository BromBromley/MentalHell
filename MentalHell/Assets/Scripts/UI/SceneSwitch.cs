using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// functionality of this script has been merged with UIMainMenu.cs please implement this from now on

public class SceneSwitch : MonoBehaviour
{
    public void SceneSwitchPlus()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
