using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 Offset = new Vector3 (0f, 0f, 0f);
    [SerializeField] private float SmoothTime = 0f;
    private Vector3 Velocity = Vector3.zero;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform Target;
    [SerializeField] private float maxDistanceFromPlayer;

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = Target.position - Player.position;
        offset = Vector3.ClampMagnitude(offset, maxDistanceFromPlayer);
        Vector3 cameraFocus = Player.position + offset;

        Vector3 TargetPosition = cameraFocus + Offset;
        
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref Velocity, SmoothTime);
    }

}
