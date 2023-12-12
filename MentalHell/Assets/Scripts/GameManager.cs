using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    private MonsterAI _monsterAI;

    void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _monsterAI = FindObjectOfType<MonsterAI>();
    }

    void Update()
    {
        if (_monsterAI.distance < 0.75)
        {
            _uiManager.ShowGameOverScreen();
        }
    }
}
