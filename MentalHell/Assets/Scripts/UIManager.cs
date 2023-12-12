using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Slider switchSlider;
    private SwitchManager _switchManager;

    private bool hidePauseScreen = true;

    void Awake()
    {
        _switchManager = FindObjectOfType<SwitchManager>();
    }

    void Update()
    {
        UpdateSwitchBar();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

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

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void UpdateSwitchBar()
    {
        switchSlider.value = _switchManager.sanityLevel / 5;
    }
}
