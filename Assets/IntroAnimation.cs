using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    public Transform bounds;
    public Vector3 MoveBoundsOnStart = Vector3.back*50;
    public GameObject tuttorialUI;
    public void StartGame()
    {
        bounds.transform.position += MoveBoundsOnStart;
        tuttorialUI.SetActive(true);
        Invoke("DisableUI", 6f);
    }
    public void DisableUI()
    {
        tuttorialUI.SetActive(false);
    }
}
