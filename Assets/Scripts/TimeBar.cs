using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxTime(float _timer)
    {
        slider.maxValue = _timer;
        slider.value = _timer;
    }
    public void SetTime(float _timer)
    {
        slider.value = _timer;
    }
}
