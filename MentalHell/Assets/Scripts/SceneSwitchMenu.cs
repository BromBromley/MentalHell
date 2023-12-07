using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchMenu : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
