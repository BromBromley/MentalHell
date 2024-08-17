using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Playtime : MonoBehaviour
{
    // this script keeps track of the time the player takes to beat the game
    // and displays it at the winning screen

    [SerializeField] private TextMeshProUGUI timerText;

    private float currentTime;



    void Start()
    {
        GameManager.onWinningGame += ShowTime;
    }


    void Update()
    {
        currentTime += Time.deltaTime;
    }



    private void ShowTime()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



    void OnDisable()
    {
        GameManager.onWinningGame -= ShowTime;
    }
}
