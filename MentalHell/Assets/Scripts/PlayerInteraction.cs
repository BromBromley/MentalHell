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

    private bool canEnterDoor = true;
    public bool showingDocument;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _doorManager = FindObjectOfType<DoorManager>();

        heartSprite.SetActive(false);
    }

    // this function checks if the player is standing in front of a door or item
    // it also manages the heart drop off
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Door")
        {
            if (Input.GetKey(KeyCode.W) && canEnterDoor)
            {
                //Debug.Log("entering door");
                StartCoroutine(DoorCooldown());
                other.GetComponent<DoorManager>().EnterRoom(this.gameObject);
            }
        }

        if (other.tag == "Stairs")
        {
            if (Input.GetKey(KeyCode.W) && canEnterDoor)
            {
                StartCoroutine(DoorCooldown());
                other.GetComponent<StairsManager>().EnterRoom(this.gameObject);
            }
        }

        if (other.tag == "Heart")
        {
            if (Input.GetKey(KeyCode.E) && pickedUpHeart == false)
            {
                Debug.Log("you picked up the heart");
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
                    showingDocument = true;
                    other.GetComponentInChildren<DocumentManager>().ShowDocument();
                    _gameManager.PauseGame();
                    // press key to close document and resume game
                    // show document in menu
                }
                else
                {
                    // rattle sound
                }
            }
        }

        if (other.tag == "Ghost")
        {
            if (Input.GetKey(KeyCode.E) && pickedUpHeart)
            {
                Debug.Log("you dropped the heart off");
                pickedUpHeart = false;
                heartSprite.SetActive(false);
                heartCounter++;
                if (heartCounter == 3)
                {
                    Debug.Log("You did it! The ghosts are happy and you can finally leave");
                }
            }
        }
    }

    private IEnumerator DoorCooldown()
    {
        canEnterDoor = false;

        yield return new WaitForSeconds(1);

        canEnterDoor = true;
    }
}
