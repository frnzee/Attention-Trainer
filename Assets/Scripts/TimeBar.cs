using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeBar : MonoBehaviour
{
    public Slider BarSlider;
    public void SetMaxTime(float _timer)
    {
        BarSlider.maxValue = _timer;
        BarSlider.value = _timer;
    }
    public void SetTime(float _timer)
    {
        BarSlider.value = _timer;
    }
}
