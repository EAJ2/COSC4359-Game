using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    //Stats
    [SerializeField]
    public int vit;
    public float vitMult;
    public int str;
    public float strMult;
    public int end;
    public float endMult;
    public int wis;
    public float wisMult;
    public int fort;
    public float fortMult;
    public int dex;
    public float dexMult;
    public int agil;
    public float agilMult;

    public int points;
    public bool canLevel;


    public int XP;
    public double maxXP;

    public int gold;

    //IGNORE THESE VARIABLES THEIR FOR MEMORY 
    public float BaseHP = 20f;
    public float BaseStam = 15f;
    public float BaseMana = 10f;
    public float BaseCritCH = 0.1f;
    public float BaseCritDMG = 1.25f;
    public float BaseWalkSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        Vitality();
        Strength();
        Endurance();
        Wisdom();
        Fortitude();
        Dexterity();
        Agility();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Vitality()
    {
        if (vit == 5)
        {
            vitMult = 0.1f;
        }
        else if (vit == 10)
        {
            vitMult = 0.2f;
        }
        else if (vit == 15)
        {
            vitMult = 0.5f;
        }
        else if (vit == 20)
        {
            vitMult = 1f;
        }
    }

    public void Strength()
    {
        if (str == 5)
        {
            strMult = 0.05f;
        }
        else if (str == 10)
        {
            strMult = 0.1f;
        }
        else if (str == 15)
        {
            strMult = 0.2f;
        }
        else if (str == 20)
        {
            strMult = 0.5f;
        }
    }

    public void Endurance()
    {
        if (end == 5)
        {
            endMult = 0.1f;
        }
        else if (end == 10)
        {
            endMult = 0.25f;
        }
        else if (end == 15)
        {
            endMult = 0.5f;
        }
        else if (end == 20)
        {
            endMult = 1f;
        }
    }

    public void Wisdom()
    {
        if (wis == 5)
        {
            wisMult = 0.05f;
        }
        else if (wis == 10)
        {
            wisMult = 0.1f;
        }
        else if (wis == 15)
        {
            wisMult = 0.2f;
        }
        else if (wis == 20)
        {
            wisMult = 0.5f;
        }
    }

    public void Fortitude()
    {
        if (fort == 5)
        {
            fortMult = 0.05f;
        }
        else if (fort == 10)
        {
            fortMult = 0.1f;
        }
        else if (fort == 15)
        {
            fortMult = 0.15f;
        }
        else if (fort == 20)
        {
            fortMult = 0.2f;
        }
    }

    public void Dexterity()
    {
        if (dex == 5)
        {
            dexMult = 0.05f;
        }
        else if (dex == 10)
        {
            dexMult = 0.1f;
        }
        else if (dex == 15)
        {
            dexMult = 0.15f;
        }
        else if (dex == 20)
        {
            dexMult = 0.2f;
        }
    }

    public void Agility()
    {
        if (agil == 5)
        {
            agilMult = 0.02f;
        }
        else if (agil == 10)
        {
            agilMult = 0.04f;
        }
        else if (agil == 15)
        {
            agilMult = 0.06f;
        }
        else if (agil == 20)
        {
            agilMult = 0.08f;
        }
    }

    public void SetMaxXP()
    {
        maxXP *= 1.5;
    }
}
