using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartOfGameMenu : MonoBehaviour
{
    private Animator anim;
    private string NewGameTravel = "BeginningOfGame";
    private string CurrentGameTravel = "SampleScene";
    private float currentHealth;

    public GameObject timeText1;
    public GameObject SaveButton1NoSave;
    public GameObject SaveButton1WithSave;
    public GameObject DeleteButton1;
    public GameObject BackgroundImage;

    public GameObject BackButton;
    public GameObject NamesImage;
    public GameObject OptionsButton;
    public GameObject CreditsButton;

    public GameObject PlayButton;
    public GameObject QuitButton;

    private bool bMainMenuShowing = true;
    private bool bSaveMenuShowing = false;
    private bool bSaveOneEmpty;

    public GameObject MainMenuCanvas;
    public GameObject SaveMenuCanvas;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        BackgroundImage.SetActive(false);
        BackButton.SetActive(false);
        NamesImage.SetActive(false);
    }

    private void Start()
    {
        SaveMenuCanvas.SetActive(false);
        LoadPlayer();
        SetFirstSave();
    }

    public void NewTravel()
    {
        SceneManager.LoadScene(NewGameTravel);
    }

    public void GameTravel()
    {
        SceneManager.LoadScene(CurrentGameTravel);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data == null)
        {
            Debug.Log("There was nothing to load at the start of the game");
            bSaveOneEmpty = true;
            return;
        }
        currentHealth = data.health;
        //Vector3 position;
        //position.x = data.position[0];
        //position.y = data.position[1];
        //position.z = data.position[2];
        string timePlayed = data.timePlayed;
        timeText1.GetComponent<Text>().text = timePlayed;

    }

    public void ToggleMainMenu()
    {
        if(bMainMenuShowing)
        {
            bMainMenuShowing = false;
            MainMenuCanvas.SetActive(false);
            SaveMenuCanvas.SetActive(true);
            bSaveMenuShowing = true;
        }
        else if(bSaveMenuShowing)
        {
            bSaveMenuShowing = false;
            SaveMenuCanvas.SetActive(false);
            MainMenuCanvas.SetActive(true);
            bMainMenuShowing = true;
        }
    }

    public void SetFirstSave()
    {
        if(bSaveOneEmpty)
        {
            SaveButton1WithSave.SetActive(false);
            SaveButton1NoSave.SetActive(true);
            DeleteButton1.SetActive(false);
        }
        else
        {
            SaveButton1NoSave.SetActive(false);
            SaveButton1WithSave.SetActive(true);
            DeleteButton1.SetActive(true);
        }
    }

    public void DeleteFirstSave()
    {
        SaveSystem.DeleteSave(1);
        bSaveOneEmpty = true;
        SetFirstSave();
    }

    public void ShowCredits()
    {
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        CreditsButton.SetActive(false);
        OptionsButton.SetActive(false);
        NamesImage.SetActive(true);
        BackButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
        CreditsButton.SetActive(true);
        OptionsButton.SetActive(true);
        NamesImage.SetActive(false);
        BackButton.SetActive(false);
    }

    public void ShowOptions()
    {
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        CreditsButton.SetActive(false);
        OptionsButton.SetActive(false);
        BackButton.SetActive(true);
    }
}
