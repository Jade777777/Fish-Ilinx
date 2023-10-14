using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 Offset = new Vector3 (0f, 0f, 0f);
    [SerializeField] private float SmoothTime = 0f;
    private Vector3 Velocity = Vector3.zero;

    [SerializeField] private Transform Target;

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPosition = Target.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref Velocity, SmoothTime);
    }

    private void FixedUpdate()
    {

    }
}
