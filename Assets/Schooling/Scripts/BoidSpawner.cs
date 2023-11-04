using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public BoidProcess boidProcess;

    public GameObject boidPrefab;

    public float SpawnRadius=10;

    public int SpawnAmount=10;

    private void Start()
    {
        SpawnBoids(SpawnAmount);
    }
    public void SpawnBoids(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            boidProcess.InitBoid( boidPrefab,Random.insideUnitSphere*SpawnRadius +transform.position, Quaternion.identity);
        }
    }
}
