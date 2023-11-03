using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BoidProcess : MonoBehaviour
{
    public BoxCollider boundCollider;
    private Bounds bounds;
    public float boundFallOffDistance = 1f;
    public float boidRange = 3;
    public float avoidanceRange = 2;
    public float viewAngle =270;
    public float boidMaxSpeed=10;
    public float boidMinSpeed=4;

    


    public List<GameObject> targets;

    public List<Boid> boids = new List<Boid>();

    public LayerMask collisionLayer;



    public BoidGridPartition boidGridPartition=  new BoidGridPartition(2);

    
    void Start()
    {
        bounds = boundCollider.bounds;
        foreach(Boid boid in FindObjectsOfType(typeof(Boid)))
        {
            boids.Add(boid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Boid boid in boids)//update the boids positions in the boid partition before calculating velocity
        {
            boidGridPartition.UpdateBoid(boid);
        }
        foreach(Boid boid in boids)
        {
            Vector3 newVelocity = Vector3.zero;
            newVelocity += AlignmentRule(boid)*0.2f;//TODO: Velocity is not being matched properly
            newVelocity += CohesionRule(boid)*0.3f;
            newVelocity += AvoidanceRule(boid)*5f;
            //newVelocity += ObstacleAvoidanceRule(boid)*10f;//this should be checking and limiting the overall moveemnt rather than
            newVelocity += TargetRule(boid)*1f;
            newVelocity += BoundsRule(boid)*10f;
            boid.TargetVelocity = Vector3.ClampMagnitude(newVelocity+ boid.CurrentVelocity, boidMaxSpeed);   //
            
            if (boid.TargetVelocity.magnitude < boidMinSpeed)
            {
                boid.TargetVelocity = boid.TargetVelocity.normalized * boidMinSpeed;
            }
        }
        
    }



    private Vector3 AlignmentRule(Boid boid)
    {
        Vector3 averageVelocity = Vector3.zero;
        List<Boid> neighbors = boidGridPartition.GetBoidsInRange(boid, boidRange, viewAngle);
        if (neighbors.Count > 0)
        {
            foreach (Boid neighbor in neighbors)
            {
                averageVelocity += neighbor.CurrentVelocity;// TODO: get the actual velocity of the boid, not the calculated
                
            }
            averageVelocity = averageVelocity / neighbors.Count;
            return averageVelocity - boid.CurrentVelocity;
        }
        else
        {
            return Vector3.zero;
        }
    }
    private Vector3 CohesionRule(Boid boid)
    {
        Vector3 centerOfMass = Vector3.zero;
        List<Boid> neighbors = boidGridPartition.GetBoidsInRange(boid, boidRange, viewAngle);
        if (neighbors.Count > 0)
        {
            foreach (Boid neighbor in neighbors)
            {
                centerOfMass += neighbor.transform.position;
                
            }
            centerOfMass = centerOfMass / (float)neighbors.Count;
            Vector3 direction = centerOfMass - boid.transform.position;
            float magnitude = 1 - direction.magnitude / boidRange;
            return direction.normalized*magnitude;
        }
        else
        {
            return Vector3.zero;
        }
    }
    private Vector3 AvoidanceRule(Boid boid)
    {
        Vector3 direction = Vector3.zero;
        List<Boid> neighbors = boidGridPartition.GetBoidsInRange(boid, avoidanceRange, viewAngle);
        if (neighbors.Count > 0)
        {
            foreach (Boid neighbor in neighbors)
            {
                
                float magnitude = 1-((neighbor.transform.position - boid.transform.position).magnitude / avoidanceRange);
                direction -= (neighbor.transform.position - boid.transform.position).normalized * magnitude;
            }
        }
        return direction;
    }

    private Vector3 ObstacleAvoidanceRule(Boid boid)
    {
        Vector3 direction = Vector3.zero;
        float distance= 5;// This should be determined by turn radius or something like that, might move the whole thing to the motor script

        Collider[] collisions = Physics.OverlapSphere(boid.transform.position, distance,collisionLayer);

        foreach (Collider collision in collisions)
        {
            Vector3 offset = (collision.ClosestPoint(boid.transform.position) - boid.transform.position);
            float magnitude = 1 - (offset.magnitude / distance);
            
            direction -= offset.normalized * magnitude;

        }


        return direction; 
    }

    public Vector3 TargetRule(Boid boid)
    {
        float maxDistance =14;
        Vector3 direction = Vector3.zero;
        foreach(GameObject target in targets)
        {
            Vector3 offset = target.transform.position - boid.transform.position;
            
            if(offset.magnitude <= maxDistance)
            {
                float magnitude = 1 - offset.magnitude / maxDistance;
  
                direction += offset.normalized; //*magnitude;
            }
        }
        return direction;
    }
    public Vector3 BoundsRule(Boid boid)
    {
        Vector3 direction = Vector3.zero;
        if (!bounds.Contains(boid.transform.position))
        {
            Vector3 offset = bounds.ClosestPoint(boid.transform.position)-boid.transform.position;
            float magnitude = offset.magnitude/boundFallOffDistance;
           // magnitude = Mathf.Pow( magnitude, 2);
            direction = offset.normalized * magnitude;
        }
        return direction;
    }

    public void InitBoid(GameObject boidPrefab, Vector3 position, Quaternion rotation)
    {
        
        GameObject newGo = Instantiate(boidPrefab,position, rotation);
        Boid newBoid = newGo.GetComponent<Boid>();
        newBoid.TargetVelocity = Random.onUnitSphere * boidMaxSpeed;
        newBoid.CurrentVelocity= newBoid.TargetVelocity;
        boids.Add(newBoid);
    }


}
