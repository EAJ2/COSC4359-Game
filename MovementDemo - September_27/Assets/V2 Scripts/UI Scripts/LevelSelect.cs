using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("CF_1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("CF_2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("CF_3");
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
