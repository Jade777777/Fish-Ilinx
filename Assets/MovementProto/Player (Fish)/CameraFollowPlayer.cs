using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 Offset = new Vector3 (0f, 0f, 0f);
    [SerializeField] private float SmoothTime = 0f;
    [SerializeField] private float MaxSpeed = 20f;
    private Vector3 cameraVelocity = Vector3.zero;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform Target;
    [SerializeField] private float maxDistanceFromPlayer;

    private float deadzone = 10f;
    [SerializeField]private AnimationCurve deadZoneFallOff;




    [HideInInspector] public bool boundedX;
    [HideInInspector] public bool boundedY;
    public float boundYPos;


    [HideInInspector]public bool pauseCameraUpdates = true;

    void Update()
    {
        if (!pauseCameraUpdates)
        {
            Vector3 offset = Target.position - Player.position;
            //prevent player from sliding allong terrain

            offset = Vector3.ClampMagnitude(offset, maxDistanceFromPlayer);

            Vector3 cameraFocus = Player.position + offset;

            float posX = cameraFocus.x + Offset.x;
            float posY = cameraFocus.y + Offset.y;

            if (boundedX) posX = this.transform.position.x;
            if (boundedY) posY = boundYPos; //Moves to y position of bounding box if effeect feels off use this.transform.position.y;

            Vector3 TargetPosition = new Vector3(posX, posY, cameraFocus.z + Offset.z);
            //Camera stays still on Z axis
            //new Vector3(posX, posY, transform.position.z);

            //deadzone
            Vector3 screenOffset = Target.position - transform.position + Offset;

            TargetPosition = Vector3.Lerp(transform.position, TargetPosition, deadZoneFallOff.Evaluate(screenOffset.magnitude / deadzone));

            transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref cameraVelocity, SmoothTime, MaxSpeed, Time.deltaTime);
        }
    }
    public void SetTargets(Transform player, Transform target)
    {
        Player = player;
        Target = target;
    }
}
