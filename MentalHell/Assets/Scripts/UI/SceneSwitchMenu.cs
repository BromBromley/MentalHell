using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// functionality of this script has been merged with UIMainMenu.cs please implement this from now on

public class SceneSwitchMenu : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

        public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
