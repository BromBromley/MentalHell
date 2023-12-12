using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] float positionX;

    public void EnterRoom(GameObject character)
    {
        if (character.transform.position.z <= 0)
        {
            character.transform.position = character.transform.position + new Vector3(positionX, 0.0f, 59.37f);
        }
        else
        {
            character.transform.position = character.transform.position + new Vector3(positionX, 0.0f, -59.37f);
        }
    }
}
