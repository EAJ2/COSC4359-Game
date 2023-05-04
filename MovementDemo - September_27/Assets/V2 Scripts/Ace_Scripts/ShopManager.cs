using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject ShopCanvas;
    [SerializeField] private Player player;
    [SerializeField] private string ClassName;

    [Header("Item Costs")]
    [SerializeField] private int HealthPotion;
    [SerializeField] private int ManaPotion;
    [SerializeField] private int StaminaPotion;
    [SerializeField] private int Ability1;
    [SerializeField] private int Ability2;
    [SerializeField] private int Ability3;
    [SerializeField] private int Ability4;

    private int NewGold;

    private bool bOpen = false;

    private void Start()
    {
        ShopCanvas.SetActive(false);
        if(player == null)
        {
            Debug.Log("Player missing in the Shop Manager");
        }

        if(ClassName != player.GetClassName())
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleCanvas();
        }
    }

    private void ToggleCanvas()
    {
        if(bOpen)
        {
            bOpen = false;
            ShopCanvas.SetActive(false);
        }
        else
        {
            bOpen = true;
            ShopCanvas.SetActive(true);
        }
    }

    public void BuyHealthPotion()
    {
        if(player.Gold >= HealthPotion)
        {
            NewGold = player.Gold - HealthPotion;
            player.GetComponent<Stats>().SetOriginalGold(NewGold);
            player.GetComponent<ConsumableInventory>().buyHealthPotion();
        }
    }

    public void BuyManaPotion()
    {
        if (player.Gold >= ManaPotion)
        {
            NewGold = player.Gold - ManaPotion;
            player.GetComponent<Stats>().SetOriginalGold(NewGold);
            player.GetComponent<ConsumableInventory>().buyManaPotion();
        }
    }

    public void BuyStaminaPotion()
    {
        if (player.Gold >= StaminaPotion)
        {
            NewGold = player.Gold - StaminaPotion;
            player.GetComponent<Stats>().SetOriginalGold(NewGold);
            player.GetComponent<ConsumableInventory>().buyStaminaPotion();
        }
    }

    public void BuyAbility1()
    {
        if (player.Gold >= Ability1)
        {
            NewGold = player.Gold - Ability1;
            player.GetComponent<Stats>().SetOriginalGold(NewGold);
            player.UnlockAbility1();
        }
    }

    public void BuyAbility2()
    {
        if (player.Gold >= Ability1)
        {
            NewGold = player.Gold - Ability2;
            player.GetComponent<Stats>().SetOriginalGold(NewGold);
            player.UnlockAbility2();
        }
    }

    public void BuyAbility3()
    {
        if (player.Gold >= Ability1)
        {
            NewGold = player.Gold - Ability3;
            player.GetComponent<Stats>().SetOriginalGold(NewGold);
            player.UnlockAbility3();
        }
    }

    public void BuyAbility4()
    {
        if (player.Gold >= Ability1)
        {
            NewGold = player.Gold - Ability4;
            player.GetComponent<Stats>().SetOriginalGold(NewGold);
            player.UnlockAbility4();
        }
    }
}
