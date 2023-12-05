using UnityEngine;

public class WaterFog : MonoBehaviour
{
    [SerializeField] private float WaterSurfaceLevel;
    [SerializeField] private Color UnderwaterColor;
    [SerializeField] private FogMode FogMode;
    [SerializeField] private float InitialFogDensity = .02f;
    [SerializeField] private float AdditiveFogDensity = .005f;


    // Start is called before the first frame update
    void Start()
    {
        //RenderSettings.fog = false;
        SetUnderwater();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DepthDensity();
    }
    void SetUnderwater()
    {
       RenderSettings.fogColor = UnderwaterColor;
       RenderSettings.fogMode = FogMode;
       RenderSettings.fogDensity = InitialFogDensity;
    }
   

    void DepthDensity()
    {
        int Depth = 0;

        // You can change the y values to support the depth of your level
        if (transform.position.y > WaterSurfaceLevel)
        {
            Depth = default;
            
        }

        if (transform.position.y < WaterSurfaceLevel & transform.position.y >= 0)
        {
            Depth = 1;
        }

        if (transform.position.y < 0 & transform.position.y > -25)
        {
            Depth = 2;
        }

        if (transform.position.y < -25)
        {
            Depth = 3;
        }
        Debug.Log(Depth+ "    "+transform.position.y);
        switch (Depth)
        {
            case 3:
                RenderSettings.fog = true;
                RenderSettings.fogDensity = InitialFogDensity + (AdditiveFogDensity * 2);
                break;

            case 2:
                RenderSettings.fog = true;
                RenderSettings.fogDensity = InitialFogDensity + AdditiveFogDensity;
                break;

            case 1:
                RenderSettings.fog = true;
                RenderSettings.fogDensity = InitialFogDensity;
                break;

            default:
                RenderSettings.fog = false;
                break;
        }
    }
}