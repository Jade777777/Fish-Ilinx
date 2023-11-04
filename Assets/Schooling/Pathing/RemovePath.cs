using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemovePath : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private BoidProcess process;
    [SerializeField]
    private float radius =  10;//this should be pulled from the path base class
    private Path path;//this should be a path base class
    private void Start()
    {
        path = GetComponent<Path>();
       
    }
    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < radius)
        {
            process.paths.Remove(path);
            Destroy(path.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
