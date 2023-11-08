using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }
}
