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
}
