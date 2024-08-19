using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // this is the game manager of the main scene

    private UIManager _uiManager;
    private AudioManager _audioManager;
    private MonsterAI _monsterAI;
    private MonsterSpawnManager _monsterSpawnManager;
    private PlayerInteraction _playerInteraction;
    private SwitchManager _switchManager;
    private DocumentManager _documentManager;

    private bool isRunning;
    private bool isGameOver;

    public delegate void OnPausingGame();
    public static OnPausingGame onPausingGame;

    public delegate void OnResumingGame();
    public static OnResumingGame onResumingGame;

    public delegate void OnWinningGame();
    public static OnWinningGame onWinningGame;


    public delegate void OnUsingDoor(GameObject character);
    public static OnUsingDoor onUsingDoor;
    public static OnUsingDoor onUsingStairs;


    public delegate void OnSwitching(bool isSwitchtingUp);
    public static OnSwitching onSwitching;

    public delegate void OnPickingUpHeart();
    public static OnPickingUpHeart onPickingUpHeart;


    void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _monsterAI = FindObjectOfType<MonsterAI>();
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
        _monsterSpawnManager = FindObjectOfType<MonsterSpawnManager>();
        _switchManager = FindObjectOfType<SwitchManager>();
        _documentManager = FindObjectOfType<DocumentManager>();
    }


    void Start()
    {
        ResumeGame();

        _uiManager.continueButton.onClick.AddListener(ResumeGame);

        onWinningGame += WonGame;
        onSwitching += SpawnMonsterAfterSwitching;
    }


    void Update()
    {
        // checks if the pause menu should be activated/deactivated
        if (Input.GetKeyDown(KeyCode.Escape) && !_documentManager.showingDocument && !_documentManager.showingInventory)
        {
            if (isRunning)
            {
                StartCoroutine(PauseSound(0f));
                _uiManager.ActivatePauseScreen();
                PauseGame();
            }
            else
            {
                _uiManager.ActivatePauseScreen();
                ResumeGame();
            }
        }
        // checks if a document or the inventory is open and reacts accordingly
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_documentManager.showingInventory && !_documentManager.openedFromInventory)
            {
                ResumeGame();
                _documentManager.CloseInventory();
            }
            if (_documentManager.showingDocument && !_documentManager.openedFromInventory)
            {
                ResumeGame();
                _playerInteraction.ClosingDocument();
                _documentManager.CloseAllDocuments();
            }
            if (_documentManager.showingDocument && _documentManager.openedFromInventory)
            {
                _documentManager.ReturnToInventory();
            }
        }

        // this checks if the player presses the I key to open the document overview
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_documentManager.ActivatingInventory() == true)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }


    void FixedUpdate()
    {
        // checks if the monster is close enough to attack the player
        if (_monsterAI.distance < 2 && !_switchManager.isSwitching && isRunning && !_playerInteraction.playerIsBusy)
        {
            _uiManager.ShowGameOverScreen();

            // Destroy All Sound Sources so that it is quiet in the Game Over Screen
            _audioManager.DestroyAllSound();

            // Play Death Splatter Sound
            Sound[] Soundarray = _audioManager.sfxHerzAbgeben;
            _audioManager.PlayOnce("Herz_02", Soundarray);

            isGameOver = true;
            PauseGame();
            StartCoroutine(PauseSound(1f));
        }
    }



    // freezes the game and all update functions
    public void PauseGame()
    {
        onPausingGame?.Invoke();
        Cursor.visible = true;
        isRunning = false;
        if (!isGameOver)
        {
            Time.timeScale = 0;
        }
    }


    private IEnumerator PauseSound(float time)
    {
        yield return new WaitForSeconds(time);
        _audioManager.PauseAllSound();
    }



    // unfreezes the game and all update functions
    public void ResumeGame()
    {
        onResumingGame?.Invoke();
        Cursor.visible = false;
        _audioManager.UnPauseAllSound();
        isRunning = true;
        Time.timeScale = 1;
    }



    private void SpawnMonsterAfterSwitching(bool switchUp)
    {
        StartCoroutine(SpawnMonster(1f));
    }


    // checks if the player is switching or using the door and tells the monster what to do
    // replace check for 'isSwitching' with event?
    public IEnumerator SpawnMonster(float time)
    {
        if (!_switchManager.isSwitching && _playerInteraction.isInHallway)
        {
            yield return new WaitForSeconds(1f);
            _monsterSpawnManager.FindClosestSpawnPoint();
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            _monsterSpawnManager.SendBackToStart();
        }
    }



    private void WonGame()
    {
        StartCoroutine(PauseSound(1f));
        PauseGame();
    }



    // unsubcribes from events so they don't get called when the scene gets reloaded
    void OnDestroy()
    {
        onWinningGame -= WonGame;
        onSwitching -= SpawnMonsterAfterSwitching;
    }
}
