using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject NoSaveGameCanvas;
    [SerializeField] private GameObject SaveGameCanvas;
    [SerializeField] private GameObject DeleteSaveConfirmationCanvas;
    [SerializeField] private GameObject ClassSelectedCanvas;

    [SerializeField] private Player player;

    private string ClassName;

    private float Intelligence;
    private float Strength;

    private float Dexterity;
    private float Sneaky;

    private bool bIsThereSave;

    private void Start()
    {
        NoSaveGameCanvas.SetActive(false);
        SaveGameCanvas.SetActive(false);
        DeleteSaveConfirmationCanvas.SetActive(false);
        ClassSelectedCanvas.SetActive(false);

        LoadPlayer();
    }

    public void Save()
    {
        player.GetComponent<ClassStats>().SetIntelligence(Intelligence);
        player.GetComponent<ClassStats>().SetStrength(Strength);
        player.GetComponent<ClassStats>().SetDexterity(Dexterity);
        player.GetComponent<ClassStats>().SetSneaky(Sneaky);

        player.GetComponent<V2Health>().SetMaxHealth();

        SaveClassInformation.SavePlayer(player);
    }

    public void LoadPlayer()
    {
        V2PlayerData data = SaveClassInformation.LoadPlayer();
        if(data == null)
        {
            Debug.Log("There was no save");
            bIsThereSave = false;
        }
        else
        {
            Debug.Log("There is a save");
            bIsThereSave = true;
        }
    }

    public void DeleteSave()
    {
        SaveClassInformation.DeleteSave(1);
    }

    public void PlayButton()
    {
        if(bIsThereSave == true)
        {
            DisableMainMenu();
            EnableSaveGame();
        }
        else if (bIsThereSave == false)
        {
            DisableMainMenu();
            EnableNoSaveGame();
        }
    }

    public void BackToMainMenuButton()
    {
        DisableAllButMain();
        EnableMainMenu();
    }

    public void BackToClassSelectButton()
    {
        DisableClassSelected();
        EnableNoSaveGame();
    }

    public void DeleteSaveConfirmationButton()
    {
        DisableSaveGame();
        EnableDeleteSaveConfirmation();
    }

    public void DoDeleteButton()
    {
        DeleteSave();
        DisableDeleteSaveConfirmation();
        EnableMainMenu();
        bIsThereSave = false;
    }

    public void DontDeleteButton()
    {
        DisableDeleteSaveConfirmation();
        EnableSaveGame();
    }

    public void GoToClassSelectedButton()
    {
        DisableNoSaveGame();
        EnableClassSelected();
    }

    void DisableAllButMain()
    {
        DisableClassSelected();
        DisableDeleteSaveConfirmation();
        DisableNoSaveGame();
        DisableSaveGame();
    }

    void EnableMainMenu()
    {
        MainMenuCanvas.SetActive(true);
    }

    void DisableMainMenu()
    {
        MainMenuCanvas.SetActive(false);
    }

    void EnableSaveGame()
    {
        SaveGameCanvas.SetActive(true);
    }

    void DisableSaveGame()
    {
        SaveGameCanvas.SetActive(false);
    }

    void EnableNoSaveGame()
    {
        NoSaveGameCanvas.SetActive(true);
    }

    void DisableNoSaveGame()
    {
        NoSaveGameCanvas.SetActive(false);
    }

    void EnableDeleteSaveConfirmation()
    {
        DeleteSaveConfirmationCanvas.SetActive(true);
    }

    void DisableDeleteSaveConfirmation()
    {
        DeleteSaveConfirmationCanvas.SetActive(false);
    }

    void EnableClassSelected()
    {
        ClassSelectedCanvas.SetActive(true);
    }

    void DisableClassSelected()
    {
        ClassSelectedCanvas.SetActive(false);
    }

    public void DoExitGame()
    {
        Application.Quit();
    }
}
