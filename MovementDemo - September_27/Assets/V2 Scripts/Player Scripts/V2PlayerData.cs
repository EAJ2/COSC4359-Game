using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class V2PlayerData 
{
    public string ClassName;

    public int VitalityStat;
    public int StrengthStat;
    public int EnduranceStat;
    public int WisdomStat;
    public int FortitudeStat;
    public int DexterityStat;
    public int AgilityStat;


    public string LevelName;
    public float Health;
    public float[] position;

    public bool bDash;
    public bool bJump;

    public bool bTutorialDone;

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

    public V2PlayerData(Player player)
    {
        ClassName = player.GetClassName();
        VitalityStat = player.GetComponent<Stats>().GetVitality();
        StrengthStat = player.GetComponent<Stats>().GetStrength();
        EnduranceStat = player.GetComponent<Stats>().GetEndurance();
        WisdomStat = player.GetComponent<Stats>().GetWisdom();
        FortitudeStat = player.GetComponent<Stats>().GetFortitude();
        DexterityStat = player.GetComponent<Stats>().GetDexterity();
        AgilityStat = player.GetComponent<Stats>().GetAgility();

        Health = player.GetComponent<V2Health>().GetHealth();

        bHeadUnlocked = player.bHeadUnlocked;
        bChestUnlocked = player.bChestUnlocked;
        bLegsUnlocked = player.bLegsUnlocked;
        bShoesUnlocked = player.bShoesUnlocked;
        bWeaponUnlocked = player.bWeaponUnlocked;
        bArtifactUnlocked = player.bArtifactUnlocked;

        bAbility1Unlocked = player.bAbility1Unlocked;
        bAbility2Unlocked = player.bAbility2Unlocked;
        bAbility3Unlocked = player.bAbility3Unlocked;
        bAbility4Unlocked = player.bAbility4Unlocked;



        bDash = player.GetComponent<V2PlayerMovement>().CanDash();
        bJump = player.GetComponent<V2PlayerMovement>().CanJump();

        bTutorialDone = player.GetIsTutorialDone();
    }
}
