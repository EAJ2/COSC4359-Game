using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImage : MonoBehaviour
{
    [SerializeField] private GameObject Image;
    [SerializeField] private GameObject StartCollider;
    [SerializeField] private GameObject EndCollider;
    private bool bStartEntered = false;
    private bool bEndEntered = false;

    [Header("Tutorial Scene")]
    [SerializeField] private bool bTutorialScene;
    [SerializeField] private TutorialManager TM;


    private void Awake()
    {
        Image.SetActive(false);
    }

    private void Update()
    {
        if (StartCollider.GetComponent<NotifyTriggerCollision>().IsTriggered() == true && bStartEntered == false)
        {
            bStartEntered = true;
            StartCollider.SetActive(false);
            Image.SetActive(true);
        }
        if(EndCollider.GetComponent<NotifyTriggerCollision>().IsTriggered() == true && bEndEntered == false)
        {
            bEndEntered = true;
            EndCollider.SetActive(false);
            Image.SetActive(false);

            if (bTutorialScene == true)
            {
                TM.IncrTutorialsDone();
            }
            gameObject.SetActive(false);
        }
    }
}
