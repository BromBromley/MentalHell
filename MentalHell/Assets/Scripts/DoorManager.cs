using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // this script manages the usage of doors and teleports the player accordingly
    // attached to every door, needs the 'Door' tag

    [SerializeField] private float positionX;



    // called by PlayerInteraction script
    public void EnterRoom(GameObject character)
    {
        StartCoroutine(TeleportDelayForward(character));

        // play Door sound effect of opening the door when entering the room
        Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxOpenDoor;
        FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);
    }


    public void ExitRoom(GameObject character)
    {
        StartCoroutine(TeleportDelayBackward(character));

        // play Door sound effect of closing the door when leaving the room
        Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxCloseDoor;
        FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);
    }



    // delays the transport of the character in order to match with the fade to black
    private IEnumerator TeleportDelayForward(GameObject character)
    {
        yield return new WaitForSeconds(0.3f);

        character.transform.position = character.transform.position + new Vector3(positionX, 0.0f, 59.37f);
    }


    private IEnumerator TeleportDelayBackward(GameObject character)
    {
        yield return new WaitForSeconds(0.3f);

        character.transform.position = character.transform.position + new Vector3(positionX, 0.0f, -59.37f);
    }


    // checks if the player is standing in the door colliders and shows the interaction icon
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>().OpenInteractableIcon();
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>().CloseInteractableIcon();
        }
    }
}
