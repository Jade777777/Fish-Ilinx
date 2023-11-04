using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    CameraFollowPlayer cameraController;
    [SerializeField]
    Transform cameraTarget;

    [SerializeField] 
    GameObject menu;

    private void Update()
    {
        if (gameObject.GetComponent<Collider>().bounds.Contains(player.position))
        {
            cameraController.SetTargets(cameraTarget, cameraTarget);
            menu.SetActive(true);
        }
    }
}
