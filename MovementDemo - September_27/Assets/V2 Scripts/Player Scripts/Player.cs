using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D cc;
    private V2PlayerMovement pm;
    private V2Health health;
    private Stats stats;

    [Header("Add the Players UI here")]
    [SerializeField] private GameObject playerUI;

    [Header("Inventory Stuff (Dont Touch the Variables) (Leave ClassName Blank")]
    [SerializeField] private InventoryV3_Ace inventory;
    public bool bHeadUnlocked;
    public bool bChestUnlocked;
    public bool bLegsUnlocked;
    public bool bShoesUnlocked;
    public bool bWeaponUnlocked;
    public bool bArtifactUnlocked;

    public bool bAbility1Unlocked;
    public bool bAbility2Unlocked;
    public bool bAbility3Unlocked;
    public bool bAbility4Unlocked;

    [Header("Ability Fields")]
    [SerializeField] private ShurikenAbility Shuriken;
    [SerializeField] private ArrowAbility ArrowAb;
    [SerializeField] private HealAbility HealAb;
    private SuperSpeed ss;

    private bool bIsThereSave = false;
    private bool bTutorialDone = false;

    public string ClassName = "";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        pm = GetComponent<V2PlayerMovement>();
        health = GetComponent<V2Health>();
        stats = GetComponent<Stats>();
        ss = GetComponent<SuperSpeed>();

        // SaveClassInformation.DeleteSave(1);

        if (ss == null)
        {
            Debug.Log("Missing the Super Speed Script on the Player!");
        }
        if(HealAb == null)
        {
            Debug.Log("Missing the HealAbility Script on the Player!");
        }
        if (ArrowAb == null)
        {
            Debug.Log("Missing the ArrowAbility Script on the Player!");
        }
        if (Shuriken == null)
        {
            Debug.Log("Missing the Shuriken Ability Script on the Player!");
        }

        if (playerUI == null)
        {
            Debug.Log("You need to add the Player UI gameobject to the Player field in the Player Script!");
        }
        else
        {
            playerUI.SetActive(true);
        }

        LoadPlayer();
    }

    //Save Game
    public void SavePlayer()
    {
        SaveClassInformation.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        V2PlayerData data = SaveClassInformation.LoadPlayer();
        if(data == null)
        {
            Debug.Log("There is no Save for the player, Player.cs");
            bIsThereSave = false;
            bTutorialDone = false;

            bHeadUnlocked = false;
            bChestUnlocked = false;
            bLegsUnlocked = false;
            bShoesUnlocked = false;
            bWeaponUnlocked = false;
            bArtifactUnlocked = false;

            bAbility1Unlocked = false;
            bAbility2Unlocked = false;
            bAbility3Unlocked = false;
            bAbility4Unlocked = false;
            return;
        }
        else
        {
            Debug.Log("There is a Save for the player, Player.cs");

            ClassName = data.ClassName;

            bIsThereSave = true;
            bTutorialDone = true;

            stats.SetVitality(data.VitalityStat);
            stats.SetStrength(data.StrengthStat);
            stats.SetEndurance(data.EnduranceStat);
            stats.SetWisdom(data.WisdomStat);
            stats.SetFortitude(data.FortitudeStat);
            stats.SetDexterity(data.DexterityStat);
            stats.SetAgility(data.AgilityStat);

            health.SetHealth(data.Health);

            if(data.bDash == true)
            {
                pm.EnableDash();
            }

            if(data.bJump == true)
            {
                pm.EnableJump();
            }

            //Gear
            bHeadUnlocked = data.bHeadUnlocked;
            if(bHeadUnlocked)
            {
                UnlockHead();
            }
            bChestUnlocked = data.bChestUnlocked;
            if(bChestUnlocked)
            {
                UnlockChest();
            }
            bLegsUnlocked = data.bLegsUnlocked;
            if(bLegsUnlocked)
            {
                UnlockLegs();
            }
            bShoesUnlocked = data.bShoesUnlocked;
            if(bShoesUnlocked)
            {
                UnlockShoes();
            }
            bWeaponUnlocked = data.bWeaponUnlocked;
            if(bWeaponUnlocked)
            {
                UnlockWeapon();
            }
            bArtifactUnlocked = data.bArtifactUnlocked;
            if(bArtifactUnlocked)
            {
                UnlockArtifact();
            }

            //Abilities
            bAbility1Unlocked = data.bAbility1Unlocked;
            if(bAbility1Unlocked)
            {
                UnlockAbility1();
            }
            bAbility2Unlocked = data.bAbility2Unlocked;
            if (bAbility2Unlocked)
            {
                UnlockAbility2();
            }
            bAbility3Unlocked = data.bAbility3Unlocked;
            if (bAbility3Unlocked)
            {
                UnlockAbility3();
            }
            bAbility4Unlocked = data.bAbility4Unlocked;
            if (bAbility4Unlocked)
            {
                UnlockAbility4();
            }
        }
    }

    public bool GetIsThereSave()
    {
        return bIsThereSave;
    }

    public string GetClassName()
    {
        return ClassName;
    }

    public void SetClassName(string n)
    {
        ClassName = n;
    }

    public bool GetIsTutorialDone()
    {
        return bTutorialDone;
    }

    public void SetTutorialDone()
    {
        bTutorialDone = true;
    }

    //////////////////////Start - Inventory UI Code
    //Unlock from Loading Save
    public void UnlockHead()
    {
        bHeadUnlocked = true;
        inventory.UnlockHead();
    }
    public void UnlockChest()
    {
        bChestUnlocked = true;
        inventory.UnlockChest();
    }
    public void UnlockLegs()
    {
        bLegsUnlocked = true;
        inventory.UnlockLegs();
    }
    public void UnlockShoes()
    {
        bShoesUnlocked = true;
        inventory.UnlockShoes();
    }
    public void UnlockWeapon()
    {
        bWeaponUnlocked = true;
        inventory.UnlockWeapon();
    }
    public void UnlockArtifact()
    {
        bArtifactUnlocked = true;
        inventory.UnlockHead();
    }

    //Functionality
    //Equip Gear
    public void EquipHead()
    {
        if(ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void EquipChest()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void EquipLegs()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void EquipShoes()
    {
        if (ClassName == "Warrior" || ClassName == "Knight")
        {
            Debug.Log("Shoes Equipped");
            pm.WarriorEquipShoes();
        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void EquipWeapon()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void EquipArtifact()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }

    //Unequip Gear
    public void UnequipHead()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void UnequipChest()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void UnequipLegs()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void UnequipShoes()
    {
        if (ClassName == "Warrior")
        {
            pm.WarriorUnequipShoes();
        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void UnequipWeapon()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }
    public void UnequipArtifact()
    {
        if (ClassName == "Warrior")
        {

        }
        else if (ClassName == "Mage")
        {

        }
    }


    //Unlock Ability
    public void UnlockAbility1()
    {
        bAbility1Unlocked = true;
        inventory.UnlockAbility1();
    }

    public void UnlockAbility2()
    {
        bAbility2Unlocked = true;
        inventory.UnlockAbility2();
    }

    public void UnlockAbility3()
    {
        bAbility3Unlocked = true;
        inventory.UnlockAbility3();
    }

    public void UnlockAbility4()
    {
        bAbility4Unlocked = true;
        inventory.UnlockAbility4();
    }

    //Equip Ability
    public void EquipAbility1()
    {
        Shuriken.SetEquipped();
        SetAbility1Status();
    }

    public void EquipAbility2()
    {
        ArrowAb.SetEquipped();
        SetAbility2Status();
    }

    public void EquipAbility3()
    {
        ss.EquipAbility3();
        SetAbility3Status();
    }

    public void EquipAbility4()
    {
        HealAb.Equip();
        SetAbility4Status();
    }

    //UnequipAbility
    public void UnequipAbility1()
    {
        Shuriken.Unequip();
    }

    public void UnequipAbility2()
    {
        ArrowAb.Unequip();
    }

    public void UnequipAbility3()
    {
        ss.UnequipAbility3();
    }

    public void UnequipAbility4()
    {
        HealAb.Unequip();
    }


    //Check if abilities can be used
    public void SetAbility1Status()
    {
        if (Shuriken.IsReady() == true)
        {
            inventory.Ability1Ready();
        }
        else
        {
            inventory.Ability1Used();
        }
    }

    public void SetAbility2Status()
    {
        if(ArrowAb.IsReady() == true)
        {
            inventory.Ability2Ready();
        }
        else
        {
            inventory.Ability2Used();
        }
    }

    public void SetAbility3Status()
    {
        if(ss.IsReady() == true)
        {
            inventory.Ability3Ready();
        }
        else
        {
            inventory.Ability3Used();
        }
    }

    public void SetAbility4Status()
    {
        if(HealAb.IsReady() == true)
        {
            inventory.Ability4Ready();
        }
        else
        {
            inventory.Ability4Used();
        }
    }


    //Functionality of equipping 
    //////////////////////End - Inventory UI Code
}
