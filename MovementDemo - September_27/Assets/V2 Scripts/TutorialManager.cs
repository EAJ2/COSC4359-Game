using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public GameObject ConfirmTutorialEndCanvas;
    public GameObject StartTutorialCanvas;
    public GameObject ResetTutorialCanvas;
    public GameObject PlayerHUD;

    private bool bTutorialsDone = false;
    private bool bMenuShowing = false;

    //Set Canvas Inactive
    private void Awake()
    {
        ResetTutorialCanvas.SetActive(false);
        ConfirmTutorialEndCanvas.SetActive(false);
        StartTutorialCanvas.SetActive(true);
        player.GetComponent<V2PlayerMovement>().DisableMovement();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ToggleEscape();
        }
    }

    private void ToggleEscape()
    {
        if(bMenuShowing)
        {
            bMenuShowing = false;
            PlayerHUD.SetActive(true);
            ResetTutorialCanvas.SetActive(false);
        }
        else
        {
            bMenuShowing = true;
            PlayerHUD.SetActive(false);
            ResetTutorialCanvas.SetActive(true);
        }
    }

    public void ResetTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }


    public void StartTutorialButton()
    {
        StartTutorialCanvas.SetActive(false);
        player.GetComponent<V2PlayerMovement>().EnableMovement();
    }

    //Set active end tutorial canvas when the Final Collider is triggered and all the tutorials are done
    public void EndTutorial()
    {
    }

    //Go to main menu when canvas is pressed to OK
    public void GoToMainMenu()
    {
        player.SetTutorialDone();
        player.SavePlayer();
        ConfirmTutorialEndCanvas.SetActive(false);
        SceneManager.LoadScene("V2MainMenu");
    }
}
