using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class V2PlayerData
{
    public string ClassName;

    public float VitalityStat;
    public float StrengthStat;
    public float EnduranceStat;
    public float WisdomStat;
    public float FortitudeStat;
    public float DexterityStat;
    public float AgilityStat;


    public string LevelName;
    public float Health;
    public float[] position;

    public bool bDash;
    public bool bJump;

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
    }
}
