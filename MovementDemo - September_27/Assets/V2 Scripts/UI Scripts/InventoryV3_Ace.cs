using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV3_Ace : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] public Player player;

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

    private bool bCanvasActive = false;

    private bool bHeadEquipped = false;
    private bool bChestEquipped = false;
    private bool bLegsEquipped = false;
    private bool bShoesEquipped = false;
    private bool bWeaponEquipped = false;
    private bool bArtifactEquipped = false;

    [SerializeField] public bool bDemo = false;
    [SerializeField] private string ClassName;


    private void Awake()
    {
        Canvas.SetActive(false);

        //ClassName = player.GetClassName();
        Debug.Log("(Remember to uncommentNe line above me) Class Name in InventoryV3_Ace = " + ClassName);

        if(bDemo == false)
        {
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
        }
        else
        {
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

            if (ClassName == "Warrior")
            {
                Warrior_HeadUnlockedButton.SetActive(true);
                Warrior_ChestUnlockedButton.SetActive(true);
                Warrior_LegsUnlockedButton.SetActive(true);
                Warrior_ShoesUnlockedButton.SetActive(true);
                Warrior_WeaponUnlockedButton.SetActive(true);
                Warrior_ArtifactUnlockedButton.SetActive(true);
            }
            else if (ClassName == "Mage")
            {
                Mage_HeadUnlockedButton.SetActive(true);
                Mage_ChestUnlockedButton.SetActive(true);
                Mage_LegsUnlockedButton.SetActive(true);
                Mage_ShoesUnlockedButton.SetActive(true);
                Mage_WeaponUnlockedButton.SetActive(true);
                Mage_ArtifactUnlockedButton.SetActive(true);
            }
  
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
        }
        else
        {
            bCanvasActive = false;
            Canvas.SetActive(false);
        }
    }

    public void UnlockHead()
    {
        HeadLocked.SetActive(false);
        if (ClassName == "Warrior")
        {
            Warrior_HeadUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_HeadUnlockedButton.SetActive(true);
        }
    }

    public void EquipHead()
    {
        bHeadEquipped = true;
        HeadEquipped.SetActive(true);

        if(ClassName == "Warrior")
        {
            Warrior_HeadUnlockedButton.SetActive(false);
            Warrior_HeadUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_HeadBB.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_HeadUnlockedButton.SetActive(false);
            Mage_HeadUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_HeadBB.SetActive(true);
        }

        player.EquipHead();
    }

    public void UnequipHead()
    {
        bHeadEquipped = false;
        HeadEquipped.SetActive(false);

        if(ClassName == "Warrior")
        {
            Warrior_HeadBB.SetActive(false);
            Warrior_HeadBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_HeadUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_HeadBB.SetActive(false);
            Mage_HeadBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_HeadUnlockedButton.SetActive(true);
        }

        player.UnequipHead();
    }

    public void UnlockChest()
    {
        ChestLocked.SetActive(false);
        if (ClassName == "Warrior")
        {
            Warrior_ChestUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ChestUnlockedButton.SetActive(true);
        }
    }

    public void EquipChest()
    {
        bChestEquipped = true;
        ChestEquipped.SetActive(true);

        if (ClassName == "Warrior")
        {
            Warrior_ChestUnlockedButton.SetActive(false);
            Warrior_ChestUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ChestBB.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ChestUnlockedButton.SetActive(false);
            Mage_ChestUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ChestBB.SetActive(true);
        }

        player.EquipChest();
    }

    public void UnequipChest()
    {
        bChestEquipped = false;
        ChestEquipped.SetActive(false);

        if (ClassName == "Warrior")
        {
            Warrior_ChestBB.SetActive(false);
            Warrior_ChestBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ChestUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ChestBB.SetActive(false);
            Mage_ChestBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ChestUnlockedButton.SetActive(true);
        }        

        player.UnequipChest();
    }

    public void UnlockLegs()
    {
        LegsLocked.SetActive(false);
        if (ClassName == "Warrior")
        {
            Warrior_LegsUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_LegsUnlockedButton.SetActive(true);
        }
    }

    public void EquipLegs()
    {
        bLegsEquipped = true;
        LegsEquipped.SetActive(true);

        if (ClassName == "Warrior")
        {
            Warrior_LegsUnlockedButton.SetActive(false);
            Warrior_LegsUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_LegsBB.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_LegsUnlockedButton.SetActive(false);
            Mage_LegsUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_LegsBB.SetActive(true);
        }

        player.EquipLegs();
    }

    public void UnequipLegs()
    {
        bLegsEquipped = false;
        LegsEquipped.SetActive(false);

        if (ClassName == "Warrior")
        {
            Warrior_LegsBB.SetActive(false);
            Warrior_LegsBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_LegsUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_LegsBB.SetActive(false);
            Mage_LegsBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_LegsUnlockedButton.SetActive(true);
        }        

        player.UnequipLegs();
    }

    public void UnlockShoes()
    {
        ShoesLocked.SetActive(false);
        if (ClassName == "Warrior")
        {
            Warrior_ShoesUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ShoesUnlockedButton.SetActive(true);
        }
    }

    public void EquipShoes()
    {
        bShoesEquipped = true;
        ShoesEquipped.SetActive(true);

        if (ClassName == "Warrior")
        {
            Warrior_ShoesUnlockedButton.SetActive(false);
            Warrior_ShoesUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ShoesBB.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ShoesUnlockedButton.SetActive(false);
            Mage_ShoesUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ShoesBB.SetActive(true);
        }
        player.EquipShoes();
    }

    public void UnequipShoes()
    {
        bShoesEquipped = false;
        ShoesEquipped.SetActive(false);

        if (ClassName == "Warrior")
        {
            Warrior_ShoesBB.SetActive(false);
            Warrior_ShoesBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ShoesUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ShoesBB.SetActive(false);
            Mage_ShoesBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ShoesUnlockedButton.SetActive(true);
        }

        player.UnequipShoes();
    }

    public void UnlockWeapon()
    {
        WeaponLocked.SetActive(false);
        if (ClassName == "Warrior")
        {
            Warrior_WeaponUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_WeaponUnlockedButton.SetActive(true);
        }
    }

    public void EquipWeapon()
    {
        bWeaponEquipped = true;
        WeaponEquipped.SetActive(true);

        if (ClassName == "Warrior")
        {
            Warrior_WeaponUnlockedButton.SetActive(false);
            Warrior_WeaponUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_WeaponBB.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_WeaponUnlockedButton.SetActive(false);
            Mage_WeaponUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_WeaponBB.SetActive(true);
        }

        player.EquipWeapon();
    }

    public void UnequipWeapon()
    {
        bWeaponEquipped = false;
        WeaponEquipped.SetActive(false);

        if (ClassName == "Warrior")
        {
            Warrior_WeaponBB.SetActive(false);
            Warrior_WeaponBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_WeaponUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_WeaponBB.SetActive(false);
            Mage_WeaponBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_WeaponUnlockedButton.SetActive(true);
        }

        player.UnequipWeapon();
    }

    public void UnlockArtifact()
    {
        ArtifactLocked.SetActive(false);
        if (ClassName == "Warrior")
        {
            Warrior_ArtifactUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ArtifactUnlockedButton.SetActive(true);
        }
    }

    public void EquipArtifact()
    {
        bArtifactEquipped = true;
        ArtifactEquipped.SetActive(true);

        if (ClassName == "Warrior")
        {
            Warrior_ArtifactUnlockedButton.SetActive(false);
            Warrior_ArtifactUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ArtifactBB.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ArtifactUnlockedButton.SetActive(false);
            Mage_ArtifactUnlockedButton.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ArtifactBB.SetActive(true);
        }        
        player.EquipArtifact();
    }

    public void UnequipArtifact()
    {
        bArtifactEquipped = false;
        ArtifactEquipped.SetActive(false);

        if (ClassName == "Warrior")
        {
            Warrior_ArtifactBB.SetActive(false);
            Warrior_ArtifactBB.GetComponent<TextHover>().mytext.SetActive(false);
            Warrior_ArtifactUnlockedButton.SetActive(true);
        }
        else if (ClassName == "Mage")
        {
            Mage_ArtifactBB.SetActive(false);
            Mage_ArtifactBB.GetComponent<TextHover>().mytext.SetActive(false);
            Mage_ArtifactUnlockedButton.SetActive(true);
        }

        player.UnequipArtifact();
    }

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

}