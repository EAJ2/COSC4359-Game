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
    [SerializeField] public GameObject ClassSelectedCanvas;

    [SerializeField] public GameObject MageSelectedCanvas;
    [SerializeField] public GameObject KnightSelectedCanvas;
    [SerializeField] public GameObject RangerSelectedCanvas;
    [SerializeField] public GameObject VagabondSelectedCanvas;



    [SerializeField] Player player;

     bool bIsThereSave;

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


        if(player != null)
        {
            if(player.GetIsThereSave() == true)
            {
                bIsThereSave = true;
            }
            else
            {
                bIsThereSave = false;
            }

        }
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
        SaveClassInformation.DeleteSave(1);
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

    public void SetSelectedCanvas(GameObject selectedClass)
    {
        ClassSelectedCanvas = selectedClass;
    }

    public void SetText(Text vit, Text str, Text fort, Text end, Text wis, Text agil, Text dex)
    {
        vitText = vit;
        strText = str;
        fortText = fort;
        endText = end;
        wisText = wis;
        agilText = agil;
        dexText = dex;
    }

    public void SetVitalityText(Text vit)
    {
        vitText = vit;
    }

    public void SetStrengthText(Text text)
    {
        strText = text;
    }

    public void SetWisdomText(Text text)
    {
        wisText = text;
    }

    public void SetFortitudeText(Text text)
    {
        fortText = text;
    }

    public void SetEnduranceText(Text text)
    {
        endText = text;
    }

    public void SetAgilityText(Text text)
    {
        agilText = text;
    }

    public void SetDexterityText(Text text)
    {
        dexText = text;
    }
}