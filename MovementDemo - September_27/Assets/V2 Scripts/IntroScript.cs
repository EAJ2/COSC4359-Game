using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("V2MainMenu");
        Debug.Log("You switched scenes");
    }
}