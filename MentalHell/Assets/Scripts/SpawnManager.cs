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

    private float distancePlayerPoint;
    private float distancePlayerClosest;

    public bool checkingForSpawns = true;

    void Awake()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn"));
    }

    // this checks for the distance between player and monster getting too big
    void Update()
    {
        if (this.gameObject.GetComponent<MonsterAI>().distance > 50 && checkingForSpawns)
        {
            FindClosestSpawnPoint();
        }
    }

    // this goes through the list of spawn points to find the one closest to the player
    public void FindClosestSpawnPoint()
    {
        //Debug.Log("searching for spawn point");
        foreach (GameObject point in spawnPoints)
        {
            distancePlayerPoint = Vector3.Distance(this.gameObject.GetComponent<MonsterAI>().player.transform.position, point.transform.position);
            distancePlayerClosest = Vector3.Distance(this.gameObject.GetComponent<MonsterAI>().player.transform.position, closestPoint);
            if (distancePlayerPoint < distancePlayerClosest)
            {
                closestPoint = point.transform.position;
            }
        }

        // and transports the monster to said point if the player is far enough away
        if (distancePlayerClosest > 15)
        {
            transform.position = new Vector3(closestPoint.x, transform.position.y, transform.position.z);
            this.gameObject.GetComponent<MonsterAI>().ChooseDirection();
        }
    }
}
