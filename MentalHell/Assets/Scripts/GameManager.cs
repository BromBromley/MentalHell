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
    private DocumentManager _documentManager;

    private bool isRunning;

    void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _monsterAI = FindObjectOfType<MonsterAI>();
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _switchManager = FindObjectOfType<SwitchManager>();
        _documentManager = FindObjectOfType<DocumentManager>();
    }

    void Start()
    {
        PauseGame();
        StartCoroutine(PauseSound(0));
    }

    void Update()
    {
        // checks if the player pressed 'escape' and reacts accordingly
        if (Input.GetKeyDown(KeyCode.Escape) && !_playerInteraction.showingDocument && !_documentManager.showingInventory)
        {
            if (isRunning)
            {
                _uiManager.ActivatePauseScreen();
                PauseGame();
                StartCoroutine(PauseSound(0f));
            }
            else
            {
                _uiManager.ActivatePauseScreen();
                ResumeGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_playerInteraction.showingDocument)
            {
                ResumeGame();
                _playerInteraction.ClosingDocument();
            }
            else if (_documentManager.showingInventory)
            {
                ResumeGame();
                _documentManager.CloseAllDocuments();
                _documentManager.CloseInventory();
            }
        }

        // this checks if the player presses the I key to open the document overview
        if (Input.GetKeyDown(KeyCode.I) && !_documentManager.showingInventory)
        {
            _documentManager.OpenInventory();
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.I) && _documentManager.showingInventory)
        {
            _documentManager.CloseInventory();
            ResumeGame();
        }
    }

    void FixedUpdate()
    {
        // checks if the monster is close enough to attack the player
        if (_monsterAI.distance < 2 && !_switchManager.isSwitching && isRunning && !_playerInteraction.playerIsBusy)
        {
            _uiManager.ShowGameOverScreen();

            // Destroy All Sound Sources so that it is quiet in the Game Over Screen
            FindObjectOfType<AudioManager>().DestroyAllSound();

            // Play Death Splatter Sound
            Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxHerzAbgeben;
            FindObjectOfType<AudioManager>().PlayOnce("Herz_02", Soundarray);

            PauseGame();
            StartCoroutine(PauseSound(1f));
        }
    }

    // freezes the game and all update functions
    public void PauseGame()
    {
        Cursor.visible = true;
        isRunning = false;
        _playerMovement.movementEnabled = false;
        _spawnManager.checkingForSpawns = false;
        Time.timeScale = 0;
    }

    private IEnumerator PauseSound(float time)
    {
        yield return new WaitForSeconds(time);
        FindObjectOfType<AudioManager>().PauseAllSound();
    }

    // unfreezes the game and all update functions
    public void ResumeGame()
    {
        Cursor.visible = false;
        FindObjectOfType<AudioManager>().UnPauseAllSound();
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

    // checks if the player is switching or using the door and tells the monster what to do
    public IEnumerator SpawnMonster(float time)
    {
        if (!_switchManager.isSwitching && _playerInteraction.isInHallway)
        {
            yield return new WaitForSeconds(time);
            _spawnManager.FindClosestSpawnPoint();
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            _spawnManager.SendBackToStart();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
