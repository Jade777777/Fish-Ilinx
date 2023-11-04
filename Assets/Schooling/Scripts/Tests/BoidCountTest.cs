using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoidCountTest : MonoBehaviour
{
    public BoidProcess boidProcess;
    public Transform target;
    public float range;
    public TMP_Text text;
    public void CheckBoidsInRange()
    {
        text.text = "Count: " + boidProcess.boidGridPartition.GetBoidsInRange(target.position, range).Count;
    }
}
