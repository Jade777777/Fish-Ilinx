using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMover : MonoBehaviour
{
    public float turnSpeed =90;
    public float acceleration = 3f;
    public float size=0.5f;
    public LayerMask collisionLayer;
    
    private Boid boid;
    private void Start()
    {
        boid = GetComponent<Boid>();
    }
    void Update()
    {
        if (Physics.Raycast(transform.position,
                            transform.forward,
                            out RaycastHit hit,
                            Time.deltaTime * boid.CurrentVelocity.magnitude + size,
                            collisionLayer))
        {
            Collide();
        }

        boid.CurrentVelocity = Vector3.RotateTowards(boid.CurrentVelocity, boid.TargetVelocity, Mathf.Deg2Rad * Time.deltaTime * turnSpeed, acceleration * Time.deltaTime);
        transform.position += (boid.CurrentVelocity * Time.deltaTime);

        if (boid.CurrentVelocity != Vector3.zero) transform.forward = boid.CurrentVelocity;
    }
    private void Collide()
    {
        float dist = 1f;
        Collider[] collisions = Physics.OverlapSphere(transform.position, dist);
        Vector3 direction = Vector3.zero;
        boid.CurrentVelocity = Vector3.zero;
    }
}
