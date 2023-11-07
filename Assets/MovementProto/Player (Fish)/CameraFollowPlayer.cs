using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 Offset = new Vector3 (0f, 0f, 0f);
    [SerializeField] private float SmoothTime = 0f;
    [SerializeField] private float MaxSpeed = 20f;
    private Vector3 Velocity = Vector3.zero;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform Target;
    [SerializeField] private float maxDistanceFromPlayer;

    private float deadzone = 10f;
    [SerializeField]private AnimationCurve deadZoneFallOff;

    // Update is called once per frame
    void Update()
    {
        

        Vector3 offset = Target.position - Player.position;
        

        offset = Vector3.ClampMagnitude(offset, maxDistanceFromPlayer);
        Vector3 cameraFocus = Player.position + offset;

        Vector3 TargetPosition = cameraFocus + Offset;
        //if (Vector3.Distance(TargetPosition,transform.position)< deadzone) TargetPosition = transform.position;
        Vector3 screenOffset = Target.position - transform.position + Offset;
        TargetPosition = Vector3.Lerp(transform.position, TargetPosition, deadZoneFallOff.Evaluate( screenOffset.magnitude/deadzone));
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref Velocity, SmoothTime,20,Time.deltaTime);
    }
    public void SetTargets(Transform player, Transform target)
    {
        Player = player;
        Target = target;
    }

}
