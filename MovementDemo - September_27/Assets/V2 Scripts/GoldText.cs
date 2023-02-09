using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    public Text goldText;

    public Stats stats;

    void Update()
    {
        goldText.text = stats.gold.ToString();
    }
}
