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

    private int TutorialsDone = 0;

    private bool bTutorialsDone = false;

    //Set Canvas Inactive
    private void Awake()
    {
        ConfirmTutorialEndCanvas.SetActive(false);
    }

    private void Update()
    {
        if(bTutorialsDone)
        {
            EndTutorial();
        }
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
