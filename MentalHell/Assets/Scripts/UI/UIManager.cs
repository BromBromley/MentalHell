using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // this script manages most HUD elements and screens 

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Slider switchSlider;
    private SwitchManager _switchManager;

    private bool hidePauseScreen;

    void Awake()
    {
        _switchManager = FindObjectOfType<SwitchManager>();
    }

    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        hidePauseScreen = true;
    }

    void Update()
    {
        UpdateSwitchBar();
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
            pauseScreen.SetActive(false);
        }
        else
        {
            pauseScreen.SetActive(true);
        }
        hidePauseScreen = !hidePauseScreen;
    }

    // updates the switch bar to the match the player's sanity level
    public void UpdateSwitchBar()
    {
        switchSlider.value = _switchManager.sanityLevel / 5;
    }
}