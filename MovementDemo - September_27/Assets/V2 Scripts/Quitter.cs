using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitter : MonoBehaviour
{
    private void OnEnable()
    {
        Application.Quit();
        Debug.Log("You quit");
    }
}
