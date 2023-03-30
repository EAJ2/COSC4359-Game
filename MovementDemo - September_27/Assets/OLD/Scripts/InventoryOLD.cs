using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private bool bUnlockAll = false;
    [SerializeField] private ExoV3Movement player;
    [SerializeField] private Teleport teleport;
    [SerializeField] private GrapplingHook grapple;

    public GameObject InventoryCanvas;

    private bool bShowing = false;

    //The Inventory slots
    private bool bGear1_Equipped = false;
    private bool bGear2_Equipped = false;
    private bool bGear3_Equipped = false;

    private bool bPowerChip1_Equipped = false;
    private bool bPowerChip2_Equipped = false;
    private bool bPowerChip3_Equipped = false;

    private bool bGear1Unlocked = false;
    private bool bGear2Unlocked = false;
    private bool bGear3Unlocked = false;
    private bool bChip1Unlocked = false;
    private bool bChip2Unlocked = false;
    private bool bChip3Unlocked = false;

    struct Slot
    {
        public bool bUsed;
        public bool bBadge1Equipped;
        public bool bBadge2Equipped;
        public bool bBadge3Equipped;

        public Slot(bool used, bool badge1, bool badge2, bool badge3)
        {
            this.bUsed = used;
            this.bBadge1Equipped = badge1;
            this.bBadge2Equipped = badge2;
            this.bBadge3Equipped = badge3;
        }
    }

    [Header("Gear Equipped 1")]
    public GameObject Gear1_Icon1;
    public GameObject Gear1_Icon2;
    public GameObject Gear1_Icon3;
    public GameObject Gear1_IconMissing;
    public Button Gear1_Button;

    [Header("Gear Equipped 2")]
    public GameObject Gear2_Icon1;
    public GameObject Gear2_Icon2;
    public GameObject Gear2_Icon3;
    public GameObject Gear2_IconMissing;
    public Button Gear2_Button;

    [Header("Gear Equipped 3")]
    public GameObject Gear3_Icon1;
    public GameObject Gear3_Icon2;
    public GameObject Gear3_Icon3;
    public GameObject Gear3_IconMissing;
    public Button Gear3_Button;

    [Header("Gear Inventory 1")]
    public GameObject Gear1Inventory_Icon;
    public GameObject Gear1Inventory_IconMissing;
    public GameObject Gear1Inventory_IconLocked;
    public Button Gear1Inventory_Button;

    [Header("Gear Inventory 2")]
    public GameObject Gear2Inventory_Icon;
    public GameObject Gear2Inventory_IconMissing;
    public GameObject Gear2Inventory_IconLocked;
    public Button Gear2Inventory_Button;

    [Header("Gear Inventory 3")]
    public GameObject Gear3Inventory_Icon;
    public GameObject Gear3Inventory_IconMissing;
    public GameObject Gear3Inventory_IconLocked;
    public Button Gear3Inventory_Button;

    [Header("Power Chips Equipped 1")]
    public GameObject PC1_Icon1;
    public GameObject PC1_Icon2;
    public GameObject PC1_Icon3;
    public GameObject PC1_IconMissing;
    public Button PC1_Button;

    [Header("Power Chips Equipped 2")]
    public GameObject PC2_Icon1;
    public GameObject PC2_Icon2;
    public GameObject PC2_Icon3;
    public GameObject PC2_IconMissing;
    public Button PC2_Button;

    [Header("Power Chips Equipped 3")]
    public GameObject PC3_Icon1;
    public GameObject PC3_Icon2;
    public GameObject PC3_Icon3;
    public GameObject PC3_IconMissing;
    public Button PC3_Button;

    [Header("Power Chips Inventory 1")]
    public GameObject PCInventory1_Icon;
    public GameObject PCInventory1_IconMissing;
    public GameObject PCInventory1_IconLocked;
    public Button PCInventory1_Button;

    [Header("Power Chips Inventory 2")]
    public GameObject PCInventory2_Icon;
    public GameObject PCInventory2_IconMissing;
    public GameObject PCInventory2_IconLocked;
    public Button PCInventory2_Button;

    [Header("Power Chips Inventory 3")]
    public GameObject PCInventory3_Icon;
    public GameObject PCInventory3_IconMissing;
    public GameObject PCInventory3_IconLocked;
    public Button PCInventory3_Button;

    //Gear Slot 1 2 3
    Slot GS1;
    Slot GS2;
    Slot GS3;

    //Power Slot 1 2 3
    Slot PS1;
    Slot PS2;
    Slot PS3;

    private void Awake()
    {
        InventoryCanvas.SetActive(false);

        //Set Gear Images
        Gear1_Icon1.SetActive(false);
        Gear1_Icon2.SetActive(false);
        Gear1_Icon3.SetActive(false);
        Gear1_IconMissing.SetActive(true);

        Gear2_Icon1.SetActive(false);
        Gear2_Icon2.SetActive(false);
        Gear2_Icon3.SetActive(false);
        Gear2_IconMissing.SetActive(true);

        Gear3_Icon1.SetActive(false);
        Gear3_Icon2.SetActive(false);
        Gear3_Icon3.SetActive(false);
        Gear3_IconMissing.SetActive(true);

        Gear1Inventory_Icon.SetActive(false);
        Gear1Inventory_IconMissing.SetActive(true);
        Gear1Inventory_IconLocked.SetActive(false);

        Gear2Inventory_Icon.SetActive(false);
        Gear2Inventory_IconMissing.SetActive(true);
        Gear2Inventory_IconLocked.SetActive(false);

        Gear3Inventory_Icon.SetActive(false);
        Gear3Inventory_IconMissing.SetActive(true);
        Gear3Inventory_IconLocked.SetActive(false);

        //Set Badge Images
        PC1_Icon1.SetActive(false);
        PC1_Icon2.SetActive(false);
        PC1_Icon3.SetActive(false);
        PC1_IconMissing.SetActive(true);

        PC2_Icon1.SetActive(false);
        PC2_Icon2.SetActive(false);
        PC2_Icon3.SetActive(false);
        PC2_IconMissing.SetActive(true);

        PC3_Icon1.SetActive(false);
        PC3_Icon2.SetActive(false);
        PC3_Icon3.SetActive(false);
        PC3_IconMissing.SetActive(true);

        PCInventory1_Icon.SetActive(false);
        PCInventory1_IconMissing.SetActive(true);
        PCInventory1_IconLocked.SetActive(false);

        PCInventory2_Icon.SetActive(false);
        PCInventory2_IconMissing.SetActive(true);
        PCInventory2_IconLocked.SetActive(false);

        PCInventory3_Icon.SetActive(false);
        PCInventory3_IconMissing.SetActive(true);
        PCInventory3_IconLocked.SetActive(false);



        Gear1Inventory_Button.GetComponent<Button>().interactable = false;
        Gear2Inventory_Button.GetComponent<Button>().interactable = false;
        Gear3Inventory_Button.GetComponent<Button>().interactable = false;

        PCInventory1_Button.GetComponent<Button>().interactable = false;
        PCInventory2_Button.GetComponent<Button>().interactable = false;
        PCInventory3_Button.GetComponent<Button>().interactable = false;

        //Create slots structs
        GS1 = new Slot(false, false, false, false);
        GS2 = new Slot(false, false, false, false);
        GS3 = new Slot(false, false, false, false);

        PS1 = new Slot(false, false, false, false);
        PS2 = new Slot(false, false, false, false);
        PS3 = new Slot(false, false, false, false);

        if(bUnlockAll)
        {
            UnlockChip1();
            UnlockChip2();
            UnlockChip3();
            UnlockGear1();
            UnlockGear2();
            UnlockGear3();
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
        if (bShowing)
        {
            InventoryCanvas.SetActive(false);
            bShowing = false;
        }
        else
        {
            InventoryCanvas.SetActive(true);
            bShowing = true;
        }
    }

    //Equips the first Gear to one of 3 Active slots
    public void EquipGearBadge1()
    {
        if(bGear1Unlocked)
        {
            if (!bGear1_Equipped) //If Gear Inventory slot 1 is not equipped
            {
                if (GS1.bUsed == false) //if Gear slots 1 is not being used
                {
                    GS1.bUsed = true; //Set its to true 
                    GS1.bBadge1Equipped = true; //Set that badge 1 in the Gear slots is the one being equiped to slot 1

                    Gear1_Icon1.SetActive(true);
                    Gear1_IconMissing.SetActive(false);

                    Gear1Inventory_Icon.SetActive(false);
                    Gear1Inventory_IconLocked.SetActive(true);
                    Gear1Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear1_Equipped = true;
                    player.EnableDoubleJump();
                }
                else if (GS2.bUsed == false)
                {
                    GS2.bUsed = true;
                    GS2.bBadge1Equipped = true;

                    Gear2_Icon1.SetActive(true);
                    Gear2_IconMissing.SetActive(false);

                    Gear1Inventory_Icon.SetActive(false);
                    Gear1Inventory_IconLocked.SetActive(true);
                    Gear1Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear1_Equipped = true;
                    player.EnableDoubleJump();
                }
                else if (GS3.bUsed == false)
                {
                    GS3.bUsed = true;
                    GS3.bBadge1Equipped = true;

                    Gear3_Icon1.SetActive(true);
                    Gear3_IconMissing.SetActive(false);

                    Gear1Inventory_Icon.SetActive(false);
                    Gear1Inventory_IconLocked.SetActive(true);
                    Gear1Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear1_Equipped = true;
                    player.EnableDoubleJump();
                }
            }
        }
    }

    public void EquipPowerBadge1()
    {
        if(bChip1Unlocked)
        {
            if (!bPowerChip1_Equipped)
            {
                if (PS1.bUsed == false)
                {
                    PS1.bUsed = true;
                    PS1.bBadge1Equipped = true;

                    PC1_Icon1.SetActive(true);
                    PC1_IconMissing.SetActive(false);

                    PCInventory1_Icon.SetActive(false);
                    PCInventory1_IconLocked.SetActive(true);
                    PCInventory1_Button.GetComponent<Button>().interactable = false;

                    bPowerChip1_Equipped = true;

                    player.EnablePower1();
                }
                else if (PS2.bUsed == false)
                {
                    PS2.bUsed = true;
                    PS2.bBadge1Equipped = true;

                    PC2_Icon1.SetActive(true);
                    PC2_IconMissing.SetActive(false);

                    PCInventory1_Icon.SetActive(false);
                    PCInventory1_IconLocked.SetActive(true);
                    PCInventory1_Button.GetComponent<Button>().interactable = false;

                    bPowerChip1_Equipped = true;

                    player.EnablePower1();
                }
                else if (PS3.bUsed == false)
                {
                    PS3.bUsed = true;
                    PS3.bBadge1Equipped = true;

                    PC3_Icon1.SetActive(true);
                    PC3_IconMissing.SetActive(false);

                    PCInventory1_Icon.SetActive(false);
                    PCInventory1_IconLocked.SetActive(true);
                    PCInventory1_Button.GetComponent<Button>().interactable = false;

                    bPowerChip1_Equipped = true;

                    player.EnablePower1();
                }
            }
        }
    }

    public void EquipGearBadge2()
    {
        if(bGear2Unlocked)
        {
            if (!bGear2_Equipped)
            {
                if (GS1.bUsed == false)
                {
                    GS1.bUsed = true;
                    GS1.bBadge2Equipped = true;

                    Gear1_Icon2.SetActive(true);
                    Gear1_IconMissing.SetActive(false);

                    Gear2Inventory_Icon.SetActive(false);
                    Gear2Inventory_IconLocked.SetActive(true);
                    Gear2Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear2_Equipped = true;
                    teleport.EnableTeleport();
                }
                else if (GS2.bUsed == false)
                {
                    GS2.bUsed = true;
                    GS2.bBadge2Equipped = true;

                    Gear2_Icon2.SetActive(true);
                    Gear2_IconMissing.SetActive(false);

                    Gear2Inventory_Icon.SetActive(false);
                    Gear2Inventory_IconLocked.SetActive(true);
                    Gear2Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear2_Equipped = true;
                    teleport.EnableTeleport();
                }
                else if (GS3.bUsed == false)
                {
                    GS3.bUsed = true;
                    GS3.bBadge1Equipped = true;

                    Gear3_Icon2.SetActive(true);
                    Gear3_IconMissing.SetActive(false);

                    Gear2Inventory_Icon.SetActive(false);
                    Gear2Inventory_IconLocked.SetActive(true);
                    Gear2Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear2_Equipped = true;
                    teleport.EnableTeleport();
                }
            }
        }
    }

    public void EquipPowerBadge2()
    {
        if(bChip2Unlocked)
        {
            if (!bPowerChip2_Equipped)
            {
                if (PS1.bUsed == false)
                {
                    PS1.bUsed = true;
                    PS1.bBadge2Equipped = true;

                    PC1_Icon2.SetActive(true);
                    PC1_IconMissing.SetActive(false);

                    PCInventory2_Icon.SetActive(false);
                    PCInventory2_IconLocked.SetActive(true);
                    PCInventory2_Button.GetComponent<Button>().interactable = false;

                    bPowerChip2_Equipped = true;

                    player.EnablePower2();
                }
                else if (PS2.bUsed == false)
                {
                    PS2.bUsed = true;
                    PS2.bBadge2Equipped = true;

                    PC2_Icon2.SetActive(true);
                    PC2_IconMissing.SetActive(false);

                    PCInventory2_Icon.SetActive(false);
                    PCInventory2_IconLocked.SetActive(true);
                    PCInventory2_Button.GetComponent<Button>().interactable = false;

                    bPowerChip2_Equipped = true;

                    player.EnablePower2();
                }
                else if (PS3.bUsed == false)
                {
                    PS3.bUsed = true;
                    PS3.bBadge2Equipped = true;

                    PC3_Icon1.SetActive(true);
                    PC3_IconMissing.SetActive(false);

                    PCInventory2_Icon.SetActive(false);
                    PCInventory2_IconLocked.SetActive(true);
                    PCInventory2_Button.GetComponent<Button>().interactable = false;

                    bPowerChip2_Equipped = true;

                    player.EnablePower2();
                }
            }
        }
    }


    public void EquipGearBadge3()
    {
        if(bGear3Unlocked)
        {
            if (!bGear3_Equipped)
            {
                if (GS1.bUsed == false)
                {
                    GS1.bUsed = true;
                    GS1.bBadge3Equipped = true;

                    Gear1_Icon3.SetActive(true);
                    Gear1_IconMissing.SetActive(false);

                    Gear3Inventory_Icon.SetActive(false);
                    Gear3Inventory_IconLocked.SetActive(true);
                    Gear3Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear3_Equipped = true;
                    grapple.EnableGrapple();
                }
                else if (GS2.bUsed == false)
                {
                    GS2.bUsed = true;
                    GS2.bBadge3Equipped = true;

                    Gear2_Icon3.SetActive(true);
                    Gear2_IconMissing.SetActive(false);

                    Gear3Inventory_Icon.SetActive(false);
                    Gear3Inventory_IconLocked.SetActive(true);
                    Gear3Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear3_Equipped = true;
                    grapple.EnableGrapple();
                }
                else if (GS3.bUsed == false)
                {
                    GS3.bUsed = true;
                    GS3.bBadge3Equipped = true;

                    Gear3_Icon3.SetActive(true);
                    Gear3_IconMissing.SetActive(false);

                    Gear3Inventory_Icon.SetActive(false);
                    Gear3Inventory_IconLocked.SetActive(true);
                    Gear3Inventory_Button.GetComponent<Button>().interactable = false;

                    bGear3_Equipped = true;
                    grapple.EnableGrapple();
                }
            }
        }
    }

    public void EquipPowerBadge3()
    {
        if(bChip3Unlocked)
        {
            if (!bPowerChip3_Equipped)
            {
                if (PS1.bUsed == false)
                {
                    PS1.bUsed = true;
                    PS1.bBadge3Equipped = true;

                    PC1_Icon3.SetActive(true);
                    PC1_IconMissing.SetActive(false);

                    PCInventory3_Icon.SetActive(false);
                    PCInventory3_IconLocked.SetActive(true);
                    PCInventory3_Button.GetComponent<Button>().interactable = false;

                    bPowerChip3_Equipped = true;

                    player.EnablePower3();
                }
                else if (PS2.bUsed == false)
                {
                    PS2.bUsed = true;
                    PS2.bBadge3Equipped = true;

                    PC2_Icon3.SetActive(true);
                    PC2_IconMissing.SetActive(false);

                    PCInventory3_Icon.SetActive(false);
                    PCInventory3_IconLocked.SetActive(true);
                    PCInventory3_Button.GetComponent<Button>().interactable = false;

                    bPowerChip3_Equipped = true;

                    player.EnablePower3();
                }
                else if (PS3.bUsed == false)
                {
                    PS3.bUsed = true;
                    PS3.bBadge3Equipped = true;

                    PC3_Icon3.SetActive(true);
                    PC3_IconMissing.SetActive(false);

                    PCInventory3_Icon.SetActive(false);
                    PCInventory3_IconLocked.SetActive(true);
                    PCInventory3_Button.GetComponent<Button>().interactable = false;

                    bPowerChip3_Equipped = true;

                    player.EnablePower3();
                }
            }
        }
    }

    public void UnEquipGearSlot1()
    {
        if (GS1.bBadge1Equipped == true)
        {
            GS1.bUsed = false;
            GS1.bBadge1Equipped = false;

            Gear1_Icon1.SetActive(false);
            Gear1_IconMissing.SetActive(true);

            Gear1Inventory_Icon.SetActive(true);
            Gear1Inventory_IconLocked.SetActive(false);
            Gear1Inventory_Button.GetComponent<Button>().interactable = true;

            bGear1_Equipped = false;
            player.DisableDoubleJump();
        }
        else if (GS1.bBadge2Equipped == true)
        {
            GS1.bUsed = false;
            GS1.bBadge2Equipped = false;

            Gear1_Icon2.SetActive(false);
            Gear1_IconMissing.SetActive(true);

            Gear2Inventory_Icon.SetActive(true);
            Gear2Inventory_IconLocked.SetActive(false);
            Gear2Inventory_Button.GetComponent<Button>().interactable = true;

            bGear2_Equipped = false;
            teleport.DisableTeleport();
        }
        else if (GS1.bBadge3Equipped == true)
        {
            GS1.bUsed = false;
            GS1.bBadge3Equipped = false;

            Gear1_Icon3.SetActive(false);
            Gear1_IconMissing.SetActive(true);

            Gear3Inventory_Icon.SetActive(true);
            Gear3Inventory_IconLocked.SetActive(false);
            Gear3Inventory_Button.GetComponent<Button>().interactable = true;

            bGear3_Equipped = false;
            grapple.DisableGrapple();
        }
    }

    public void UnEquipPowerSlot1()
    {
        if (PS1.bBadge1Equipped == true)
        {
            PS1.bUsed = false;
            PS1.bBadge1Equipped = false;

            PC1_Icon1.SetActive(false);
            PC1_IconMissing.SetActive(true);

            PCInventory1_Icon.SetActive(true);
            PCInventory1_IconLocked.SetActive(false);
            PCInventory1_Button.GetComponent<Button>().interactable = true;

            bPowerChip1_Equipped = false;

            player.DisablePower1();
        }
        else if(PS1.bBadge2Equipped == true)
        {
            PS1.bUsed = false;
            PS1.bBadge2Equipped = false;

            PC1_Icon2.SetActive(false);
            PC1_IconMissing.SetActive(true);

            PCInventory2_Icon.SetActive(true);
            PCInventory2_IconLocked.SetActive(false);
            PCInventory2_Button.GetComponent<Button>().interactable = true;

            bPowerChip2_Equipped = false;

            player.DisablePower2();
        }
        else if (PS1.bBadge3Equipped == true)
        {
            PS1.bUsed = false;
            PS1.bBadge3Equipped = false;

            PC1_Icon3.SetActive(false);
            PC1_IconMissing.SetActive(true);

            PCInventory3_Icon.SetActive(true);
            PCInventory3_IconLocked.SetActive(false);
            PCInventory3_Button.GetComponent<Button>().interactable = true;

            bPowerChip3_Equipped = false;

            player.DisablePower3();
        }
    }

    public void UnEquipGearSlot2()
    {
        if (GS2.bBadge1Equipped == true)
        {
            GS2.bUsed = false;
            GS2.bBadge1Equipped = false;

            Gear2_Icon1.SetActive(false);
            Gear2_IconMissing.SetActive(true);

            Gear1Inventory_Icon.SetActive(true);
            Gear1Inventory_IconLocked.SetActive(false);
            Gear1Inventory_Button.GetComponent<Button>().interactable = true;

            bGear1_Equipped = false;
            player.DisableDoubleJump();
        }
        else if (GS2.bBadge2Equipped == true)
        {
            GS2.bUsed = false;
            GS2.bBadge2Equipped = false;

            Gear2_Icon2.SetActive(false);
            Gear2_IconMissing.SetActive(true);

            Gear2Inventory_Icon.SetActive(true);
            Gear2Inventory_IconLocked.SetActive(false);
            Gear2Inventory_Button.GetComponent<Button>().interactable = true;

            bGear2_Equipped = false;
            teleport.DisableTeleport();
        }
        else if (GS2.bBadge3Equipped == true)
        {
            GS2.bUsed = false;
            GS2.bBadge3Equipped = false;

            Gear2_Icon3.SetActive(false);
            Gear2_IconMissing.SetActive(true);

            Gear3Inventory_Icon.SetActive(true);
            Gear3Inventory_IconLocked.SetActive(false);
            Gear3Inventory_Button.GetComponent<Button>().interactable = true;

            bGear3_Equipped = false;
            grapple.DisableGrapple();
        }
    }

    public void UnEquipPowerSlot2()
    {
        if (PS2.bBadge1Equipped == true)
        {
            PS2.bUsed = false;
            PS2.bBadge1Equipped = false;

            PC2_Icon1.SetActive(false);
            PC2_IconMissing.SetActive(true);

            PCInventory1_Icon.SetActive(true);
            PCInventory1_IconLocked.SetActive(false);
            PCInventory1_Button.GetComponent<Button>().interactable = true;

            bPowerChip1_Equipped = false;

            player.DisablePower1();

        }
        else if (PS2.bBadge2Equipped == true)
        {
            PS2.bUsed = false;
            PS2.bBadge2Equipped = false;

            PC2_Icon2.SetActive(false);
            PC2_IconMissing.SetActive(true);

            PCInventory2_Icon.SetActive(true);
            PCInventory2_IconLocked.SetActive(false);
            PCInventory2_Button.GetComponent<Button>().interactable = true;

            bPowerChip2_Equipped = false;

            player.DisablePower2();
        }
        else if (PS2.bBadge3Equipped == true)
        {
            PS2.bUsed = false;
            PS2.bBadge3Equipped = false;

            PC2_Icon3.SetActive(false);
            PC2_IconMissing.SetActive(true);

            PCInventory3_Icon.SetActive(true);
            PCInventory3_IconLocked.SetActive(false);
            PCInventory3_Button.GetComponent<Button>().interactable = true;

            bPowerChip3_Equipped = false;

            player.DisablePower3();
        }
    }

    public void UnEquipGearSlot3()
    {
        if (GS3.bBadge1Equipped == true)
        {
            GS3.bUsed = false;
            GS3.bBadge1Equipped = false;

            Gear3_Icon1.SetActive(false);
            Gear3_IconMissing.SetActive(true);

            Gear1Inventory_Icon.SetActive(true);
            Gear1Inventory_IconLocked.SetActive(false);
            Gear1Inventory_Button.GetComponent<Button>().interactable = true;

            bGear1_Equipped = false;
            player.DisableDoubleJump();
        }
        else if (GS3.bBadge2Equipped == true)
        {
            GS3.bUsed = false;
            GS3.bBadge2Equipped = false;

            Gear3_Icon2.SetActive(false);
            Gear3_IconMissing.SetActive(true);

            Gear2Inventory_Icon.SetActive(true);
            Gear2Inventory_IconLocked.SetActive(false);
            Gear2Inventory_Button.GetComponent<Button>().interactable = true;

            bGear2_Equipped = false;
            teleport.DisableTeleport();
        }
        else if (GS3.bBadge3Equipped == true)
        {
            GS3.bUsed = false;
            GS3.bBadge3Equipped = false;

            Gear3_Icon3.SetActive(false);
            Gear3_IconMissing.SetActive(true);

            Gear3Inventory_Icon.SetActive(true);
            Gear3Inventory_IconLocked.SetActive(false);
            Gear3Inventory_Button.GetComponent<Button>().interactable = true;

            bGear3_Equipped = false;
            grapple.DisableGrapple();
        }
    }

    public void UnEquipPowerSlot3()
    {
        if (PS3.bBadge1Equipped == true)
        {
            PS3.bUsed = false;
            PS3.bBadge1Equipped = false;

            PC3_Icon1.SetActive(false);
            PC3_IconMissing.SetActive(true);

            PCInventory1_Icon.SetActive(true);
            PCInventory1_IconLocked.SetActive(false);
            PCInventory1_Button.GetComponent<Button>().interactable = true;

            bPowerChip1_Equipped = false;

            player.DisablePower1();
        }
        else if (PS3.bBadge2Equipped == true)
        {
            PS3.bUsed = false;
            PS3.bBadge2Equipped = false;

            PC3_Icon2.SetActive(false);
            PC3_IconMissing.SetActive(true);

            PCInventory2_Icon.SetActive(true);
            PCInventory2_IconLocked.SetActive(false);
            PCInventory2_Button.GetComponent<Button>().interactable = true;

            bPowerChip2_Equipped = false;

            player.DisablePower2();
        }
        else if (PS3.bBadge3Equipped == true)
        {
            PS3.bUsed = false;
            PS3.bBadge3Equipped = false;

            PC3_Icon3.SetActive(false);
            PC3_IconMissing.SetActive(true);

            PCInventory3_Icon.SetActive(true);
            PCInventory3_IconLocked.SetActive(false);
            PCInventory3_Button.GetComponent<Button>().interactable = true;

            bPowerChip3_Equipped = false;

            player.DisablePower3();
        }
    }

    public void UnlockGear1()
    {
        if(!bGear1Unlocked)
        {
            Gear1Inventory_IconMissing.SetActive(false);
            Gear1Inventory_Icon.SetActive(true);
            Gear1Inventory_Button.GetComponent<Button>().interactable = true;
            bGear1Unlocked = true;
        }
    }

    public void UnlockGear2()
    {
        if (!bGear2Unlocked)
        {
            Gear2Inventory_IconMissing.SetActive(false);
            Gear2Inventory_Icon.SetActive(true);
            Gear2Inventory_Button.GetComponent<Button>().interactable = true;
            bGear2Unlocked = true;
        }
    }

    public void UnlockGear3()
    {
        if (!bGear3Unlocked)
        {
            Gear3Inventory_IconMissing.SetActive(false);
            Gear3Inventory_Icon.SetActive(true);
            Gear3Inventory_Button.GetComponent<Button>().interactable = true;
            bGear3Unlocked = true;
        }
    }

    public void UnlockChip1()
    {
        if (!bChip1Unlocked)
        {
            PCInventory1_IconMissing.SetActive(false);
            PCInventory1_Icon.SetActive(true);
            PCInventory1_Button.GetComponent<Button>().interactable = true;
            bChip1Unlocked = true;
        }
    }

    public void UnlockChip2()
    {
        if (!bChip2Unlocked)
        {
            PCInventory2_IconMissing.SetActive(false);
            PCInventory2_Icon.SetActive(true);
            PCInventory2_Button.GetComponent<Button>().interactable = true;
            bChip2Unlocked = true;
        }
    }

    public void UnlockChip3()
    {
        if (!bChip3Unlocked)
        {
            PCInventory3_IconMissing.SetActive(false);
            PCInventory3_Icon.SetActive(true);
            PCInventory3_Button.GetComponent<Button>().interactable = true;
            bChip3Unlocked = true;
        }
    }
}
