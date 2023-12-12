using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Slider switchSlider;
    private SwitchManager _switchManager;

    void Awake()
    {
        _switchManager = FindObjectOfType<SwitchManager>();

        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        UpdateSwitchBar();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void UpdateSwitchBar()
    {
        switchSlider.value = _switchManager.sanityLevel / 5;
    }
}
