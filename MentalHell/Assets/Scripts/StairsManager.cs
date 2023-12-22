using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StairsManager : MonoBehaviour
{
    // this script manages the usage of stairs and teleports the player accordingly
    // attached to every stairway, needs the 'Stairs' tag

    [SerializeField] private float positionX;

    // called by PlayerInteraction script
    public void EnterRoom(GameObject character)
    {
        StartCoroutine(TeleportDelay(character));
    }

    // delays the transport of the character in order to match with the fade to black
    private IEnumerator TeleportDelay(GameObject character)
    {
        yield return new WaitForSeconds(0.3f);

        character.transform.position = character.transform.position + new Vector3(positionX, 0.0f, 0.0f);
    }
}
