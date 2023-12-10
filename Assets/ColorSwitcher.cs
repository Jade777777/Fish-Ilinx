using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public List<Texture2D> sprites = new List<Texture2D>();
    
    // Start is called before the first frame update
    void Start()
    {
        int randIndex = Random.Range(0, sprites.Count);
        GetComponent<MeshRenderer>().material.mainTexture = sprites[randIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
