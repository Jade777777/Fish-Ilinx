using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private LayerMask Background;
    [SerializeField] private LayerMask Deadzone;
    [SerializeField] private float MovementSpeed;
    

    Rigidbody Rb;
    bool CanMove;


    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, Background) && CanMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, raycasthit.point, Time.deltaTime * MovementSpeed);
            transform.LookAt(new Vector3(raycasthit.point.x, raycasthit.point.y, transform.position.z));
        }
        
        if (Physics.Raycast(ray, out raycasthit, float.MaxValue, Deadzone))
        {
            CanMove = false;
            transform.LookAt(new Vector3(raycasthit.point.x, raycasthit.point.y, transform.position.z));
        }
        else
        {
            CanMove = true;
        }

    }

    private void FixedUpdate()
    {

    }
}
