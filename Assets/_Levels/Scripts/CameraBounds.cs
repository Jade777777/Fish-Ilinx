using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] private CameraFollowPlayer cameraScript;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraScript == null) cameraScript = this.transform.parent.GetComponentInChildren<CameraFollowPlayer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if (this.tag == "CameraBoundsX") cameraScript.boundedX = true;
        else if (this.tag == "CameraBoundsY") { 
            cameraScript.boundedY = true;
            cameraScript.boundYPos = this.transform.position.y;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        if (this.tag == "CameraBoundsX") cameraScript.boundedX = false;
        else if (this.tag == "CameraBoundsY") cameraScript.boundedY = false;
    }
}
