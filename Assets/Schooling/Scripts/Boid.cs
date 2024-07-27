using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 TargetVelocity =Vector3.zero;
    public Vector3 CurrentVelocity = Vector3.zero;
    public List<Boid> neighbors;
    public bool isPathing;
    
}
