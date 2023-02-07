using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class V2PlayerData
{
    public string ClassName;

    public float Intelligence;
    public float Strength;

    public float Dexterity;
    public float Sneaky;


    public string LevelName;
    public float Health;
    public float[] position;

    public bool bDash;
    public bool bJump;

    public V2PlayerData(Player player)
    {


        /*
>>>>>>> Stashed changes
        /*
        ClassName = player.GetComponent<ClassStats>().GetClass();
        Intelligence = player.GetComponent<ClassStats>().GetIntelligence();
        Strength = player.GetComponent<ClassStats>().GetStrength();
        Dexterity = player.GetComponent<ClassStats>().GetDexterity();
        Sneaky = player.GetComponent<ClassStats>().GetSneaky();

        Health = player.GetComponent<V2Health>().GetHealth();

        bDash = player.GetComponent<V2PlayerMovement>().CanDash();
        bJump = player.GetComponent<V2PlayerMovement>().CanJump();
<<<<<<< Updated upstream
=======
        */
    }
}
