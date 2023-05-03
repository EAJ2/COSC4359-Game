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
    private RangerCombat rc;
    private ConsumableInventory inv;

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

    [Header("This must be set to False")]
    [SerializeField] private bool bDeleteSave = false;

    [SerializeField] private bool bDemo = false;
    [SerializeField] private string DemoClass;

    [SerializeField] private bool bInTutorial = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        pm = GetComponent<V2PlayerMovement>();
        health = GetComponent<V2Health>();
        stats = GetComponent<Stats>();
        ss = GetComponent<SuperSpeed>();
        rc = GetComponent<RangerCombat>();
        inv = GetComponent<ConsumableInventory>();

        if (bDeleteSave)
        {
            SaveClassInformation.DeleteSave(1);
        }

        if (ss == null)
        {
            Debug.Log("Missing the Super Speed Script on the Player!");
        }
        if(HealAb == null)
        {
            Debug.Log("Missing the Heal Ability Script on the Player!");
        }
        if (ArrowAb == null)
        {
            Debug.Log("Missing the Arrow Ability Script on the Player!");
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
        if(bInTutorial)
        {
            ClassName = "Vagabond";
        }
        if (ClassName != stats.Class && !bInTutorial)
        {
            this.gameObject.SetActive(false);
        }

        if (bDemo)
        {
            ClassName = DemoClass;
        }

        inventory.SetClassName(ClassName);
    }

    //Save Game
    public void SavePlayer()
    {
        if(bInTutorial)
        {
            ClassName = "";
        }
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
        if(ClassName == "Vagabond" || ClassName == "Knight")
        {
            pm.HeadEquip();
        }
        else if (ClassName == "Ranger")
        {
            pm.HeadEquip();
        }
    }
    public void EquipChest()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            health.WarriorChestEquip();
        }
        else if (ClassName == "Ranger")
        {
            pm.RangerEquipChest();
        }
    }
    public void EquipLegs()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            pm.EnableDoubleJump();
        }
        else if (ClassName == "Ranger")
        {
            pm.EnableDoubleJump();
        }
    }
    public void EquipShoes()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            pm.WarriorEquipShoes();
        }
        else if (ClassName == "Ranger")
        {
            pm.RangerEquipShoes();
        }
    }
    public void EquipWeapon()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            stats.EquipWeapon();
        }
        else if (ClassName == "Ranger")
        {
            rc.EquipWeapon();
        }
    }
    public void EquipArtifact()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            stats.EquipKnightArtifact();
        }
        else if (ClassName == "Ranger")
        {
            stats.EquipRangerArtifact();
        }
    }

    //Unequip Gear
    public void UnequipHead()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            pm.HeadUnequip();
        }
        else if (ClassName == "Ranger")
        {
            pm.HeadUnequip();
        }
    }
    public void UnequipChest()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            health.WarriorChestUnequip();
        }
        else if (ClassName == "Ranger")
        {
            pm.RangerUnequipChest();
        }
    }
    public void UnequipLegs()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            pm.DisableDoubleJump();
        }
        else if (ClassName == "Ranger")
        {
            pm.DisableDoubleJump();
        }
    }
    public void UnequipShoes()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            pm.WarriorUnequipShoes();
        }
        else if (ClassName == "Ranger")
        {
            pm.RangerUnequipShoe();
        }
    }
    public void UnequipWeapon()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            stats.UnequipWeapon();
        }
        else if (ClassName == "Ranger")
        {
            rc.UnquipWeapon();
        }
    }
    public void UnequipArtifact()
    {
        if (ClassName == "Vagabond" || ClassName == "Knight")
        {
            stats.UnequipKnightArtifact();
        }
        else if (ClassName == "Ranger")
        {
            stats.UnequipRangerArtifact();
        }
    }


    //Unlock Ability
    public void UnlockAbility1()
    {

        if (stats.GetGold() > 250 && bAbility1Unlocked == false)
        {
            bAbility1Unlocked = true;
            inventory.UnlockAbility1();
            stats.RemoveGold(250);
        }
        
    }

    public void UnlockAbility2()
    {
        if (stats.GetGold() > 250 && bAbility2Unlocked == false)
        {
            bAbility2Unlocked = true;
            inventory.UnlockAbility2();
            stats.RemoveGold(250);
        }
    }

    public void UnlockAbility3()
    {
        if (stats.GetGold() > 250 && bAbility3Unlocked == false)
        {
            bAbility3Unlocked = true;
            inventory.UnlockAbility3();
            stats.RemoveGold(250);
        }

    }

    public void UnlockAbility4()
    {
        if (stats.GetGold() > 250 && bAbility4Unlocked == false)
        {
            bAbility4Unlocked = true;
            inventory.UnlockAbility4();
            stats.RemoveGold(250);
        }

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


    //Key functionality
    public bool CanEnterBossRoom()
    {
        return inventory.CanEnterBossRoom();
    }

    public void UnlockKey1()
    {
        inventory.UnlockKey1();
    }

    public void UnlockKey2()
    {
        inventory.UnlockKey2();
    }

    public void UnlockKey3()
    {
        inventory.UnlockKey3();
    }


    //Functionality of equipping 
    //////////////////////End - Inventory UI Code

    //////////////////////
    // Shop functionality
    public void addHealthPotion()
    {
        if (stats.GetGold() > 50)
        {
            inv.buyHealthPotion();
            stats.RemoveGold(50);
        }
    }
    public void addManaPotion()
    {
        if (stats.GetGold() > 50)
        {
            inv.buyManaPotion();
            stats.RemoveGold(50);
        }
    }
    public void addStaminaPotion()
    {
        if (stats.GetGold() > 50)
        {
            inv.buyStaminaPotion();
            stats.RemoveGold(50);
        }

    }
}
