using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTravel : MonoBehaviour
{
    public void LoadScene(string SceneNameToLoad)
    {
        SceneManager.LoadScene(SceneNameToLoad);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
