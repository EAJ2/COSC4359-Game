using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private string Class;


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
    public int heavyDMG;
    public int heavyCritDMG;
    public float dmgRed;


    //Level UP
    public int points;
    public int pointsSpent;
    public int pointsSpent_dex;
    public int pointsSpent_str;
    public int pointsSpent_fort;
    public int pointsSpent_wis;
    public int pointsSpent_end;
    public int pointsSpent_vit;
    public int pointsSpent_agil;
    public bool canLevel;
    public float XP;
    public float maxXP;
    public LevelUpBar levelBar;
    public GameObject levelUpIcon;
    public GameObject statsCanvas;

    //Stats Text
    public Text vitText;
    public Text strText;
    public Text endText;
    public Text fortText;
    public Text wisText;
    public Text dexText;
    public Text agilText;
    public Text pointsText;
    public Text levelText;
    public Text classText;

    //Stats Buttons
    public GameObject strArrowDown;
    public GameObject endArrowDown;
    public GameObject fortArrowDown;
    public GameObject vitArrowDown;
    public GameObject dexArrowDown;
    public GameObject agilArrowDown;
    public GameObject wisArrowDown;
    public GameObject strArrowUp;
    public GameObject vitArrowUp;
    public GameObject endArrowUp;
    public GameObject dexArrowUp;
    public GameObject agilArrowUp;
    public GameObject fortArrowUp;
    public GameObject wisArrowUp;

    //Currency
    public int gold;

    public float mana;
    public float MAXmana;

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
        //levelBar.SetMaxXP(maxXP);
        //levelBar.SetXP(XP);
        critDMG = (int)Mathf.Ceil((float)dmg * critMult);
        heavyDMG = (int)Mathf.Ceil((float)dmg * 1.25f);
        heavyCritDMG = (int)Mathf.Ceil(((float)dmg * 1.25f) * critMult);

    }

    // Update is called once per frame
    void Update()
    {
        CheckLevelUp();
        OpenStats();
        ButtonCheck();

        vitText.text = vit.ToString() + "/20";
        endText.text = end.ToString() + "/20";
        strText.text = str.ToString() + "/20";
        fortText.text = fort.ToString() + "/20";
        agilText.text = agil.ToString() + "/20";
        wisText.text = wis.ToString() + "/20";
        dexText.text = dex.ToString() + "/20";
        pointsText.text = "Points: " + points.ToString();
        classText.text = "Class: " + Class;
        levelText.text = "Level: " + level.ToString();
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

        if (wisMult > 0)
        {
            MAXmana = BaseMana + (wis * 5) + (BaseMana + (wis * 5)) * wisMult;
            mana = MAXmana;
        }
        else
        {
            MAXmana = BaseMana + (wis * 5);
            mana = MAXmana;
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

        if (fortMult > 0)
        {
            dmgRed = (fort * 1 + (fort * 1) * fortMult)/100;
        }
        else
        {
            dmgRed = (fort * 1)/100;
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
        levelBar.SetMaxXP(maxXP);
    }

    public void CheckLevelUp()
    {
        if (XP >= maxXP)
        {
            SetMaxXP();
            levelBar.SetXP(XP);
            level += 1;
            points += 3;
        }

        if (points > 0)
        {
            levelUpIcon.SetActive(true);
        }
        else
        {
            levelUpIcon.SetActive(false);
        }
    }

    public void OpenStats()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && statsCanvas.activeInHierarchy == false)
        {
            statsCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && statsCanvas.activeInHierarchy == true)
        {
            pointsSpent_str = 0;
            pointsSpent_dex = 0;
            pointsSpent_end = 0;
            pointsSpent_wis = 0;
            pointsSpent_vit = 0;
            pointsSpent_agil = 0;
            pointsSpent_fort = 0;
            statsCanvas.SetActive(false);
        }
    }










    public void SetClass(string val)
    {
        Class = val;
    }

    public string GetClass()
    {
        return Class;
    }

    public void SetVitality(int val)
    {
        vit = val;
    }

    public void SetStrength(int val)
    {
        str = val;
    }

    public void SetEndurance(int val)
    {
        end = val;
    }

    public void SetWisdom(int val)
    {
        wis = val;
    }

    public void SetFortitude(int val)
    {
        fort = val;
    }

    public void SetDexterity(int val)
    {
        dex = val;
    }

    public void SetAgility(int val)
    {
        agil = val;
    }

    public int GetVitality()
    {
        return vit;
    }

    public int GetStrength()
    {
        return str;
    }

    public int GetEndurance()
    {
        return end;
    }

    public int GetWisdom()
    {
        return wis;
    }

    public int GetFortitude()
    {
        return fort;
    }

    public int GetDexterity()
    {
        return dex;
    }

    public int GetAgility()
    {
        return agil;
    }

    public void AddStr()
    {
        if (points > 0 && str < 20)
        {
            str += 1;
            points -= 1;
            pointsSpent_str += 1;
        }
    }

    public void AddDex()
    {
        if (points > 0 && dex < 20)
        {
            dex += 1;
            points -= 1;
            pointsSpent_dex += 1;
        }
    }

    public void AddWis()
    {
        if (points > 0 && wis < 20)
        {
            wis += 1;
            points -= 1;
            pointsSpent_wis += 1;
        }
    }

    public void AddFort()
    {
        if (points > 0 && fort < 20)
        {
            fort += 1;
            points -= 1;
            pointsSpent_fort += 1;
        }
    }

    public void AddAgil()
    {
        if (points > 0 && agil < 20)
        {
            agil += 1;
            points -= 1;
            pointsSpent_agil += 1;
        }
    }
    public void AddVit()
    {
        if (points > 0 && vit < 20)
        {
            vit += 1;
            points -= 1;
            pointsSpent_vit += 1;
        }
    }

    public void AddEnd()
    {
        if (points > 0 && end < 20)
        {
            end += 1;
            points -= 1;
            pointsSpent_end += 1;
        }
    }

    public void SubStr()
    {
        if (pointsSpent_str > 0)
        {
            str -= 1;
            points += 1;
            pointsSpent_str -= 1;
        }
    }

    public void SubDex()
    {
        if (pointsSpent_dex > 0)
        {
            dex -= 1;
            points += 1;
            pointsSpent_dex -= 1;
        }
    }

    public void SubWis()
    {
        if (pointsSpent_wis > 0)
        {
            wis -= 1;
            points += 1;
            pointsSpent_wis -= 1;
        }
    }

    public void SubFort()
    {
        if (pointsSpent_fort > 0)
        {
            fort -= 1;
            points += 1;
            pointsSpent_fort -= 1;
        }
    }

    public void SubAgil()
    {
        if (pointsSpent_agil > 0)
        {
            agil -= 1;
            points += 1;
            pointsSpent_agil -= 1;
        }
    }
    public void SubVit()
    {
        if (pointsSpent_vit > 0)
        {
            vit -= 1;
            points += 1;
            pointsSpent_vit -= 1;
        }
    }

    public void SubEnd()
    {
        if (pointsSpent_end > 0)
        {
            end -= 1;
            points += 1;
            pointsSpent_end -= 1;
        }
    }

    public void ButtonCheck()
    {
        if (pointsSpent_vit > 0)
        {
            vitArrowDown.SetActive(true);
        }
        else
        {
            vitArrowDown.SetActive(false);
        }

        if (pointsSpent_dex > 0)
        {
            dexArrowDown.SetActive(true);
        }
        else
        {
            dexArrowDown.SetActive(false);
        }

        if (pointsSpent_end > 0)
        {
            endArrowDown.SetActive(true);
        }
        else
        {
            endArrowDown.SetActive(false);
        }

        if (pointsSpent_str > 0)
        {
            strArrowDown.SetActive(true);
        }
        else
        {
            strArrowDown.SetActive(false);
        }

        if (pointsSpent_fort > 0)
        {
            fortArrowDown.SetActive(true);
        }
        else
        {
            fortArrowDown.SetActive(false);
        }

        if (pointsSpent_agil > 0)
        {
            agilArrowDown.SetActive(true);
        }
        else
        {
            agilArrowDown.SetActive(false);
        }

        if (pointsSpent_wis > 0)
        {
            wisArrowDown.SetActive(true);
        }
        else
        {
            wisArrowDown.SetActive(false);
        }

        if (vit >= 20)
        {
            vitArrowUp.SetActive(false);
        }
        else if (points <= 0)
        {
            vitArrowUp.SetActive(false);
        }
        else
        {
            vitArrowUp.SetActive(true);
        }

        if (dex >= 20)
        {
            dexArrowUp.SetActive(false);
        }
        else if (points <= 0)
        {
            dexArrowUp.SetActive(false);
        }
        else
        {
            dexArrowUp.SetActive(true);
        }

        if (end >= 20)
        {
            endArrowUp.SetActive(false);
        }
        else if (points <= 0)
        {
            endArrowUp.SetActive(false);
        }
        else
        {
            endArrowUp.SetActive(true);
        }

        if (wis >= 20)
        {
            wisArrowUp.SetActive(false);
        }
        else if (points <= 0)
        {
            wisArrowUp.SetActive(false);
        }
        else
        {
            wisArrowUp.SetActive(true);
        }

        if (str >= 20)
        {
            strArrowUp.SetActive(false);
        }
        else if (points <= 0)
        {
            strArrowUp.SetActive(false);
        }
        else 
        {
            strArrowUp.SetActive(true);
        }

        if (fort >= 20)
        {
            fortArrowUp.SetActive(false);
        }
        else if (points <= 0)
        {
            fortArrowUp.SetActive(false);
        }
        else
        {
            fortArrowUp.SetActive(true);
        }

        if (agil >= 20)
        {
            agilArrowUp.SetActive(false);
        }
        else if (points <= 0)
        {
            agilArrowUp.SetActive(false);
        }
        else
        {
            agilArrowUp.SetActive(true);
        }
    }
}
