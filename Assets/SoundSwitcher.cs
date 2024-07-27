using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class SoundSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource start;
    [SerializeField] AudioSource repeat;
    void Start()
    {
        start.Play();
        Invoke("PlayLoopingClip", start.clip.length);
    }

    // Update is called once per frame
    public void PlayLoopingClip()
    {
        repeat.Play();
    }
}
