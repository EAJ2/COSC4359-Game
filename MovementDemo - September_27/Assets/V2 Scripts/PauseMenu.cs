using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool bPaused = false;
    public GameObject MenuCanvas;
    public GameObject SaveCanvas;
    public GameObject ControlsCanvas;
    public GameObject RespawnCanvas;
    public GameObject ShopCanvas;
    public Player player;
    public GameObject playerObject;
    public Stats stats;
    [SerializeField] private string ClassName;

    private bool bRespawnCanvasShown = false;

    public Text goldText;

    private void Awake()
    {
        MenuCanvas.SetActive(false);
        SaveCanvas.SetActive(false);
        RespawnCanvas.SetActive(false);
        Debug.Log("Player Class Name = " + player.GetClassName());

        playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if(ClassName != player.GetClassName())
        {
            gameObject.SetActive(false);
        }
        stats = playerObject.GetComponent<Stats>();
        //goldText.text = "Gold: " + stats.gold;
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (bPaused)
            {
                HideAll();
            }
            else
            {
                ShowShopCanvas();
            }
        }
        
        /*if(player.GetComponent<V2Health>().IsDead() && !bRespawnCanvasShown)
        {
            bRespawnCanvasShown = true;
            ShowRespawnCanvas();
        }*/
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
    public void ShowShopCanvas()
    {
        ShopCanvas.SetActive(true);
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
    public void HideShopCanvas()
    {
        ShopCanvas.SetActive(false);
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
        HideShopCanvas();
        // ControlsCanvas.SetActive(false);
        bPaused = false;
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("V2MainMenu");
        bPaused = false;
        Time.timeScale = 1f;
    }
}
