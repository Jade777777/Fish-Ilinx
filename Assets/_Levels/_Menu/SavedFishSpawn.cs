using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedFishSpawn : MonoBehaviour
{
    public static int savedFish;
    public BoidProcess boidProcess;
    public GameObject boidPrefab;
    public float SpawnRadius = 10;
    private void Start()
    {

        for (int i = 0; i < savedFish; i++)
        {
            boidProcess.InitBoid(boidPrefab, Random.insideUnitSphere * SpawnRadius + transform.position, Quaternion.identity);
        }
    }
}
