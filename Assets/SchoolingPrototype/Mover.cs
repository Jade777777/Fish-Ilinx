using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float turnSpeed =90;
    public float acceleration = 3f;

    
    private Boid boid;
    private void Start()
    {
        boid = GetComponent<Boid>();
    }
    void Update()
    {
        boid.CurrentVelocity = Vector3.RotateTowards(boid.CurrentVelocity, boid.TargetVelocity, Mathf.Deg2Rad * Time.deltaTime * turnSpeed, acceleration * Time.deltaTime);
        transform.position += (boid.CurrentVelocity * Time.deltaTime);
        if(boid.CurrentVelocity != Vector3.zero)transform.forward = boid.CurrentVelocity;
    }
}
