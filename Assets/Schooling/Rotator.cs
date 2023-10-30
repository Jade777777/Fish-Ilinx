using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float degreesPerSecond = 5;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, degreesPerSecond * Time.deltaTime);
    }
}
