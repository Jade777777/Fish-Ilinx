using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedFishSpawn : MonoBehaviour
{
    public BoidProcess boidProcess;
    public GameObject boidPrefab;
    public float SpawnRadius = 10;
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        int savedFish = gameManager.GetCollectedFishFromAllLevels();
        for (int i = 0; i < savedFish; i++)
        {
            boidProcess.InitBoid(boidPrefab, Random.insideUnitSphere * SpawnRadius + transform.position, Quaternion.identity);
        }
    }
}
