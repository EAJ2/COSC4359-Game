using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV3_Ace : MonoBehaviour
{
    [SerializeField] private string InventoryClassName;

    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject CooldownCanvas;
    [SerializeField] public Player player;
    [SerializeField] public GameObject playerUI;

    [Header("Warrior Border Buttons")]
    [SerializeField] private GameObject Warrior_HeadBB;
    [SerializeField] private GameObject Warrior_ChestBB;
    [SerializeField] private GameObject Warrior_LegsBB;
    [SerializeField] private GameObject Warrior_ShoesBB;
    [SerializeField] private GameObject Warrior_WeaponBB;
    [SerializeField] private GameObject Warrior_ArtifactBB;

    [Header("Mage Border Buttons")]
    [SerializeField] private GameObject Mage_HeadBB;
    [SerializeField] private GameObject Mage_ChestBB;
    [SerializeField] private GameObject Mage_LegsBB;
    [SerializeField] private GameObject Mage_ShoesBB;
    [SerializeField] private GameObject Mage_WeaponBB;
    [SerializeField] private GameObject Mage_ArtifactBB;

    [Header("Inventory Items Locked")]
    [SerializeField] private GameObject HeadLocked;
    [SerializeField] private GameObject ChestLocked;
    [SerializeField] private GameObject LegsLocked;
    [SerializeField] private GameObject ShoesLocked;
    [SerializeField] private GameObject WeaponLocked;
    [SerializeField] private GameObject ArtifactLocked;

    [Header("Warrior Inventory Items Unlocked")]
    [SerializeField] private GameObject Warrior_HeadUnlockedButton;
    [SerializeField] private GameObject Warrior_ChestUnlockedButton;
    [SerializeField] private GameObject Warrior_LegsUnlockedButton;
    [SerializeField] private GameObject Warrior_ShoesUnlockedButton;
    [SerializeField] private GameObject Warrior_WeaponUnlockedButton;
    [SerializeField] private GameObject Warrior_ArtifactUnlockedButton;

    [Header("Mage Inventory Items Unlocked")]
    [SerializeField] private GameObject Mage_HeadUnlockedButton;
    [SerializeField] private GameObject Mage_ChestUnlockedButton;
    [SerializeField] private GameObject Mage_LegsUnlockedButton;
    [SerializeField] private GameObject Mage_ShoesUnlockedButton;
    [SerializeField] private GameObject Mage_WeaponUnlockedButton;
    [SerializeField] private GameObject Mage_ArtifactUnlockedButton;

    [Header("Inventory Items Equipped")]
    [SerializeField] private GameObject HeadEquipped;
    [SerializeField] private GameObject ChestEquipped;
    [SerializeField] private GameObject LegsEquipped;
    [SerializeField] private GameObject ShoesEquipped;
    [SerializeField] private GameObject WeaponEquipped;
    [SerializeField] private GameObject ArtifactEquipped;


    [Header("Abilities Equipped Border")]
    [SerializeField] private GameObject Ability1Equipped;
    [SerializeField] private GameObject Ability2Equipped;
    [SerializeField] private GameObject Ability3Equipped;
    [SerializeField] private GameObject Ability4Equipped;

    [Header("Abilities Locked Image")]
    [SerializeField] private GameObject Ability1Locked;
    [SerializeField] private GameObject Ability2Locked;
    [SerializeField] private GameObject Ability3Locked;
    [SerializeField] private GameObject Ability4Locked;

    [Header("Abilities Buttons")]
    [SerializeField] private GameObject Ability1Button;
    [SerializeField] private GameObject Ability2Button;
    [SerializeField] private GameObject Ability3Button;
    [SerializeField] private GameObject Ability4Button;

    [Header("Ready Abilities")]
    [SerializeField] private GameObject Ability1_Ready;
    [SerializeField] private GameObject Ability2_Ready;
    [SerializeField] private GameObject Ability3_Ready;
    [SerializeField] private GameObject Ability4_Ready;

    [Header("Cooldown Abilities")]
    [SerializeField] private GameObject Ability1_Used;
    [SerializeField] private GameObject Ability2_Used;
    [SerializeField] private GameObject Ability3_Used;
    [SerializeField] private GameObject Ability4_Used;

    [Header("Keys Images")]
    [SerializeField] private GameObject K1_Image;
    [SerializeField] private GameObject K2_Image;
    [SerializeField] private GameObject K3_Image;

    [Header("Keys Locked")]
    [SerializeField] private GameObject K1_Locked;
    [SerializeField] private GameObject K2_Locked;
    [SerializeField] private GameObject K3_Locked;


    private bool bCanvasActive = false;

    private bool bHeadEquipped = false;
    private bool bChestEquipped = false;
    private bool bLegsEquipped = false;
    private bool bShoesEquipped = false;
    private bool bWeaponEquipped = false;
    private bool bArtifactEquipped = false;

    private bool bAbility1Equipped = false;
    private bool bAbility2Equipped = false;
    private bool bAbility3Equipped = false;
    private bool bAbility4Equipped = false;

    [SerializeField] public bool bDemo = false;

    [Header("Leave Blank")]
    [SerializeField] private string ClassName;
    [SerializeField] private bool bRanger = false;
    [SerializeField] private bool bKnight = false;
    [SerializeField] private bool bVagabond = false;


    [SerializeField] private bool bTutorial = false;

    private int NumberOfKeys = 0;



    private void Awake()
    {
        Canvas.SetActive(false);

        if(bTutorial == true)
        {
            K1_Locked.SetActive(true);
            K2_Locked.SetActive(true);
            K3_Locked.SetActive(true);

            Ability1Locked.SetActive(true);
            Ability2Locked.SetActive(true);
            Ability3Locked.SetActive(true);
            Ability4Locked.SetActive(true);

            HeadLocked.SetActive(true);
            ChestLocked.SetActive(true);
            LegsLocked.SetActive(true);
            ShoesLocked.SetActive(true);
            WeaponLocked.SetActive(true);
            ArtifactLocked.SetActive(true);
        }
        else
        {
            //Ability Cooldown Canvas SetActive(false)
            Ability1_Ready.SetActive(false);
            Ability2_Ready.SetActive(false);
            Ability3_Ready.SetActive(false);
            Ability4_Ready.SetActive(false);
            Ability1_Used.SetActive(false);
            Ability2_Used.SetActive(false);
            Ability3_Used.SetActive(false);
            Ability4_Used.SetActive(false);
            CooldownCanvas.SetActive(false);

            ClassName = player.GetClassName();
            if (player == null)
            {
                Debug.Log("From InventoryV3_Ace, Add the Players UI to the Gamemanage UI");
            }

            if(InventoryClassName != player.GetClassName())
            {
                Debug.Log("InventoryClassName = " + InventoryClassName);
                Debug.Log("Inventory playerClassName = " + player.GetClassName());
                gameObject.SetActive(false);
            }

            if (bDemo == false)
            {
                //Gear
                Warrior_HeadBB.SetActive(false);
                Warrior_ChestBB.SetActive(false);
                Warrior_LegsBB.SetActive(false);
                Warrior_ShoesBB.SetActive(false);
                Warrior_WeaponBB.SetActive(false);
                Warrior_ArtifactBB.SetActive(false);

                Mage_HeadBB.SetActive(false);
                Mage_ChestBB.SetActive(false);
                Mage_LegsBB.SetActive(false);
                Mage_ShoesBB.SetActive(false);
                Mage_WeaponBB.SetActive(false);
                Mage_ArtifactBB.SetActive(false);

                Warrior_HeadUnlockedButton.SetActive(false);
                Warrior_ChestUnlockedButton.SetActive(false);
                Warrior_LegsUnlockedButton.SetActive(false);
                Warrior_ShoesUnlockedButton.SetActive(false);
                Warrior_WeaponUnlockedButton.SetActive(false);
                Warrior_ArtifactUnlockedButton.SetActive(false);

                Mage_HeadUnlockedButton.SetActive(false);
                Mage_ChestUnlockedButton.SetActive(false);
                Mage_LegsUnlockedButton.SetActive(false);
                Mage_ShoesUnlockedButton.SetActive(false);
                Mage_WeaponUnlockedButton.SetActive(false);
                Mage_ArtifactUnlockedButton.SetActive(false);

                HeadEquipped.SetActive(false);
                ChestEquipped.SetActive(false);
                LegsEquipped.SetActive(false);
                ShoesEquipped.SetActive(false);
                WeaponEquipped.SetActive(false);
                ArtifactEquipped.SetActive(false);

                //Abilities
                Ability1Button.SetActive(false);
                Ability2Button.SetActive(false);
                Ability3Button.SetActive(false);
                Ability4Button.SetActive(false);

                Ability1Equipped.SetActive(false);
                Ability2Equipped.SetActive(false);
                Ability3Equipped.SetActive(false);
                Ability4Equipped.SetActive(false);

                //Keys
                K1_Locked.SetActive(true);
                K2_Locked.SetActive(true);
                K3_Locked.SetActive(true);

                K1_Image.SetActive(false);
                K2_Image.SetActive(false);
                K3_Image.SetActive(false);
            }
            else
            {
                NumberOfKeys = 3;
                if(bRanger)
                {
                    ClassName = "Ranger";
                }
                if(bKnight)
                {
                    ClassName = "Knight";
                }
                if(bVagabond)
                {
                    ClassName = "Vagabond";
                }
                //Gear
                HeadLocked.SetActive(false);
                ChestLocked.SetActive(false);
                LegsLocked.SetActive(false);
                ShoesLocked.SetActive(false);
                WeaponLocked.SetActive(false);
                ArtifactLocked.SetActive(false);

                HeadEquipped.SetActive(false);
                ChestEquipped.SetActive(false);
                LegsEquipped.SetActive(false);
                ShoesEquipped.SetActive(false);
                WeaponEquipped.SetActive(false);
                ArtifactEquipped.SetActive(false);

                Warrior_HeadBB.SetActive(false);
                Warrior_ChestBB.SetActive(false);
                Warrior_LegsBB.SetActive(false);
                Warrior_ShoesBB.SetActive(false);
                Warrior_WeaponBB.SetActive(false);
                Warrior_ArtifactBB.SetActive(false);

                Mage_HeadBB.SetActive(false);
                Mage_ChestBB.SetActive(false);
                Mage_LegsBB.SetActive(false);
                Mage_ShoesBB.SetActive(false);
                Mage_WeaponBB.SetActive(false);
                Mage_ArtifactBB.SetActive(false);

                Mage_HeadUnlockedButton.SetActive(false);
                Mage_ChestUnlockedButton.SetActive(false);
                Mage_LegsUnlockedButton.SetActive(false);
                Mage_ShoesUnlockedButton.SetActive(false);
                Mage_WeaponUnlockedButton.SetActive(false);
                Mage_ArtifactUnlockedButton.SetActive(false);

                Warrior_HeadUnlockedButton.SetActive(false);
                Warrior_ChestUnlockedButton.SetActive(false);
                Warrior_LegsUnlockedButton.SetActive(false);
                Warrior_ShoesUnlockedButton.SetActive(false);
                Warrior_WeaponUnlockedButton.SetActive(false);
                Warrior_ArtifactUnlockedButton.SetActive(false);

                if (ClassName == "Knight" || ClassName == "Vagabond")
                {
                    Warrior_HeadUnlockedButton.SetActive(true);
                    Warrior_ChestUnlockedButton.SetActive(true);
                    Warrior_LegsUnlockedButton.SetActive(true);
                    Warrior_ShoesUnlockedButton.SetActive(true);
                    Warrior_WeaponUnlockedButton.SetActive(true);
                    Warrior_ArtifactUnlockedButton.SetActive(true);
                }
                else if (ClassName == "Ranger")
                {
                    Mage_HeadUnlockedButton.SetActive(true);
                    Mage_ChestUnlockedButton.SetActive(true);
                    Mage_LegsUnlockedButton.SetActive(true);
                    Mage_ShoesUnlockedButton.SetActive(true);
                    Mage_WeaponUnlockedButton.SetActive(true);
                    Mage_ArtifactUnlockedButton.SetActive(true);
                }

                //Abilities
                //Abilities
                Ability1Button.SetActive(true);
                Ability2Button.SetActive(true);
                Ability3Button.SetActive(true);
                Ability4Button.SetActive(true);

                Ability1Equipped.SetActive(false);
                Ability2Equipped.SetActive(false);
                Ability3Equipped.SetActive(false);
                Ability4Equipped.SetActive(false);

                Ability1Locked.SetActive(false);
                Ability2Locked.SetActive(false);
                Ability3Locked.SetActive(false);
                Ability4Locked.SetActive(false);

                //Keys
                K1_Locked.SetActive(false);
                K2_Locked.SetActive(false);
                K3_Locked.SetActive(false);

                K1_Image.SetActive(true);
                K2_Image.SetActive(true);
                K3_Image.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleCanvas();
        }
    }

    private void ToggleCanvas()
    {
        if(bCanvasActive == false)
        {
            bCanvasActive = true;
            Canvas.SetActive(true);
            CooldownCanvas.SetActive(false);
            playerUI.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            bCanvasActive = false;
            Canvas.SetActive(false);
            CooldownCanvas.SetActive(true);
            playerUI.SetActive(true);
            Time.timeScale = 1;
        }
    }

    //Unlock Gear
    public void UnlockHead()
    {
        HeadLocked.SetActive(false);
        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_HeadUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_HeadUnlockedButton.SetActive(true);
        }
    }
    public void UnlockChest()
    {
        ChestLocked.SetActive(false);
        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ChestUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ChestUnlockedButton.SetActive(true);
        }
    }
    public void UnlockLegs()
    {
        LegsLocked.SetActive(false);
        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_LegsUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_LegsUnlockedButton.SetActive(true);
        }
    }
    public void UnlockShoes()
    {
        ShoesLocked.SetActive(false);
        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ShoesUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ShoesUnlockedButton.SetActive(true);
        }
    }
    public void UnlockWeapon()
    {
        WeaponLocked.SetActive(false);
        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_WeaponUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_WeaponUnlockedButton.SetActive(true);
        }
    }
    public void UnlockArtifact()
    {
        ArtifactLocked.SetActive(false);
        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ArtifactUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ArtifactUnlockedButton.SetActive(true);
        }
    }

    //Equip Gear
    public void EquipHead()
    {
        bHeadEquipped = true;
        HeadEquipped.SetActive(true);

        if(ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_HeadUnlockedButton.SetActive(false);
            Warrior_HeadUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_HeadBB.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_HeadUnlockedButton.SetActive(false);
            Mage_HeadUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_HeadBB.SetActive(true);
        }

        player.EquipHead();
    }
    public void EquipChest()
    {
        bChestEquipped = true;
        ChestEquipped.SetActive(true);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ChestUnlockedButton.SetActive(false);
            Warrior_ChestUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ChestBB.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ChestUnlockedButton.SetActive(false);
            Mage_ChestUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ChestBB.SetActive(true);
        }

        player.EquipChest();
    }
    public void EquipLegs()
    {
        bLegsEquipped = true;
        LegsEquipped.SetActive(true);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_LegsUnlockedButton.SetActive(false);
            Warrior_LegsUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_LegsBB.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_LegsUnlockedButton.SetActive(false);
            Mage_LegsUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_LegsBB.SetActive(true);
        }

        player.EquipLegs();
    }
    public void EquipShoes()
    {
        bShoesEquipped = true;
        ShoesEquipped.SetActive(true);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ShoesUnlockedButton.SetActive(false);
            Warrior_ShoesUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ShoesBB.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ShoesUnlockedButton.SetActive(false);
            Mage_ShoesUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ShoesBB.SetActive(true);
        }
        player.EquipShoes();
    }
    public void EquipWeapon()
    {
        bWeaponEquipped = true;
        WeaponEquipped.SetActive(true);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_WeaponUnlockedButton.SetActive(false);
            Warrior_WeaponUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_WeaponBB.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_WeaponUnlockedButton.SetActive(false);
            Mage_WeaponUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_WeaponBB.SetActive(true);
        }

        player.EquipWeapon();
    }
    public void EquipArtifact()
    {
        bArtifactEquipped = true;
        ArtifactEquipped.SetActive(true);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ArtifactUnlockedButton.SetActive(false);
            Warrior_ArtifactUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ArtifactBB.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ArtifactUnlockedButton.SetActive(false);
            Mage_ArtifactUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ArtifactBB.SetActive(true);
        }        
        player.EquipArtifact();
    }

    //Unequip Gear
    public void UnequipHead()
    {
        bHeadEquipped = false;
        HeadEquipped.SetActive(false);

        if(ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_HeadBB.SetActive(false);
            Warrior_HeadBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_HeadUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_HeadBB.SetActive(false);
            Mage_HeadBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_HeadUnlockedButton.SetActive(true);
        }

        player.UnequipHead();
    }
    public void UnequipChest()
    {
        bChestEquipped = false;
        ChestEquipped.SetActive(false);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ChestBB.SetActive(false);
            Warrior_ChestBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ChestUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ChestBB.SetActive(false);
            Mage_ChestBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ChestUnlockedButton.SetActive(true);
        }        

        player.UnequipChest();
    }
    public void UnequipLegs()
    {
        bLegsEquipped = false;
        LegsEquipped.SetActive(false);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_LegsBB.SetActive(false);
            Warrior_LegsBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_LegsUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_LegsBB.SetActive(false);
            Mage_LegsBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_LegsUnlockedButton.SetActive(true);
        }        

        player.UnequipLegs();
    }
    public void UnequipShoes()
    {
        bShoesEquipped = false;
        ShoesEquipped.SetActive(false);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ShoesBB.SetActive(false);
            Warrior_ShoesBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ShoesUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ShoesBB.SetActive(false);
            Mage_ShoesBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ShoesUnlockedButton.SetActive(true);
        }

        player.UnequipShoes();
    }
    public void UnequipWeapon()
    {
        bWeaponEquipped = false;
        WeaponEquipped.SetActive(false);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_WeaponBB.SetActive(false);
            Warrior_WeaponBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_WeaponUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_WeaponBB.SetActive(false);
            Mage_WeaponBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_WeaponUnlockedButton.SetActive(true);
        }

        player.UnequipWeapon();
    }
    public void UnequipArtifact()
    {
        bArtifactEquipped = false;
        ArtifactEquipped.SetActive(false);

        if (ClassName == "Knight" || ClassName == "Vagabond")
        {
            Warrior_ArtifactBB.SetActive(false);
            Warrior_ArtifactBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ArtifactUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Ranger")
        {
            Mage_ArtifactBB.SetActive(false);
            Mage_ArtifactBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ArtifactUnlockedButton.SetActive(true);
        }

        player.UnequipArtifact();
    }

    //Check for Bools

    public bool IsHeadEquipped()
    {
        return bHeadEquipped;
    }

    public bool IsChestEquipped()
    {
        return bChestEquipped;
    }

    public bool IsLegsEquipped()
    {
        return bLegsEquipped;
    }

    public bool IsShoesEquipped()
    {
        return bShoesEquipped;
    }

    public bool IsWeaponEquipped()
    {
        return bWeaponEquipped;
    }

    public bool IsArtifactEquipped()
    {
        return bArtifactEquipped;
    }

    //Ability Unlock
    public void UnlockAbility1()
    {
        Ability1Locked.SetActive(false);
        Ability1Button.SetActive(true);
    }

    public void UnlockAbility2()
    {
        Ability2Locked.SetActive(false);
        Ability2Button.SetActive(true);
    }

    public void UnlockAbility3()
    {
        Ability3Locked.SetActive(false);
        Ability3Button.SetActive(true);
    }

    public void UnlockAbility4()
    {
        Ability4Locked.SetActive(false);
        Ability4Button.SetActive(true);
    }

    //Ability Equip
    public void EquipAbility1()
    {
        if(bAbility1Equipped == false)
        {
            bAbility1Equipped = true;
            Ability1Equipped.SetActive(true);
            player.EquipAbility1();
            CooldownCanvas.SetActive(true);
        }
        else
        {
            UnequipAbility1();
        }
    }

    public void EquipAbility2()
    {
        if(bAbility2Equipped == false)
        {
            bAbility2Equipped = true;
            Ability2Equipped.SetActive(true);
            player.EquipAbility2();
            CooldownCanvas.SetActive(true);
        }
        else
        {
            UnequipAbility2();
        }
    }

    public void EquipAbility3()
    {
        if(bAbility3Equipped == false)
        {
            bAbility3Equipped = true;
            Ability3Equipped.SetActive(true);
            player.EquipAbility3();
            CooldownCanvas.SetActive(true);
        }
        else
        {
            UnequipAbility3();
        }
    }

    public void EquipAbility4()
    {
        if (bAbility4Equipped == false)
        {
            bAbility4Equipped = true;
            Ability4Equipped.SetActive(true);
            player.EquipAbility4();
            CooldownCanvas.SetActive(true);
        }
        else
        {
            UnequipAbility4();
        }
    }

    //Ability Unequip
    public void UnequipAbility1()
    {
        bAbility1Equipped = false;
        Ability1Equipped.SetActive(false);
        Ability1_Ready.SetActive(false);
        Ability1_Used.SetActive(false);
        player.UnequipAbility1();
    }

    public void UnequipAbility2()
    {
        bAbility2Equipped = false;
        Ability2Equipped.SetActive(false);
        Ability2_Ready.SetActive(false);
        Ability2_Used.SetActive(false);
        player.UnequipAbility2();
    }

    public void UnequipAbility3()
    {
        bAbility3Equipped = false;
        Ability3Equipped.SetActive(false);
        Ability3_Ready.SetActive(false);
        Ability3_Used.SetActive(false);
        player.UnequipAbility3();
    }

    public void UnequipAbility4()
    {
        bAbility4Equipped = false;
        Ability4Equipped.SetActive(false);
        Ability4_Ready.SetActive(false);
        Ability4_Used.SetActive(false);
        player.UnequipAbility4();
    }

    //Ability Getters
    public bool IsAbility1Equipped()
    {
        return bAbility1Equipped;
    }

    public bool IsAbility2Equipped()
    {
        return bAbility2Equipped;
    }

    public bool IsAbility3Equipped()
    {
        return bAbility3Equipped;
    }

    public bool IsAbility4Equipped()
    {
        return bAbility4Equipped;
    }

    //Abilities Ready to Use
    public void Ability1Ready()
    {
        Ability1_Ready.SetActive(true);
        Ability1_Used.SetActive(false);
    }

    public void Ability2Ready()
    {
        Ability2_Ready.SetActive(true);
        Ability2_Used.SetActive(false);
    }

    public void Ability3Ready()
    {
        Ability3_Ready.SetActive(true);
        Ability3_Used.SetActive(false);
    }

    public void Ability4Ready()
    {
        Ability4_Ready.SetActive(true);
        Ability4_Used.SetActive(false);
    }

    //Abilities on Cooldown
    public void Ability1Used()
    {
        Ability1_Used.SetActive(true);
        Ability1_Ready.SetActive(false);
    }

    public void Ability2Used()
    {
        Ability2_Used.SetActive(true);
        Ability2_Ready.SetActive(false);
    }

    public void Ability3Used()
    {
        Ability3_Used.SetActive(true);
        Ability3_Ready.SetActive(false);
    }

    public void Ability4Used()
    {
        Ability4_Used.SetActive(true);
        Ability4_Ready.SetActive(false);
    }

    //Keys
    public void UnlockKey1()
    {
        K1_Image.SetActive(true);
        K1_Locked.SetActive(false);
        NumberOfKeys++;
    }

    public void UnlockKey2()
    {
        K2_Image.SetActive(true);
        K2_Locked.SetActive(false);
        NumberOfKeys++;
    }

    public void UnlockKey3()
    {
        K3_Image.SetActive(true);
        K3_Locked.SetActive(false);
        NumberOfKeys++;
    }

    public bool CanEnterBossRoom()
    {
        if(NumberOfKeys == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetClassName(string x)
    {
        ClassName = x;
    }

    public string GetClassName()
    {
        return ClassName;
    }

}