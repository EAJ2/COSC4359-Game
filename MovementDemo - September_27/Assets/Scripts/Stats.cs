using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    //Stats
    [SerializeField]
    public int level;
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

    public int dmg;
    public int critDMG;
    public float critMult;
    public int critRange = 10;

    //Level UP
    public int points;
    public bool canLevel;
    public float XP;
    public float maxXP;
    public LevelUpBar levelBar;

    //Currency
    public int gold;

    //IGNORE THESE VARIABLES THEIR FOR MEMORY 
    public static float BaseHP = 20f;
    public static float BaseStam = 15f;
    public static float BaseMana = 10f;
    public static float BaseCritCH = 0.1f;
    public static float BaseCritDMG = 1.25f;
    public static float BaseWalkSpeed = 3f;


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
        levelBar.SetMaxXP(maxXP);
        levelBar.SetXP(XP);
        critDMG = (int)Mathf.Ceil((float)dmg * critMult);
    }

    // Update is called once per frame
    void Update()
    {
        levelBar.SetXP(XP);
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

        if (strMult > 0)
        {
            dmg = (int)(dmg + (2 * str) + (dmg + (2 * str)) * strMult);
            critDMG = (int)Mathf.Ceil((float)dmg * critMult);
        }
        else
        {
            dmg = dmg + (2 * str);
            critDMG = (int)Mathf.Ceil((float)dmg * critMult);
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

        if (dexMult > 0)
        {
            critRange = (int)(critRange + (dex * 1) + (critRange + (dex * 1)) * dexMult);
            critMult = BaseCritDMG + (dex * 0.02f) + ((BaseCritDMG + (dex * 0.02f)) * dexMult);
        }
        else
        {
            critRange = critRange + (dex * 1);
            critMult = BaseCritDMG + (dex * 0.01f);
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
        maxXP *= 1.5f;
    }
}
