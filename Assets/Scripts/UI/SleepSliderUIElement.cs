using System;
using UnityEngine;
using UnityEngine.UI;

public class SleepSliderUIElement : MonoBehaviour
{
    private Slider slider;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = SleepManager.Instance.TimeToSleepNormalized;
    }
}