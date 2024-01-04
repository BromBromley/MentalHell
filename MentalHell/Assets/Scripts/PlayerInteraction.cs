using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // this scripts manages the player's ability to interact with items and doors

    private GameManager _gameManager;
    private DoorManager _doorManager;
    public Animator doorAnimator;

    [SerializeField] private bool pickedUpHeart = false;
    public int heartCounter;
    [SerializeField] private GameObject heartSprite;
    [SerializeField] private GameObject ghosts;
    [SerializeField] private Material twoGhosts;
    [SerializeField] private Material oneGhost;
    [SerializeField] private GameObject fadeEffect;

    private bool gameWon = false;
    private bool canEnterDoor = true;
    public bool showingDocument;

    private Sound[] Soundarray;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _doorManager = FindObjectOfType<DoorManager>();

        heartSprite.SetActive(false);
    }

    // this function checks what the player is interacting with
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Door")
        {
            if (Input.GetKey(KeyCode.E) && canEnterDoor)
            {
                fadeEffect.SetActive(true);
                StartCoroutine(DoorCooldown());
                other.GetComponent<DoorManager>().EnterRoom(this.gameObject);
            }
        }

        if (other.tag == "Stairs")
        {
            if (Input.GetKey(KeyCode.E) && canEnterDoor)
            {
                fadeEffect.SetActive(true);
                StartCoroutine(DoorCooldown());
                other.GetComponent<StairsManager>().EnterRoom(this.gameObject);
            }
        }

        if (other.tag == "Heart")
        {
            if (Input.GetKey(KeyCode.E) && pickedUpHeart == false)
            {
                pickedUpHeart = true;
                heartSprite.SetActive(true);
                other.gameObject.SetActive(false);

                // Play Herz Pickup Sound
                Soundarray = FindObjectOfType<AudioManager>().sfxHerzNehmen;
                FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);
            }
        }

        if (other.tag == "Cabinet")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (other.transform.childCount > 0)
                {
                    /*
                    showingDocument = true;
                    other.GetComponentInChildren<DocumentManager>().ShowDocument();
                    _gameManager.PauseGame();
                    */
                    // press key to close document and resume game
                    // show document in menu
                }
                else
                {
                    // rattle sound
                }
            }
        }

        // tracks how many hearts the player has given to the ghosts and changes the ghosts' material accordingly
        if (other.tag == "Ghost")
        {
            if (Input.GetKey(KeyCode.E) && pickedUpHeart)
            {
                //ghosts.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.black, 0.5f );
                pickedUpHeart = false;
                heartSprite.SetActive(false);
                heartCounter++;
                if (heartCounter == 1)
                {
                    StartCoroutine(FadeGhostMaterial(twoGhosts));
                }
                if (heartCounter == 2)
                {
                    StartCoroutine(FadeGhostMaterial(oneGhost));
                }
                if (heartCounter == 3)
                {
                    ghosts.SetActive(false);
                    _gameManager.GameWon();
                    gameWon = true;
                    doorAnimator.SetBool("open", gameWon);
                }

                // Play Herz Drop Sound
                Soundarray = FindObjectOfType<AudioManager>().sfxHerzAbgeben;
                FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);

                // Play Geister Befreit Sound
                Soundarray = FindObjectOfType<AudioManager>().sfxNPCGeisterBefreit;
                FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);


            }
        }
    }

    // this prevents accidentally going through doors
    private IEnumerator DoorCooldown()
    {
        canEnterDoor = false;

        yield return new WaitForSeconds(1);

        canEnterDoor = true;
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
}
