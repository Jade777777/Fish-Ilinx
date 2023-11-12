using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPath : Path
{
    public float MaxDistance;
    public float Weight = 1f;
    public override Vector3 Evaluate(Boid boid)
    {
        Vector3 direction = Vector3.zero;
        Vector3 offset = transform.position - boid.transform.position;

        if (offset.magnitude <= MaxDistance)
        {
            float magnitude = 1 - offset.magnitude / MaxDistance;
            direction = offset.normalized * Weight; 
        }

        return direction;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
}
