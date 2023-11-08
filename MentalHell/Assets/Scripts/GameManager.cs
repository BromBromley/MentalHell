using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private SwitchManager _switchManager;
    [SerializeField] private GameObject item;
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _switchManager = FindObjectOfType<SwitchManager>();
    }
}
