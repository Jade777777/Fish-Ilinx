using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterEffect : MonoBehaviour
{
    [SerializeField] private float WaterLevel;
    private bool IsUnderwater;
    [SerializeField] private Color NormalColor;
    [SerializeField] private Color UnderwaterColor;


    // Start is called before the first frame update
    void Start()
    {
       RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position.y < WaterLevel))
        {
            RenderSettings.fog = true;
            SetUnderwater();
        }
        else
        {
            SetNormal();
        }
    }

    void SetNormal()
    {
        RenderSettings.fogColor = NormalColor;
        
    }

    void SetUnderwater()
    {
        RenderSettings.fogColor = UnderwaterColor;
        
    }

}
