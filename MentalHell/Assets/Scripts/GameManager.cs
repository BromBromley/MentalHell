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

    private bool isRunning;

    void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _monsterAI = FindObjectOfType<MonsterAI>();
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        // checks if the monster is too close to the player
        if (_monsterAI.distance < 2.50)
        {
            _uiManager.ShowGameOverScreen();
        }

        // checks if the player pressed escape and reacts accordingly
        if (Input.GetKeyDown(KeyCode.Escape) && _playerInteraction.showingDocument)
        {
            // close document 
            // ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isRunning)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isRunning)
        {
            ResumeGame();
        }
    }

    // freezes the game and all update functions
    public void PauseGame()
    {
        isRunning = false;
        _uiManager.ActivatePauseScreen();
        _playerMovement.movementEnabled = false;
        _spawnManager.checkingForSpawns = false;
        Time.timeScale = 0;
    }

    // unfreezes the game and all update functions
    public void ResumeGame()
    {
        isRunning = true;
        _uiManager.ActivatePauseScreen();
        _playerMovement.movementEnabled = true;
        _spawnManager.checkingForSpawns = true;
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
