using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // this scripts manages the player's ability to interact with items and doors

    private GameManager _gameManager;
    private DoorManager _doorManager;

    private bool pickedUpHeart = false;
    public int heartCounter;
    [SerializeField] private GameObject heartSprite;
    [SerializeField] private GameObject ghosts;
    [SerializeField] private Material twoGhosts;
    [SerializeField] private Material oneGhost;

    private bool canEnterDoor = true;
    public bool showingDocument;

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
                StartCoroutine(DoorCooldown());
                other.GetComponent<DoorManager>().EnterRoom(this.gameObject);
            }
        }

        if (other.tag == "Stairs")
        {
            if (Input.GetKey(KeyCode.E) && canEnterDoor)
            {
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
                pickedUpHeart = false;
                heartSprite.SetActive(false);
                heartCounter++;
                if (heartCounter == 1)
                {
                    ghosts.GetComponent<Renderer>().material = twoGhosts;
                }
                if (heartCounter == 2)
                {
                    ghosts.GetComponent<Renderer>().material = oneGhost;
                }
                if (heartCounter == 3)
                {
                    ghosts.SetActive(false);
                    _gameManager.GameWon();
                }
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
}
