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
    PlayerMover player;
    LineRenderer lineRenderer;

    [SerializeField]
    Color startColor;
    [SerializeField]
    Color endColor;
    [SerializeField]
    Color maxColor;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();


        mainCamera= Camera.main;
        player = FindObjectOfType<PlayerMover>();
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

            Vector3 offset = targetPoint- player.transform.position ;
            if (Physics.Raycast(player.transform.position, offset, out RaycastHit hit, offset.magnitude,collisionLayer))
            {
                targetPoint = movementPlane.ClosestPointOnPlane(hit.point);
            }

            transform.position = targetPoint;
            
        }

        
        Vector3 lineOffset = transform.position - player.transform.position ;
        lineOffset = Vector3.ClampMagnitude(lineOffset, player.smoothDistance + player.deadzone);
        Vector3 startOffset = Vector3.ClampMagnitude(lineOffset, player.deadzone/2);
        lineRenderer.SetPosition(0, player.transform.position+lineOffset);
        lineRenderer.SetPosition(1, player.transform.position +startOffset);

        float magnitude = (lineOffset.magnitude-player.deadzone) / (player.smoothDistance);
        lineRenderer.startColor = Color.Lerp(endColor, maxColor, magnitude);
        lineRenderer.endColor = startColor;


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.5f);
        if (player != null)
        {
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}
