using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StairsManager : MonoBehaviour
{
    [SerializeField] float positionX;

    public void EnterRoom(GameObject character)
    {
        character.transform.position = character.transform.position + new Vector3(positionX, 0.0f, 0.0f);
    }
}
