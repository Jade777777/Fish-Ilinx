using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMover : MonoBehaviour
{
    public GameObject targetPosition;
    public float smoothDistance= 8f;
    public float maxSpeed = 10f;
    public float minSpeed = 4f;

    public float turnSpeed = 90;
    public float avoidanceTurnSpeed = 180;
    public float acceleration = 3f;
    public float size = 1f;
    public LayerMask collisionLayer;

    public float deadzone = 8f;

    public float sinusoidalMagnitude=1f;
    public float sinusoidalFrequency = 5;
    private Boid boid;


    private void Start()
    {
        boid = GetComponent<Boid>();
        
    }
    void Update()
    {
        Vector3 offset = (targetPosition.transform.position - transform.position);
        
        float magnitude = (offset.magnitude-deadzone) / smoothDistance;

        float impactRange = 0.5f;
        Vector3 direction = offset.normalized + (transform.up * Mathf.Sin(Time.time*sinusoidalFrequency) * sinusoidalMagnitude*Time.deltaTime);
        AvoidObstacles(out Vector3 avoidance, out float obstacleDistance);

        float avoidanceWeight = Mathf.Pow(Mathf.Clamp(avoidance.magnitude, 0, 1 - impactRange) / (1 - impactRange), 2);
        direction = Vector3.Slerp(direction, avoidance, avoidanceWeight);

        Vector3 targetVelocity = direction.normalized * Mathf.Lerp(minSpeed, maxSpeed, magnitude);
        

        float deltaRadians = Mathf.Lerp(Mathf.Deg2Rad * Time.deltaTime * turnSpeed,Mathf.Deg2Rad*Time.deltaTime*avoidanceTurnSpeed ,avoidanceWeight );//Mathf.Pow(avoidance.magnitude,2)
        boid.CurrentVelocity = Vector3.RotateTowards(boid.CurrentVelocity, targetVelocity ,deltaRadians , acceleration * Time.deltaTime);



        #region CollisionDamping
        float impactDot = Vector3.Dot(boid.CurrentVelocity, avoidance.normalized);//cur velocity in avoidance direction
        if (impactDot < 0 &&
            avoidance.magnitude > 1 - impactRange)
        {
            
            if (obstacleDistance <= Mathf.Abs(impactDot*2) * Time.deltaTime)
            {
                boid.CurrentVelocity -= avoidance.normalized * impactDot;
                Debug.Log("Impacting");
            }
            else
            {
                float dampImpact = Mathf.Pow(impactDot, 2) / (obstacleDistance - Mathf.Abs(impactDot) * Time.deltaTime);//stop movement by impact time (vi^2/d)
                boid.CurrentVelocity += avoidance.normalized * dampImpact * Time.deltaTime; //
            }
        }
        #endregion


        transform.position += (boid.CurrentVelocity * Time.deltaTime);

        if (boid.CurrentVelocity != Vector3.zero)
        {
            Vector3 targetDirection = boid.CurrentVelocity.normalized;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
           
            transform.rotation = Quaternion.RotateTowards( transform.rotation,targetRotation, 480 * Time.deltaTime);



            
        }

    }
  




    private Vector3 AvoidObstacles(out Vector3 direction, out float obstacleDistance)
    {
        direction = Vector3.zero;
        
        float distance = 3;// This should be determined by turn radius or something like that, might move the whole thing to the motor script
        obstacleDistance = distance;
        float velocityScale = size + Time.deltaTime * boid.CurrentVelocity.magnitude;
        Collider[] collisions = Physics.OverlapSphere(transform.position, distance + velocityScale, collisionLayer);

        float maxMagnitude = 0;
        
        foreach (Collider collision in collisions)
        {
            
            Vector3 offset = (collision.ClosestPoint(transform.position) - transform.position);
            float magnitude =1 - ((offset.magnitude - velocityScale) / distance);
            maxMagnitude=  Mathf.Max(maxMagnitude, magnitude);
            obstacleDistance = Mathf.Min(obstacleDistance, offset.magnitude - velocityScale);
            direction -= offset*magnitude;
        }
        direction = direction.normalized* maxMagnitude;
        
   
        return direction;
    }
}
