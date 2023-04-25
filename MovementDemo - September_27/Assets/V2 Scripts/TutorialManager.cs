using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<TutorialImage> TutorialImages;
    public NotifyTriggerCollision NTC;
    [SerializeField] private Player player;
    public GameObject ConfirmTutorialEndCanvas;
    public GameObject StartTutorialCanvas;
    public GameObject ResetTutorialCanvas;
    public GameObject PlayerHUD;

    private int TutorialsDone = 0;

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
        if (Input.GetKey(KeyCode.Tilde))
        {
            Debug.Log("Escape is pressed!");
            ToggleEscape();
        }
        if (bTutorialsDone)
        {
            EndTutorial();
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

    //Increment the var TutorialsDone to equal the amount of tutorials on the manager
    public void IncrTutorialsDone()
    {
        TutorialsDone++;
        if(TutorialsDone == TutorialImages.Count)
        {
            bTutorialsDone = true;
        }
    }

    public void StartTutorialButton()
    {
        StartTutorialCanvas.SetActive(false);
        player.GetComponent<V2PlayerMovement>().EnableMovement();
    }

    //Set active end tutorial canvas when the Final Collider is triggered and all the tutorials are done
    public void EndTutorial()
    {
        if(NTC.IsTriggered() == true)
        {
            player.GetComponentInChildren<V2PlayerMovement>().DisableMovement();
            ConfirmTutorialEndCanvas.SetActive(true);
        }
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
