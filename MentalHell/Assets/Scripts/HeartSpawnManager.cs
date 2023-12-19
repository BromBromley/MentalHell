using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawnManager : MonoBehaviour
{
    // this script randomly selects three of the hearts placed in the game

    private List<GameObject> spawnPoints;

    private void Start()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Heart"));
        SpawnHearts();
    }

    public void SpawnHearts()
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
