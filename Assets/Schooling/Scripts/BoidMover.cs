using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoidMover : MonoBehaviour
{
    public float turnSpeed = 90;
    public float avoidanceTurnSpeed = 180;

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
        float impactRange = 0.5f;
        Vector3 direction = boid.TargetVelocity.normalized;
        AvoidObstacles(out Vector3 avoidance, out float obstacleDistance);

        float avoidanceWeight = Mathf.Pow(Mathf.Clamp(avoidance.magnitude, 0, 1 - impactRange) / (1 - impactRange), 2);
        direction = Vector3.Slerp(direction, avoidance, avoidanceWeight);

        Vector3 targetVelocity = direction.normalized * boid.TargetVelocity.magnitude;

        float deltaRadians = Mathf.Deg2Rad * Time.deltaTime * Mathf.Lerp(turnSpeed, avoidanceTurnSpeed, avoidanceWeight);
        boid.CurrentVelocity = Vector3.RotateTowards(boid.CurrentVelocity, targetVelocity, deltaRadians, acceleration * Time.deltaTime);


        #region CollisionDamping
        float impactDot = Vector3.Dot(boid.CurrentVelocity, avoidance.normalized);//cur velocity in avoidance direction
        if (impactDot < 0 &&
            obstacleDistance > 0 &&
            avoidance.magnitude > 1 - impactRange)
        {
            if (obstacleDistance <= Mathf.Abs(impactDot) * Time.deltaTime)
            {
                boid.CurrentVelocity += avoidance.normalized * impactDot;
            }
            else
            {
                float dampImpact = Mathf.Pow(impactDot, 2) / (obstacleDistance);//stop movement by impact time (vi^2/d)
                boid.CurrentVelocity += avoidance.normalized * dampImpact * Time.deltaTime; //
            }
        }

        #endregion

        transform.position += (boid.CurrentVelocity * Time.deltaTime);

        if (boid.CurrentVelocity != Vector3.zero) transform.forward = boid.CurrentVelocity;
    }

    private Vector3 AvoidObstacles(out Vector3 avoidance, out float obstacleDistance)
    {
        avoidance = Vector3.zero;

        float distance = 3;// This should be determined by turn radius or something like that, might move the whole thing to the motor script
        obstacleDistance = distance;
        float velocityScale = size + Time.deltaTime * boid.CurrentVelocity.magnitude;
        Collider[] collisions = Physics.OverlapSphere(transform.position, distance + velocityScale, collisionLayer);

        float maxMagnitude = 0;

        foreach (Collider collision in collisions)
        {

            Vector3 offset = (collision.ClosestPoint(transform.position) - transform.position);
            float magnitude = 1 - ((offset.magnitude - velocityScale) / distance);
            maxMagnitude = Mathf.Max(maxMagnitude, magnitude);
            obstacleDistance = Mathf.Min(obstacleDistance, offset.magnitude - velocityScale);
            avoidance -= offset * magnitude;
        }
        avoidance = avoidance.normalized * maxMagnitude;


        return avoidance;
    }
}
