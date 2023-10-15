using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public BoidProcess boidProcess;

    public GameObject boidPrefab;

    public float SpawnRadius=10;
    public void SpawnBoids(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            boidProcess.InitBoid( boidPrefab,Random.insideUnitSphere*SpawnRadius, Quaternion.identity);
        }
    }
}
