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

    [Header("Inventory Stuff")]
    [SerializeField] private InventoryV3_Ace inventory;
    public bool bHeadUnlocked;
    public bool bChestUnlocked;
    public bool bLegsUnlocked;
    public bool bShoesUnlocked;
    public bool bWeaponUnlocked;
    public bool bArtifactUnlocked;

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
    public void EquipHead()
    {

    }
    public void EquipChest()
    {

    }
    public void EquipLegs()
    {

    }
    public void EquipShoes()
    {

    }
    public void EquipWeapon()
    {

    }
    public void EquipArtifact()
    {

    }
    public void UnequipHead()
    {

    }
    public void UnequipChest()
    {

    }
    public void UnequipLegs()
    {

    }
    public void UnequipShoes()
    {

    }
    public void UnequipWeapon()
    {

    }
    public void UnequipArtifact()
    {

    }

    //Functionality of equipping 
    //////////////////////End - Inventory UI Code
}
