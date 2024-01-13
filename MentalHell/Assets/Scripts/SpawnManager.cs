using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // this script sets the monster to a spawn point closer to the player

    private List<GameObject> spawnPoints;
    private Vector3 closestPoint;
    private Vector3 secondClosestPoint;

    private float distancePlayerPoint;
    private float distancePlayerClosest;
    private float distancePlayerSecondClosest;

    public bool checkingForSpawns = true;

    void Awake()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn"));
    }

    /*
    // this checks for the distance between player and monster getting too big
    void Update()
    {
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
            }
            else if (distancePlayerPoint < distancePlayerSecondClosest && distancePlayerSecondClosest > distancePlayerClosest)
            {
                distancePlayerSecondClosest = distancePlayerPoint;
                secondClosestPoint = point.transform.position;
            }
        }

        //Debug.Log(distancePlayerClosest);
        //Debug.Log(distancePlayerSecondClosest);
        //Debug.Log(closestPoint);
        //Debug.Log(secondClosestPoint);

        // and transports the monster to said point if the player is far enough away
        if (distancePlayerClosest > 15)
        {
            Debug.Log("chose closest");
            transform.position = new Vector3(closestPoint.x, transform.position.y, transform.position.z);
            this.gameObject.GetComponent<MonsterAI>().ChooseDirection();
        }
        else
        {
            transform.position = new Vector3(secondClosestPoint.x, transform.position.y, transform.position.z);
            this.gameObject.GetComponent<MonsterAI>().ChooseDirection();
        }
    }
}
