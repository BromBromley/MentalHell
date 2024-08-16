using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    // this scripts manages the player's ability to interact with items and doors

    // TODO event for using stairs and doors

    private GameManager _gameManager;
    private AudioManager _audioManager;
    private PlayerMovement _playerMovement;
    private DocumentManager _documentManager;
    public GameObject interactIcon;

    public bool pickedUpHeart = false;
    public int heartCounter;
    [SerializeField] private GameObject heartSprite;
    [SerializeField] private GameObject ghosts;
    [SerializeField] private Material twoGhosts;
    [SerializeField] private Material oneGhost;
    [SerializeField] private GameObject fadeEffect;
    [SerializeField] private GameObject Counter1;
    [SerializeField] private GameObject Counter2;
    [SerializeField] private GameObject Counter3;
    [SerializeField] private GameObject Counter4;

    private bool canEnterDoor = true;
    private bool canOpenStorage = true;
    public bool playerIsBusy = false;
    public bool showingDocument;
    public bool isInHallway = true;
    public bool playAnimation;
    private GameObject storage;
    public bool playOpenDoor;
    private Vector3 boxSize = new Vector3(1f, 1f, 0f);
    public GameObject entrance_door;
    private MeshRenderer door_renderer;
    private SpriteRenderer door_sprite_renderer;
    public Animator door_animator;

    private Sound[] Soundarray;



    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _documentManager = FindObjectOfType<DocumentManager>();

        heartSprite.SetActive(false);
        interactIcon.SetActive(false);
    }



    // this function checks what the player is interacting with
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Storage")
        {
            if (Input.GetKey(KeyCode.E) && canOpenStorage)
            {
                // Play Metall Schrank Öffnene Sound
                Sound[] Soundarray = _audioManager.sfxMetallSchrankOffnen;
                _audioManager.PlayRandomOnce(Soundarray);

                StartCoroutine(StorageCooldown());
                StartCoroutine(PlayerIsInvincible());
                // gets class Interactable and Method Interact from StorageManager.cs
                if (other.GetComponent<Interactable>())
                {
                    other.GetComponent<Interactable>().Interact();
                }
                if (other.GetComponentInChildren<DocumentInteraction>() != null)
                {
                    showingDocument = true;
                    storage = other.gameObject;
                    _documentManager.ShowRandomDocument();
                    _gameManager.PauseGame();
                    interactIcon.SetActive(false);
                    storage.GetComponentInChildren<DocumentInteraction>().DestroyDocument();
                }
            }
        }

        if (other.tag == "Door")
        {
            if (Input.GetKey(KeyCode.E) && canEnterDoor)
            {
                playOpenDoor = true;
                isInHallway = !isInHallway;
                if (!isInHallway)
                {
                    other.GetComponent<DoorManager>().EnterRoom(this.gameObject);
                }
                else
                {
                    other.GetComponent<DoorManager>().ExitRoom(this.gameObject);
                }
                StartCoroutine(_gameManager.SpawnMonster(1f));
                fadeEffect.SetActive(true);
                StartCoroutine(DoorCooldown());
                StartCoroutine(PlayerIsInvincible());
                StartCoroutine(_playerMovement.StopMovement());

                // GameManager.onUsingDoor?.Invoke(this.gameObject);
            }
        }

        if (other.tag == "MainEntrance")
        {
            if (Input.GetKey(KeyCode.E))
            {
                playOpenDoor = true;
                SceneManager.LoadScene("Main");
                StartCoroutine(_gameManager.SpawnMonster(1f));
                fadeEffect.SetActive(true);
                StartCoroutine(DoorCooldown());
                StartCoroutine(PlayerIsInvincible());
                StartCoroutine(_playerMovement.StopMovement());
            }
        }

        if (other.tag == "Stairs")
        {
            if (Input.GetKey(KeyCode.E) && canEnterDoor)
            {
                StartCoroutine(_gameManager.SpawnMonster(1f));
                fadeEffect.SetActive(true);
                StartCoroutine(DoorCooldown());
                StartCoroutine(PlayerIsInvincible());
                StartCoroutine(_playerMovement.StopMovement());
                other.GetComponent<StairsManager>().EnterRoom(this.gameObject);

                // Play Stairs Up Sound
                Soundarray = _audioManager.sfxStairsUp;
                _audioManager.PlayRandomOnce(Soundarray);

                // GameManager.onUsingStairs?.Invoke(this.gameObject);
            }
        }

        if (other.tag == "Heart")
        {
            if (Input.GetKey(KeyCode.E) && pickedUpHeart == false)
            {
                GameManager.onPickingUpHeart?.Invoke();
                PickedUpHeart(other.gameObject);
            }
        }

        // tracks how many hearts the player has given to the ghosts and changes the ghosts' material accordingly
        if (other.tag == "Ghost")
        {
            if (Input.GetKey(KeyCode.E) && pickedUpHeart)
            {
                StartCoroutine(PlayerIsInvincible());
                pickedUpHeart = false;
                heartSprite.SetActive(false);
                heartCounter++;

                // Play Geister Befreit Sound
                Soundarray = _audioManager.sfxNPCGeisterBefreit;
                _audioManager.PlayRandomOnce(Soundarray);

                if (heartCounter == 1)
                {
                    StartCoroutine(FadeGhostMaterial(twoGhosts));
                    Counter1.SetActive(false);
                    Counter2.SetActive(true);
                }
                if (heartCounter == 2)
                {
                    StartCoroutine(FadeGhostMaterial(oneGhost));
                    Counter2.SetActive(false);
                    Counter3.SetActive(true);
                }
                if (heartCounter == 3)
                {
                    ghosts.SetActive(false);
                    Counter3.SetActive(false);
                    Counter4.SetActive(true);
                    door_renderer = entrance_door.GetComponent<MeshRenderer>();
                    door_sprite_renderer = entrance_door.GetComponentInChildren<SpriteRenderer>();
                    door_sprite_renderer.enabled = true;
                    door_renderer.enabled = false;
                    door_animator = entrance_door.GetComponentInChildren<Animator>();
                    door_animator.SetBool("gameWon", true);
                    StartCoroutine(AnimationDelay(20f));
                    GameManager.onWinningGame?.Invoke();
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Heart")
        {
            OpenInteractableIcon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Heart")
        {
            CloseInteractableIcon();
        }
    }



    private IEnumerator AnimationDelay(float waitTime)
    {
        playAnimation = true;
        yield return new WaitForSeconds(waitTime);
        playAnimation = false;
    }



    // this prevents accidentally going through doors
    private IEnumerator DoorCooldown()
    {
        canEnterDoor = false;
        yield return new WaitForSeconds(1);
        canEnterDoor = true;
    }


    // this prevents the storage sounds from playing too often
    private IEnumerator StorageCooldown()
    {
        canOpenStorage = false;
        yield return new WaitForSeconds(1);
        canOpenStorage = true;
    }



    // gives the player a second where they can't get attacked by the monster
    private IEnumerator PlayerIsInvincible()
    {
        playerIsBusy = true;
        yield return new WaitForSeconds(2);
        playerIsBusy = false;
    }



    // gives the ghosts a fade when swapping their material
    private IEnumerator FadeGhostMaterial(Material ghostMaterial)
    {
        float fadeTime = 0f;
        float speed = 2f;

        while (ghosts.GetComponent<Renderer>().material.color != Color.black)
        {
            fadeTime += Time.deltaTime * speed;
            ghosts.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.black, fadeTime);
            yield return null;
        }

        ghosts.GetComponent<Renderer>().material = ghostMaterial;
        ghosts.GetComponent<Renderer>().material.color = Color.black;
        fadeTime = 0f;

        while (ghosts.GetComponent<Renderer>().material.color != Color.white)
        {
            fadeTime += Time.deltaTime * speed;
            ghosts.GetComponent<Renderer>().material.color = Color.Lerp(Color.black, Color.white, fadeTime);
            yield return null;
        }
    }



    public void ClosingDocument()
    {
        StartCoroutine(PlayerIsInvincible());
        showingDocument = false;
        _documentManager.CloseDocument();
        interactIcon.SetActive(true);
    }


    private void PickedUpHeart(GameObject heart)
    {
        pickedUpHeart = true;
        heartSprite.SetActive(true);
        heart.SetActive(false);

        // Play Herz Pickup Sound
        Soundarray = _audioManager.sfxHerzNehmen;
        _audioManager.PlayRandomOnce(Soundarray);
    }


    // this manages the interact icon while the player stands in a collider
    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }
}
