using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // this script sets the monster to a spawn point closer to the player

    private List<GameObject> spawnPoints;
    private Vector3 closestPoint;
    private Vector3 secondClosestPoint;
    private Vector3 timeOut;

    private float distancePlayerPoint;
    private float distancePlayerClosest;
    private float distancePlayerSecondClosest;

    public bool checkingForSpawns = true;

    void Awake()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn"));
    }
    void Start()
    {
        timeOut = new Vector3(-60, transform.position.y, transform.position.z);
    }

    /*
    // this checks for the distance between player and monster getting too big
    void Update()
    {
        // Debug.Log(timeOut);
        if (this.gameObject.GetComponent<MonsterAI>().distance > 50 && checkingForSpawns)
        {
            FindClosestSpawnPoint();
        }
    }
    */


    // this goes through the list of spawn points to find the one closest to the player
    public void FindClosestSpawnPoint()
    {
        distancePlayerSecondClosest = 100f;
        secondClosestPoint = Vector3.zero;
        closestPoint = Vector3.zero;

        // this compares all the spawn points to find the closest and the second closest
        foreach (GameObject point in spawnPoints)
        {
            distancePlayerPoint = Vector3.Distance(this.gameObject.GetComponent<MonsterAI>().player.transform.position, point.transform.position);
            distancePlayerClosest = Vector3.Distance(this.gameObject.GetComponent<MonsterAI>().player.transform.position, closestPoint);
            if (distancePlayerPoint < distancePlayerClosest)
            {
                if (distancePlayerClosest < distancePlayerSecondClosest)
                {
                    distancePlayerSecondClosest = distancePlayerClosest;
                    secondClosestPoint = closestPoint;
                }
                closestPoint = point.transform.position;
                distancePlayerClosest = distancePlayerPoint;
            }
            else if (distancePlayerPoint < distancePlayerSecondClosest && distancePlayerSecondClosest > distancePlayerClosest)
            {
                distancePlayerSecondClosest = distancePlayerPoint;
                secondClosestPoint = point.transform.position;
            }
        }

        // and transports the monster to said point if the player is far enough away
        if (distancePlayerClosest > 20)
        {
            transform.position = new Vector3(closestPoint.x, transform.position.y, transform.position.z);
            this.gameObject.GetComponent<MonsterAI>().ChooseDirection();
        }
        else
        {
            transform.position = new Vector3(secondClosestPoint.x, transform.position.y, transform.position.z);
            this.gameObject.GetComponent<MonsterAI>().ChooseDirection();
        }
    }

    // sends the monster outside of the hallway when the player is switching or in a room
    public void SendBackToStart()
    {
        transform.position = timeOut;
    }
}
