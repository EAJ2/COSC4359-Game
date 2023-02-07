using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
