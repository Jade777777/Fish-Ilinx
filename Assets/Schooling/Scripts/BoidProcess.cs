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

    


    public  List<Path> paths;

    public List<Boid> boids = new List<Boid>();

    public LayerMask collisionLayer;



    public BoidGridPartition boidGridPartition=  new BoidGridPartition(2);

    
    void Start()
    {
        foreach (Path path in FindObjectsOfType(typeof(Path)))
        {
            paths.Add(path);
        }
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
            Vector3 newVelocity = boid.CurrentVelocity;

            newVelocity += AlignmentRule(boid)*0.2f;//TODO: Velocity is not being matched properly
            newVelocity += CohesionRule(boid)*0.3f;
            newVelocity += AvoidanceRule(boid)*5f;
            //newVelocity += ObstacleAvoidanceRule(boid)*10f;//this should be checking and limiting the overall movement rather than
            newVelocity += PathingRule(boid)*1f;
            newVelocity += BoundsRule(boid)*10f;
            boid.TargetVelocity = Vector3.ClampMagnitude(newVelocity, boidMaxSpeed);   //
            
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


    public Vector3 PathingRule(Boid boid) // add different types of pathing behaviours
    {
        boid.isPathing = false;
        Vector3 direction = Vector3.zero;
        foreach(Path path in paths)
        {
            direction += path.Evaluate(boid);

        }
        if(direction.magnitude > 0)
        {
            boid.isPathing=true;
        }
        return direction;
    }
    public Vector3 BoundsRule(Boid boid)
    {
        Vector3 direction = Vector3.zero;
        bounds = boundCollider.bounds;
        if (!bounds.Contains(boid.transform.position))
        {
            Vector3 offset = bounds.ClosestPoint(boid.transform.position)-boid.transform.position;
            float magnitude = offset.magnitude/boundFallOffDistance;
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
