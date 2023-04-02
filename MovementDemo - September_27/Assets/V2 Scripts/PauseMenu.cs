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
    public GameObject RespawnCanvas;
    public Player player;

    private bool bRespawnCanvasShown = false;

    private void Awake()
    {
        MenuCanvas.SetActive(false);
        SaveCanvas.SetActive(false);
        RespawnCanvas.SetActive(false);
        Debug.Log("Player Class Name = " + player.GetClassName());
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
        if(player.GetComponent<V2Health>().IsDead() && !bRespawnCanvasShown)
        {
            bRespawnCanvasShown = true;
            ShowRespawnCanvas();
        }
    }

    public void ShowRespawnCanvas()
    {
        RespawnCanvas.SetActive(true);
    }

    public void Respawn()
    {
        RespawnCanvas.SetActive(false);
        bRespawnCanvasShown = false;
        player.GetComponent<V2Health>().Reset();
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
        Debug.Log("Game Saved, ClassName = " + player.GetClassName());
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
