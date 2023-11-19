using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ColorChanger : MonoBehaviour
{
    Boid boid;
    public Renderer boidRenderer;
    public Color baseColor;
    public Color changerColor;
    public float maxNeighbors=6;
    private void Start()
    {
        boid = GetComponent<Boid>();

        boidRenderer.sharedMaterial.SetFloat("_TimeOffset", Random.Range(0f,5f));
        Debug.Log(boidRenderer.sharedMaterial.GetFloat("_TimeOffset"));
    }
    void Update()
    {
        Color targetColor = Color.Lerp(baseColor, changerColor, boid.neighbors.Count / maxNeighbors);
        
        boidRenderer.material.color = Color.Lerp(boidRenderer.material.color, targetColor, 1 - Mathf.Exp(-10* Time.deltaTime));

    }
}
