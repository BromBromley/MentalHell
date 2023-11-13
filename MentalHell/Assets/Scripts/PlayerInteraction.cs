using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // this scripts manages the player's ability to interact with items and doors

    private DoorManager _doorManager;
    private bool pickedUpHeart = false;

    void Start()
    {
        _doorManager = FindObjectOfType<DoorManager>();
    }

    // this function checks if the player is standing in front of a door or item
    // it also manages the heart drop off
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Door")
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("entering door");
                other.GetComponent<DoorManager>().EnterRoom();
            }
        }

        if (other.tag == "Heart")
        {
            if (Input.GetKey(KeyCode.F) && pickedUpHeart == false)
            {
                Debug.Log("you picked it up");
                pickedUpHeart = true;
                other.gameObject.SetActive(false);
                // show heart in UI
            }
        }

        if (other.tag == "Document")
        {
            if (Input.GetKey(KeyCode.F))
            {
                other.gameObject.SetActive(false);
                // show document
            }
        }

        if (other.tag == "Ghost")
        {
            if (Input.GetKey(KeyCode.F) && pickedUpHeart)
            {
                Debug.Log("you dropped the heart off");
                pickedUpHeart = false;
                // heartCounter++;
            }
        }
    }
}
