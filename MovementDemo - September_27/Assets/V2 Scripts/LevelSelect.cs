using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void loadLevel()
    {
        SceneManager.LoadScene("CF_1");
    }
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
