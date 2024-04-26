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
        if (transform.position.y >= WaterSurfaceLevel)
        {
            RenderSettings.fog = false;
        }
        else
        {
            RenderSettings.fog = true;
            AdditiveFogDensity = Mathf.Clamp(((WaterSurfaceLevel - transform.position.y) / 3000), 0f, .02f);
            RenderSettings.fogDensity = InitialFogDensity + AdditiveFogDensity;
        }
    }
}