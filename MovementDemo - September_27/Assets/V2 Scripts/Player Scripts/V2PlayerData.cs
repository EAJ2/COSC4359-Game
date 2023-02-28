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

    public V2PlayerData(Player player)
    {
        ClassName = player.GetComponent<Stats>().GetClass();
        VitalityStat = player.GetComponent<Stats>().GetVitality();
        StrengthStat = player.GetComponent<Stats>().GetStrength();
        EnduranceStat = player.GetComponent<Stats>().GetEndurance();
        WisdomStat = player.GetComponent<Stats>().GetWisdom();
        FortitudeStat = player.GetComponent<Stats>().GetFortitude();
        DexterityStat = player.GetComponent<Stats>().GetDexterity();
        AgilityStat = player.GetComponent<Stats>().GetAgility();

        Health = player.GetComponent<V2Health>().GetHealth();

        bDash = player.GetComponent<V2PlayerMovement>().CanDash();
        bJump = player.GetComponent<V2PlayerMovement>().CanJump();

        bTutorialDone = player.GetIsTutorialDone();
    }
}
