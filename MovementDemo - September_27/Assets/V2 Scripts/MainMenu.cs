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

    [SerializeField] public GameObject KnightSelectedCanvas;
    [SerializeField] public GameObject RangerSelectedCanvas;
    [SerializeField] public GameObject VagabondSelectedCanvas;



    [SerializeField] Player player;

    bool bIsThereSave;
    bool bTutorialDone;

    [SerializeField] private Stats stats;

    [SerializeField]
    public Text vitText;
    public Text strText;
    public Text endText;
    public Text wisText;
    public Text fortText;
    public Text dexText;
    public Text agilText;
    public Text ClassName;

    public string className;


    private void Awake()
    {
        MainMenuCanvas.SetActive(true);
        NoSaveGameCanvas.SetActive(false);
        SaveGameCanvas.SetActive(false);
        DeleteSaveConfirmationCanvas.SetActive(false);
        ClassSelectedCanvas.SetActive(false);
        KnightSelectedCanvas.SetActive(false);
        RangerSelectedCanvas.SetActive(false);
        VagabondSelectedCanvas.SetActive(false);
        stats = player.GetComponent<Stats>();

        if (player != null)
        {
            if(player.GetIsThereSave())
            {
                bTutorialDone = true;
            }
            else
            {
                bTutorialDone = false;
            }
            /*if(player.GetIsThereSave() == true)
            {
                bIsThereSave = true;
                bTutorialDone = true;
                Debug.Log("Class Name = " + player.GetClassName());
            }
            else
            {
                bIsThereSave = false;
            }
            if(player.GetIsTutorialDone() == true)
            {
                bTutorialDone = true;
                Debug.Log("The tutorial is Done");
            }
            else
            {
                bTutorialDone = false;
                Debug.Log("The tutorial is NOT Done");
            }*/
        }
    }

    private void Start()
    {
        if (player != null)
        {
            className = player.GetClassName();
            Debug.Log("CLass Name from Start In Main Menu = " + className);
        }
    }

    public void PlayButton()
    {
        if(player.GetIsTutorialDone() == false && player.GetIsThereSave() == false)
        {
            StartTutorial();
            return;
        }
        if (player.GetIsTutorialDone() == true && player.GetClassName() != "")
        {
            EnableSaveGame();
            DisableMainMenu();
            LoadSaveClassStats();
        }
        else if (player.GetIsTutorialDone() == true && player.GetClassName() == "")
        {
            DisableMainMenu();
            EnableNoSaveGame();
        }
    }

    public void BackToMainMenuButton()
    {
        DisableAllButMain();
        EnableMainMenu();
        Debug.Log("The Class Name is = " + player.GetClassName());
    }

    public void BackToClassSelectButton()
    {
        DisableClassSelected();
        RangerSelectedCanvas.SetActive(false);
        KnightSelectedCanvas.SetActive(false);
        VagabondSelectedCanvas.SetActive(false);
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
        bTutorialDone = false;
        player.LoadPlayer();
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
        ClassName.text = player.GetClassName();
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

    void DisableClassesSelected()
    {
        KnightSelectedCanvas.SetActive(false);
        RangerSelectedCanvas.SetActive(false);
        VagabondSelectedCanvas.SetActive(false);
    }

    public void KnightSelectedButton()
    {
        DisableNoSaveGame();
        DisableClassesSelected();
        className = "Knight";
        KnightSelectedCanvas.SetActive(true);
    }

    public void RangerSelectedButton()
    {
        DisableNoSaveGame();
        DisableClassesSelected();
        className = "Ranger";
        RangerSelectedCanvas.SetActive(true);
    }

    public void VagabondSelectedButton()
    {
        DisableNoSaveGame();
        DisableClassesSelected();
        className = "Vagabond";
        VagabondSelectedCanvas.SetActive(true);
    }

    public void EnableClassSelected()
    {
       // ClassSelectedCanvas.SetActive(true);
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

    public void LoadSaveClassStats()
    {
        vitText.text = stats.GetVitality().ToString();
        strText.text = stats.GetStrength().ToString();
        endText.text = stats.GetEndurance().ToString();
        wisText.text = stats.GetWisdom().ToString();
        fortText.text = stats.GetFortitude().ToString();
        dexText.text = stats.GetDexterity().ToString();
        agilText.text = stats.GetAgility().ToString();
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
        player.SetClassName(className);
        player.SavePlayer();
        Debug.Log("Class Name Chosen = " + className);
        Debug.Log("Player.GetClassName() = " + player.GetClassName());

        SceneManager.LoadScene("CF_2");
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
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

    public void SetClassNameText(Text text)
    {
        ClassName = text;
    }
}