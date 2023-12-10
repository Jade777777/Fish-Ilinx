using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject soundHolder;
    [SerializeField] int startValue;


    void Start()
    {
        // Get all audio sources in the sound holder
        AudioSource[] _audio = soundHolder.GetComponentsInChildren<AudioSource>();

        // Set inital values for slider
        slider.maxValue = 100;
        slider.minValue = 0;
        slider.value = startValue;

        // Slider listener to adjust volume
        slider.onValueChanged.AddListener((val) =>
        {
            foreach (var audio in _audio)
            {
                audio.volume = val / 100;
            }
        });
    }
}
