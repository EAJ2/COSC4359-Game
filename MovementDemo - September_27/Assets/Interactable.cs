using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Transform Player;
    public GameObject interactText;
    public Stats stats;
    public Animator anim;

    public bool isGoldChest;
    public bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactText.active == true && isOpen == false)
        {
            anim.SetBool("Open", true);
            isOpen = true;
            if (isGoldChest == true)
            {
                stats.gold += Random.Range(50, 300);
            }

            interactText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && isOpen == false)
        {
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactText.SetActive(false);
        }
    }

}
