using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxXP(float xp)
    {
        slider.maxValue = xp;
        slider.value = xp;
    }

    public void SetXP(float xp)
    {
        slider.value = xp;
    }
}
