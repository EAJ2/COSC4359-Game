using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditChange : MonoBehaviour
{
    public void ButtonSceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("you're in credits now");
    }
}
