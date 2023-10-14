using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private LayerMask layermask;
    [SerializeField] private float MovementSpeed;
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, layermask))
            {
                transform.position = Vector3.MoveTowards(transform.position, raycasthit.point, Time.deltaTime * MovementSpeed);
                transform.LookAt(new Vector3(raycasthit.point.x, raycasthit.point.y, transform.position.z));
            }
        }
    }

    private void FixedUpdate()
    {

    }
}
