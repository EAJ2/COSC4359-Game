using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV3_Ace : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] public Player player;

    [Header("Border Buttons")]
    [SerializeField] private GameObject HeadBB;
    [SerializeField] private GameObject ChestBB;
    [SerializeField] private GameObject LegsBB;
    [SerializeField] private GameObject ShoesBB;
    [SerializeField] private GameObject WeaponBB;
    [SerializeField] private GameObject ArtifactBB;

    [Header("Inventory Items Locked")]
    [SerializeField] private GameObject HeadLocked;
    [SerializeField] private GameObject ChestLocked;
    [SerializeField] private GameObject LegsLocked;
    [SerializeField] private GameObject ShoesLocked;
    [SerializeField] private GameObject WeaponLocked;
    [SerializeField] private GameObject ArtifactLocked;

    [Header("Inventory Items Unlocked")]
    [SerializeField] private GameObject HeadUnlockedButton;
    [SerializeField] private GameObject ChestUnlockedButton;
    [SerializeField] private GameObject LegsUnlockedButton;
    [SerializeField] private GameObject ShoesUnlockedButton;
    [SerializeField] private GameObject WeaponUnlockedButton;
    [SerializeField] private GameObject ArtifactUnlockedButton;

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

    private void Awake()
    {
        Canvas.SetActive(false);

        if(bDemo == false)
        {
            HeadBB.SetActive(false);
            ChestBB.SetActive(false);
            LegsBB.SetActive(false);
            ShoesBB.SetActive(false);
            WeaponBB.SetActive(false);
            ArtifactBB.SetActive(false);

            HeadUnlockedButton.SetActive(false);
            ChestUnlockedButton.SetActive(false);
            LegsUnlockedButton.SetActive(false);
            ShoesUnlockedButton.SetActive(false);
            WeaponUnlockedButton.SetActive(false);
            ArtifactUnlockedButton.SetActive(false);

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

            HeadBB.SetActive(false);
            ChestBB.SetActive(false);
            LegsBB.SetActive(false);
            ShoesBB.SetActive(false);
            WeaponBB.SetActive(false);
            ArtifactBB.SetActive(false);

            HeadUnlockedButton.SetActive(true);
            ChestUnlockedButton.SetActive(true);
            LegsUnlockedButton.SetActive(true);
            ShoesUnlockedButton.SetActive(true);
            WeaponUnlockedButton.SetActive(true);
            ArtifactUnlockedButton.SetActive(true);

            HeadEquipped.SetActive(false);
            ChestEquipped.SetActive(false);
            LegsEquipped.SetActive(false);
            ShoesEquipped.SetActive(false);
            WeaponEquipped.SetActive(false);
            ArtifactEquipped.SetActive(false);
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
        HeadUnlockedButton.SetActive(true);
    }

    public void EquipHead()
    {
        bHeadEquipped = true;
        HeadUnlockedButton.SetActive(false);
        HeadEquipped.SetActive(true);
        HeadBB.SetActive(true);
        player.EquipHead();
    }

    public void UnequipHead()
    {
        bHeadEquipped = false;
        HeadBB.SetActive(false);
        HeadEquipped.SetActive(false);
        HeadUnlockedButton.SetActive(true);
        player.UnequipHead();
    }

    public void UnlockChest()
    {
        ChestLocked.SetActive(false);
        ChestUnlockedButton.SetActive(true);
    }

    public void EquipChest()
    {
        bChestEquipped = true;
        ChestUnlockedButton.SetActive(false);
        ChestEquipped.SetActive(true);
        ChestBB.SetActive(true);
        player.EquipChest();
    }

    public void UnequipChest()
    {
        bChestEquipped = false;
        ChestBB.SetActive(false);
        ChestEquipped.SetActive(false);
        ChestUnlockedButton.SetActive(true);
        player.UnequipChest();
    }

    public void UnlockLegs()
    {
        LegsLocked.SetActive(false);
        LegsUnlockedButton.SetActive(true);
    }

    public void EquipLegs()
    {
        bLegsEquipped = true;
        LegsUnlockedButton.SetActive(false);
        LegsEquipped.SetActive(true);
        LegsBB.SetActive(true);
        player.EquipLegs();
    }

    public void UnequipLegs()
    {
        bLegsEquipped = false;
        LegsBB.SetActive(false);
        LegsEquipped.SetActive(false);
        LegsUnlockedButton.SetActive(true);
        player.UnequipLegs();
    }

    public void UnlockShoes()
    {
        ShoesLocked.SetActive(false);
        ShoesUnlockedButton.SetActive(true);
    }

    public void EquipShoes()
    {
        bShoesEquipped = true;
        ShoesUnlockedButton.SetActive(false);
        ShoesEquipped.SetActive(true);
        ShoesBB.SetActive(true);
        player.EquipShoes();
    }

    public void UnequipShoes()
    {
        bShoesEquipped = false;
        ShoesBB.SetActive(false);
        ShoesEquipped.SetActive(false);
        ShoesUnlockedButton.SetActive(true);
        player.UnequipShoes();
    }

    public void UnlockWeapon()
    {
        WeaponLocked.SetActive(false);
        WeaponUnlockedButton.SetActive(true);
    }

    public void EquipWeapon()
    {
        bWeaponEquipped = true;
        WeaponUnlockedButton.SetActive(false);
        WeaponEquipped.SetActive(true);
        WeaponBB.SetActive(true);
        player.EquipWeapon();
    }

    public void UnequipWeapon()
    {
        bWeaponEquipped = false;
        WeaponBB.SetActive(false);
        WeaponEquipped.SetActive(false);
        WeaponUnlockedButton.SetActive(true);
        player.UnequipWeapon();
    }

    public void UnlockArtifact()
    {
        ArtifactLocked.SetActive(false);
        ArtifactUnlockedButton.SetActive(true);
    }

    public void EquipArtifact()
    {
        bArtifactEquipped = true;
        ArtifactUnlockedButton.SetActive(false);
        ArtifactEquipped.SetActive(true);
        ArtifactBB.SetActive(true);
        player.EquipArtifact();
    }

    public void UnequipArtifact()
    {
        bArtifactEquipped = false;
        ArtifactBB.SetActive(false);
        ArtifactEquipped.SetActive(false);
        ArtifactUnlockedButton.SetActive(true);
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