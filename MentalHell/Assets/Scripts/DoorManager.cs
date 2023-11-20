using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] float positionX;
    [SerializeField] float positionZ;

    public void EnterRoom(GameObject character)
    {
        //Debug.Log("entered room");
        character.transform.position = character.transform.position + new Vector3(positionX, 0.0f, positionZ);
    }
}
