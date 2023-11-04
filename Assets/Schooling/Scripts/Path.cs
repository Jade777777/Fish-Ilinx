using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public abstract class Path : MonoBehaviour
{
    public abstract Vector3 Evaluate(Boid boid);
}
