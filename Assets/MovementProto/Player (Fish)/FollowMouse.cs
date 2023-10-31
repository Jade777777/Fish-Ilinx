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
    [SerializeField] private LayerMask Avoidable;
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
        FollowM();
    }

    void FollowM()
    {
        // Get the mouse postion from the camera to move the player (Fish)
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, Background) && CanMove == true)
        {
            //transform.position = Vector3.MoveTowards(transform.position, raycasthit.point, Time.deltaTime * MovementSpeed);
            transform.position = raycasthit.point;
            transform.LookAt(new Vector3(raycasthit.point.x, raycasthit.point.y, transform.position.z));
        }

        // Controls the Deadzone of the player (Fish)
        if (Physics.Raycast(ray, out raycasthit, float.MaxValue, Deadzone))
        {
            CanMove = false;
            transform.LookAt(new Vector3(raycasthit.point.x, raycasthit.point.y, transform.position.z));
        }
        else
        {
            CanMove = true;
        }


        //Plane movementPlane = new Plane(Vector3.forward, Vector3.zero);

        //if(movementPlane.Raycast(ray, out float enter))
        //{
        //    Vector3 hitPoint = ray.GetPoint(enter);
        //    Vector3 offset = hitPoint - transform.position;
        //    Rb.Move(transform.position + offset.normalized * MovementSpeed * Time.deltaTime,Quaternion.LookRotation(offset,Vector3.up));
        //    Debug.Log(offset);
        //}

    }


    private void FixedUpdate()
    {

    }
}
