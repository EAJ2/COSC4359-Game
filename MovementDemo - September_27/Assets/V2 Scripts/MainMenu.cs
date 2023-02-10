using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject NoSaveGameCanvas;
    [SerializeField] private GameObject SaveGameCanvas;
    [SerializeField] private GameObject DeleteSaveConfirmationCanvas;
    [SerializeField] private GameObject ClassSelectedCanvas;

    [SerializeField] private bool bIsThereSave;

    [SerializeField] private Stats stats;

    [SerializeField]
    public Text vitText;
    public Text strText;
    public Text endText;
    public Text wisText;
    public Text fortText;
    public Text dexText;
    public Text agilText;

    public string className;


    private void Start()
    {
        NoSaveGameCanvas.SetActive(false);
        SaveGameCanvas.SetActive(false);
        DeleteSaveConfirmationCanvas.SetActive(false);
        ClassSelectedCanvas.SetActive(false);
        stats = GetComponent<Stats>();
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

    public void EnableClassSelected()
    {
        ClassSelectedCanvas.SetActive(true);
        stats.vit = Random.Range(1, 7);
        stats.str = Random.Range(1, 7);
        stats.end = Random.Range(1, 7);
        stats.wis = Random.Range(1, 7);
        stats.fort = Random.Range(1, 7);
        stats.dex = Random.Range(1, 7);
        stats.agil = Random.Range(1, 7);

        vitText.text = stats.vit.ToString();
        strText.text = stats.str.ToString();
        endText.text = stats.end.ToString();
        wisText.text = stats.wis.ToString();
        fortText.text = stats.fort.ToString();
        dexText.text = stats.dex.ToString();
        agilText.text = stats.agil.ToString();
    }

    void DisableClassSelected()
    {
        ClassSelectedCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void SetClassName(string selectedClass)
    {
        className = selectedClass;
    }
}