using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestReward : MonoBehaviour
{
    [SerializeField] private ExoV3Movement player;
    private BoxCollider2D chestCollider;
    private Rigidbody2D rb;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject InteractCanvas;

    [Header("Unlockables")]
    [SerializeField] private bool bDoubleJump = false;
    [SerializeField] private bool bTeleport = false;
    [SerializeField] private bool bGrapplingHook = false;
    [SerializeField] private bool bDashBuff = false;
    [SerializeField] private bool bDamageBuff = false;
    [SerializeField] private bool bSpeedBuff = false;

    private bool bUnlocked = false;
    private int ChestIndex;

    private void Awake()
    {
        UI.SetActive(false);
        InteractCanvas.SetActive(false);

        chestCollider = GetComponent<BoxCollider2D>();

        if (bDoubleJump)
        {
            ChestIndex = 0;
        }
        if (bTeleport)
        {
            ChestIndex = 1;
        }
        if (bGrapplingHook)
        {
            ChestIndex = 2;
        }
        if (bDashBuff)
        {
            ChestIndex = 3;
        }
        if (bDamageBuff)
        {
            ChestIndex = 4;
        }
        if (bSpeedBuff)
        {
            ChestIndex = 5;
        }
    }

    public void UnlockReward()
    {
        if(!bUnlocked)
        {
            ShowRewardUI();
            if (bDoubleJump)
            {
                inventory.UnlockGear1();
            }
            if (bTeleport)
            {
                inventory.UnlockGear2();
            }
            if (bGrapplingHook)
            {
                inventory.UnlockGear3();
            }
            if (bDashBuff)
            {
                inventory.UnlockChip1();
            }
            if (bDamageBuff)
            {
                inventory.UnlockChip2();
            }
            if (bSpeedBuff)
            {
                inventory.UnlockChip3();
            }
            bUnlocked = true;
            InteractCanvas.SetActive(false);
        }
    }

    public bool IsChestLocked()
    {
        if(bUnlocked)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.SetChestIndex(ChestIndex);
            player.IsPlayerInsideChest(true);
            if(!bUnlocked)
            {
                InteractCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.IsPlayerInsideChest(false);
            InteractCanvas.SetActive(false);
        }
    }

    private void ShowRewardUI()
    {
        UI.SetActive(true);
    }

    public void HideRewardUI()
    {
        UI.SetActive(false);
        player.EnableFullMovement();
    }
}
