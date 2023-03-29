using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool bPaused = false;
    public GameObject MenuCanvas;
    public GameObject SaveCanvas;
    public GameObject ControlsCanvas;
    public Player player;

    private void Awake()
    {
        MenuCanvas.SetActive(false);
        SaveCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(bPaused)
            {
                HideAll();
            }
            else
            {
                ShowMenuCanvas();
            }
        }
    }

    public void ShowMenuCanvas()
    {
        MenuCanvas.SetActive(true);
        bPaused = true;
        Time.timeScale = 0f;
    }

    public void HideMenuCanvas()
    {
        MenuCanvas.SetActive(false);
    }

    public void ShowSaveCanvas()
    {
        SaveCanvas.SetActive(true);
    }

    public void HideSaveCanvas()
    {
        SaveCanvas.SetActive(false);
    }

    public void SaveButton()
    {
        HideMenuCanvas();
        ShowSaveCanvas();
    }

    public void YesButton()
    {
        HideSaveCanvas();
        ShowMenuCanvas();
        player.SavePlayer();
    }

    public void NoButton()
    {
        HideSaveCanvas();
        ShowMenuCanvas();
    }

    public void ControlsButton()
    {
        ControlsCanvas.SetActive(true);
        HideMenuCanvas();
    }

    public void HideAll()
    {
        HideMenuCanvas();
        HideSaveCanvas();
        ControlsCanvas.SetActive(false);
        bPaused = false;
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("V2MainMenu");
    }
}
