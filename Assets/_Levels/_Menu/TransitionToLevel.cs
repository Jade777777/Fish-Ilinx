using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor;
using UnityEngine;

public class TransitionToLevel : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    CameraFollowPlayer cameraController;
    [SerializeField]
    Transform cameraTarget;
    public void SetUpMenuTransition()
    {
        cameraController.SetTargets(cameraTarget, cameraTarget);

        this.enabled = false;
    }
}
