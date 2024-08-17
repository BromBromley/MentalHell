using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro_GameManager : MonoBehaviour
{
    // this script is the game manager of the intro scene

    private Intro_PlayerControls _introPlayerControls;
    private AudioManager _audioManager;
    private UIManager _uiManager;

    private bool isRunning;
    private bool isStart;


    void Start()
    {
        _introPlayerControls = FindObjectOfType<Intro_PlayerControls>();
        _audioManager = FindObjectOfType<AudioManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _uiManager.continueButton.onClick.AddListener(ResumeGame);

        isStart = true;
        ResumeGame();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isRunning)
            {
                StartCoroutine(PauseSound(0f));
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }



    private void PauseGame()
    {
        _uiManager.ActivatePauseScreen();
        isRunning = false;
        _introPlayerControls.movementEnabled = false;
        Cursor.visible = true;
        Time.timeScale = 0;
    }


    private IEnumerator PauseSound(float time)
    {
        yield return new WaitForSeconds(time);
        _audioManager.PauseAllSound();
    }


    private void ResumeGame()
    {
        if (!isStart)
        {
            _uiManager.ActivatePauseScreen();
        }
        else
        {
            isStart = false;
        }
        isRunning = true;
        _introPlayerControls.movementEnabled = true;
        _audioManager.UnPauseAllSound();
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
