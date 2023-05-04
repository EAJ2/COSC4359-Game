using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public GameObject Character;

    public Transform Player;
    public GameObject interactText;
    public Stats stats;
    public Animator anim;
    public ConsumableInventory inv;

    public bool isGoldChest;
    public bool isOpen = false;

    public GameObject goldIcon;
    public GameObject hpPotionIcon;
    public GameObject stamPotionIcon;
    public GameObject manaPotionIcon;
    public TextMesh numText;

    public int potionChance;


    private void Start()
    {
        Character = GameObject.FindGameObjectWithTag("Player");
        Player = Character.GetComponent<Transform>();
        stats = Character.GetComponent<Stats>();
        interactText = stats.interactText;
        inv = Character.GetComponent<ConsumableInventory>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactText.active == true && isOpen == false)
        {
            anim.SetBool("Open", true);
            isOpen = true;
            interactText.SetActive(false);
            GiveItem();
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

    public string ChestContents()
    {
        int rand = Random.RandomRange(1, 101);

        if (rand <= potionChance)
        {
            int rand2 = Random.Range(1, 4);
            if (rand2 == 1)
            {
                return "StaminaPotion";
            }
            else if (rand2 == 2)
            {
                return "HealthPotion";
            }
            else
            {
                return "ManaPotion";
            }
        }
        else if (rand > potionChance && rand <= 100)
        {
            return "Gold";
        }
        else
        {
            return null;
        }
    }

    public void GiveItem()
    {
        string item = ChestContents();
        int numPotion = Random.Range(1, 4);
        if (item == "HealthPotion")
        {
            inv.consInv[inv.healthPotion] += numPotion;
            Instantiate(hpPotionIcon, new Vector3(transform.position.x - 0.3f, transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
            numText.text = "x" + numPotion.ToString();
            Instantiate(numText, new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z), Quaternion.identity);
        }
        else if (item == "StaminaPotion")
        {
            inv.consInv[inv.staminaPotion] += numPotion;
            Instantiate(stamPotionIcon, new Vector3(transform.position.x - 0.3f, transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
            numText.text = "x" + numPotion.ToString();
            Instantiate(numText, new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z), Quaternion.identity);
        }
        else if (item == "ManaPotion")
        {
            inv.consInv[inv.manaPotion] += numPotion;
            Instantiate(manaPotionIcon, new Vector3(transform.position.x - 0.3f, transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
            numText.text = "x" + numPotion.ToString();
            Instantiate(numText, new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z), Quaternion.identity);
        }
        else if (item == "Gold")
        {
            int goldNum = Random.Range(50, 200);
            stats.gold += goldNum;
            Instantiate(goldIcon, new Vector3(transform.position.x - 0.3f, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
            numText.text = "x" + goldNum.ToString();
            Instantiate(numText, new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z), Quaternion.identity);
        }
        inv.InventoryCheck();
    }

}
