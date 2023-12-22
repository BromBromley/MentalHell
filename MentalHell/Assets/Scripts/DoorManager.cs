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
        if (character.transform.position.z <= 0)
        {
            StartCoroutine(TeleportDelayForward(character));

            // play Door sound effect of opening the door when entering the room
            Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxOpenDoor;
            FindObjectOfType<AudioManager>().PlayRandomConstantly(Soundarray);
        }
        else
        {
            StartCoroutine(TeleportDelayBackward(character));

            // play Door sound effect of closing the door when leaving the room
            Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxCloseDoor;
            FindObjectOfType<AudioManager>().PlayRandomConstantly(Soundarray);
        }
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
}
