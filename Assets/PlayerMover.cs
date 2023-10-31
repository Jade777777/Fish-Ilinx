using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public GameObject targetPosition;
    public float smoothDistance= 10f;
    public float maxSpeed = 10f;
    public float minSpeed = 4f;

    public float turnSpeed = 90;
    public float acceleration = 3f;
    public float size = 0.5f;
    public LayerMask collisionLayer;

    private Boid boid;


    private void Start()
    {
        boid = GetComponent<Boid>();
        
    }
    void Update()
    {
        Vector3 offset = Vector3.zero;
        if (Physics.Raycast(transform.position,
                            transform.forward,
                            out RaycastHit hit,
                            Time.deltaTime * boid.CurrentVelocity.magnitude + size,
                            collisionLayer))
        {
            Collide();
        }
        else 
        { 
        offset = (targetPosition.transform.position - transform.position);
        }
        float magnitude = offset.magnitude/smoothDistance;
        Vector3 direction = offset.normalized;
        direction += AvoidObstacles() ;

        
        Vector3 targetVelocity = Vector3.Lerp(direction.normalized * minSpeed, direction.normalized * maxSpeed, magnitude);

        boid.CurrentVelocity = Vector3.RotateTowards(boid.CurrentVelocity, targetVelocity, Mathf.Deg2Rad * Time.deltaTime * turnSpeed, acceleration * Time.deltaTime);
        transform.position += (boid.CurrentVelocity * Time.deltaTime);

        if (boid.CurrentVelocity != Vector3.zero) transform.forward = boid.CurrentVelocity;
    }
    private void Collide()
    {
        float dist = 1f;

        boid.CurrentVelocity = Vector3.zero;
    }
    private Vector3 AvoidObstacles()
    {
        Vector3 direction = Vector3.zero;
        float distance = 5;// This should be determined by turn radius or something like that, might move the whole thing to the motor script

        Collider[] collisions = Physics.OverlapSphere(transform.position, distance, collisionLayer);

        foreach (Collider collision in collisions)
        {
            Vector3 offset = (collision.ClosestPoint(transform.position) - transform.position);
            float magnitude = 1 - (offset.magnitude / distance);

            direction -= offset.normalized * magnitude;

        }
        return direction;
    }
}
