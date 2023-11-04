using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemovePath : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform path;//this should be a path base class
    [SerializeField]
    private BoidProcess process;
    [SerializeField]
    private float radius =  10;//this should be pulled from the path base class

    private void Update()
    {
        if (Vector3.Distance(player.position, path.position) < radius)
        {
            process.paths.Remove(path.gameObject);
            Destroy(path.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
