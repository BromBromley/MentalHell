using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    private MonsterAI _monsterAI;
    private PlayerMovement _playerMovement;
    private SpawnManager _spawnManager;
    private PlayerInteraction _playerInteraction;
    private SwitchManager _switchManager;

    private bool isRunning;

    void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _monsterAI = FindObjectOfType<MonsterAI>();
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _switchManager = FindObjectOfType<SwitchManager>();
    }

    void Start()
    {
        ResumeGame();
    }

    void Update()
    {
        // checks if the player pressed escape and reacts accordingly
        if (Input.GetKeyDown(KeyCode.Escape) && _playerInteraction.showingDocument)
        {
            // close document 
            // ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isRunning)
        {
            _uiManager.ActivatePauseScreen();
            //PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isRunning)
        {
            _uiManager.ActivatePauseScreen();
            //ResumeGame();
        }
    }

    void FixedUpdate()
    {
        // checks if the monster is too close to the player
        if (_monsterAI.distance < 2.50 && !_switchManager.isSwitching)
        {
            //Debug.Log(_monsterAI.distance);
            if (isRunning)
            {
                _uiManager.ShowGameOverScreen();
                PauseGame();
            }
        }
    }

    // freezes the game and all update functions
    public void PauseGame()
    {
        isRunning = false;
        _playerMovement.movementEnabled = false;
        _spawnManager.checkingForSpawns = false;
        Time.timeScale = 0;
    }

    // unfreezes the game and all update functions
    public void ResumeGame()
    {
        isRunning = true;
        _playerMovement.movementEnabled = true;
        _spawnManager.checkingForSpawns = true;
        Time.timeScale = 1;
    }

    public void GameWon()
    {
        _uiManager.ShowWinScreen();
        PauseGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
