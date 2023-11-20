using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class FollowMouse : MonoBehaviour
{
    public LayerMask collisionLayer;

    private Camera mainCamera;
    Transform player;
    private void Start()
    {
        mainCamera= Camera.main;
        player = FindObjectOfType<PlayerMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowM();
    }

    void FollowM()
    {
        // Get the mouse postion from the camera to move the player (Fish)
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane movementPlane = new Plane(Vector3.forward, Vector3.zero);

        if (movementPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPoint = ray.GetPoint(enter);

            Vector3 offset = targetPoint- player.position ;
            if (Physics.Raycast(player.position, offset, out RaycastHit hit, offset.magnitude,collisionLayer))
            {
                targetPoint = movementPlane.ClosestPointOnPlane(hit.point);
            }

            transform.position = targetPoint;
            
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
