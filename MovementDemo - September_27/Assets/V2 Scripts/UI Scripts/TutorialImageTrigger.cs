using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImageTrigger : MonoBehaviour
{
    [SerializeField] private GameObject BackgroundImage;
    [SerializeField] private GameObject Instruction;

    [SerializeField] private GameObject InventoryBackgrounds;
    [SerializeField] private GameObject Instructions;

    [SerializeField] private GameObject Button;

    [SerializeField] private bool bInventory = false;
    private bool bInventoryOpen = false;
    private bool bInside = false;
    [SerializeField] private bool bEndTutorial = false;

    private void Start()
    {
        BackgroundImage.SetActive(false);
        Instruction.SetActive(false);

        if(Button != null)
        {
            Button.SetActive(false);
        }

        if(InventoryBackgrounds != null)
        {
            InventoryBackgrounds.SetActive(false);
        }

        if(Instructions != null)
        {
            Instructions.SetActive(false);
        }
    }

    private void Update()
    {
        if(bInventory)
        {
            if(Input.GetKeyDown(KeyCode.I) && bInside)
            {
                if (bInventoryOpen == false)
                {
                    bInventoryOpen = true;
                    BackgroundImage.SetActive(false);
                    Instruction.SetActive(false);
                    InventoryBackgrounds.SetActive(true);
                    Instructions.SetActive(true);
                }
                else
                {
                    bInventoryOpen = false;
                    BackgroundImage.SetActive(true);
                    Instruction.SetActive(true);
                    InventoryBackgrounds.SetActive(false);
                    Instructions.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            bInside = true;
            BackgroundImage.SetActive(true);
            Instruction.SetActive(true);
            if(bEndTutorial)
            {
                Button.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bInside = false;
            if(bInventoryOpen)
            {
                InventoryBackgrounds.SetActive(false);
                Instructions.SetActive(false);
                bInventoryOpen = false;
            }
            BackgroundImage.SetActive(false);
            Instruction.SetActive(false);
            if (bEndTutorial)
            {
                Button.SetActive(false);
            }
        }
    }
}
