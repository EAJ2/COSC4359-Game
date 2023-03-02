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

    private bool bIsThereSave;
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
            return;
        }
        else
        {
            Debug.Log("There is a Save for the player, Player.cs");

            ClassName = data.ClassName;

            bIsThereSave = true;
            bTutorialDone = data.bTutorialDone;

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
}
