using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // this script manages most HUD elements and screens 


    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject fadeEffect;

    public Button continueButton;

    private bool hidePauseScreen;



    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        hidePauseScreen = true;

        continueButton = pauseScreen.transform.Find("ContinueButton").GetComponent<Button>();

        GameManager.onWinningGame += ShowWinScreen;
    }



    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }


    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }



    // activates/deactivates the pause screen, called by GameManager
    public void ActivatePauseScreen()
    {
        if (hidePauseScreen)
        {
            fadeEffect.SetActive(true);
            StartCoroutine(WaitForFade());
        }
        else
        {
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(false);
        }
        hidePauseScreen = !hidePauseScreen;
    }



    private IEnumerator WaitForFade()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        pauseScreen.SetActive(true);
    }



    void OnDestroy()
    {
        GameManager.onWinningGame -= ShowWinScreen;
    }
}
