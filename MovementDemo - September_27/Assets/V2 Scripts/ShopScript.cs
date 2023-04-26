using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject shopMenuUI;

    private void Awake()
    {
        shopMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    void Resume()
    {
        shopMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        shopMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}