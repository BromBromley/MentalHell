using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawnManager : MonoBehaviour
{
    // this script manages the spawning of the hearts the player has to collect

    private List<GameObject> spawnPoints;



    private void Start()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Heart"));
        SpawnHearts();
    }



    // picks three of the hearts and deactivates all the other ones
    private void SpawnHearts()
    {
        for (int i = 0; i < 3; i++)
        {
            spawnPoints.RemoveAt(Random.Range(0, spawnPoints.Count));
        }

        foreach (GameObject heart in spawnPoints)
        {
            heart.SetActive(false);
        }
    }
}
