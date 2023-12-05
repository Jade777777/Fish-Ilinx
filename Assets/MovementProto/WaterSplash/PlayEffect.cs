using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PlayEffect : MonoBehaviour
{
    public Transform WaterSurfaceLevel;
    public VisualEffect _WaterTrail;
    public ParticleSystem _RippleEffect;
    public Transform Player;
    //WaterLevel = 22;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Breaching();
        Skimming();
    }

    void Breaching()
    {
        if (Player.position.y > (WaterSurfaceLevel.position.y + .55) && Player.position.y < (WaterSurfaceLevel.position.y + 1.55))
        {
            VisualEffect NewWaterTrail = Instantiate(_WaterTrail, transform.position, transform.rotation);

            NewWaterTrail.Play();
           
            Destroy(NewWaterTrail.gameObject, 1f);
        }
    }

    void Skimming()
    {
        if (Player.position.y >= WaterSurfaceLevel.position.y && Player.position.y < (WaterSurfaceLevel.position.y + .53))
        {
            ParticleSystem NewRipple = Instantiate(_RippleEffect, transform.position, transform.rotation);

            NewRipple.Play();

            Destroy(NewRipple.gameObject, 1f);
        }
        else
        {
            _RippleEffect.Stop();
        }
    }

    /*
    void Splash()
    {
        if (Player.position.y >= 21.5 && Player.position.y < 21.52)
        {
            SplashEffect.SetActive(true);

            VisualEffect NewWaterSplash = Instantiate(_WaterSplash, transform.position, transform.rotation);

            NewWaterSplash.Play();

            Destroy(NewWaterSplash.gameObject, 1f);
        }
        else
        {
            SplashEffect.SetActive(false);
        }
    }
    */
}
