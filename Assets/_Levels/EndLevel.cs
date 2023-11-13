using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    BoidProcess process;
    [SerializeField]
    float range;

    private void Update()
    {
        if (gameObject.GetComponent<Collider>().bounds.Contains(player.position))
        {
            //SavedFishSpawn.savedFish = process.boidGridPartition.GetBoidsInRange(transform.position,range).Count - 1;
            SceneManager.LoadScene(0);
        }
    }
}
