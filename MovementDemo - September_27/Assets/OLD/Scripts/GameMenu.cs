using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject PlayButton;
    public GameObject QuitButton;
    public GameObject SaveButton;
    public GameObject YesButton;
    public GameObject NoButton;
    public GameObject SaveGameQuestionText;
    public GameObject ControlsInGameCanvas;
    public GameObject ControlsImage;
    public GameObject ControlsButton;
    public GameObject BackFromControlsButton;
    private bool bShowingGameCanvas = false;
    private bool bShowingSaveButtons = false;


    private void Start()
    {
        menu.SetActive(false);
        HideSaveButtons();
        ControlsInGameCanvas.SetActive(false);
        ControlsImage.SetActive(false);
        BackFromControlsButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCanvas();
        }
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            ControlsInGameCanvas.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            ControlsInGameCanvas.SetActive(false);
        }
    }

    public void QuitToMainMenu(string SceneNameToLoad)
    {
        SceneManager.LoadScene(SceneNameToLoad);
    }

    public void ToggleCanvas()
    {
        if (bShowingGameCanvas)
        {
            if(bShowingSaveButtons)
            {
                bShowingGameCanvas = false;
                menu.SetActive(false);
                GoBackToGameMenu();
            }
            else
            {
                bShowingGameCanvas = false;
                menu.SetActive(false);
            }
        }
        else if (!bShowingGameCanvas)
        {
            if(bShowingSaveButtons)
            {
                bShowingGameCanvas = true;
                menu.SetActive(true);
                GoBackToGameMenu();
            }
            else
            {
                bShowingGameCanvas = true;
                menu.SetActive(true);
            }
        }
    }

    public void GoBackToGameMenu()
    {
        HideSaveButtons();
        ShowMainButtons();
        ControlsImage.SetActive(false);
        BackFromControlsButton.SetActive(false);
        bShowingSaveButtons = false;
    }

    public void GoToSaveGameOptions()
    {
        HideMainButtons();
        ShowSaveButtons();
        bShowingSaveButtons = true;
    }

    public void GoToControlsOption()
    {
        HideMainButtons();
        BackFromControlsButton.SetActive(true);
        ControlsImage.SetActive(true);
    }

    private void ShowMainButtons()
    {
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
        SaveButton.SetActive(true);
        ControlsButton.SetActive(true);
    }

    private void HideMainButtons()
    {
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        SaveButton.SetActive(false);
        ControlsButton.SetActive(false);
    }

    private void ShowSaveButtons()
    {
        YesButton.SetActive(true);
        NoButton.SetActive(true);
        SaveGameQuestionText.SetActive(true);
    }

    private void HideSaveButtons()
    {
        YesButton.SetActive(false);
        NoButton.SetActive(false);
        SaveGameQuestionText.SetActive(false);
    }
}
