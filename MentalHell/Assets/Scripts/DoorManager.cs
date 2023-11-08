using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] float positionX;
    [SerializeField] float positionZ;

    public void EnterRoom()
    {
        Debug.Log("entered room");
        player.transform.position = new Vector3(positionX, 0.0f, positionZ);
    }
}
