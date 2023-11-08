using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private DoorManager _doorManager;

    void Start()
    {
        _doorManager = FindObjectOfType<DoorManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        if (other.tag == "Door")
        {
            Debug.Log("entering room");
            other.GetComponent<DoorManager>().EnterRoom();
        }

        if (other.tag == "Item")
        {
            Debug.Log("there is an item in front of you");

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("you picked it up");
                other.gameObject.SetActive(false);
            }
        }
    }
}
