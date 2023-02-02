using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject NoSaveGameCanvas;
    [SerializeField] private GameObject SaveGameCanvas;
    [SerializeField] private GameObject DeleteSaveConfirmationCanvas;
    [SerializeField] private GameObject ClassSelectedCanvas;

    [SerializeField] private bool bIsThereSave;

    private void Start()
    {
        NoSaveGameCanvas.SetActive(false);
        SaveGameCanvas.SetActive(false);
        DeleteSaveConfirmationCanvas.SetActive(false);
        ClassSelectedCanvas.SetActive(false);
    }

    public void PlayButton()
    {
        if (bIsThereSave == true)
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

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
