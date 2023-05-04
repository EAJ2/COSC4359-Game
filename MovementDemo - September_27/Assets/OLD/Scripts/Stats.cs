using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] public string Class;
    [SerializeField] public Text goldText;


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
    public static int BaseCritRange = 10;
    public static float BaseCritDMG = 1.25f;
    public static float BaseWalkSpeed = 3f;
    public static int BaseDMG = 5;

    public V2Health hpScript;
    public V2PlayerCombat combatScript;
    public V2PlayerMovement movementScript;

    public PlayerStaminaBar staminaBar;
    public ManaBar manaBar;

    private int GoldMultiplier = 1;
    private int NormalGoldMultiplier = 1;
    private int BoostGoldMultiplier = 2;
    
    private int NormalDamage;
    private int BoostDamage;

    private float XPMultiplier = 1;
    private float NormalXPMultiplier = 1;
    private float BoostXPMultiplier = 1.5f;


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
        manaBar.SetMaxMana(MAXmana);
        critDMG = (int)Mathf.Ceil((float)dmg * critMult);
        heavyDMG = (int)Mathf.Ceil((float)dmg * 1.25f);
        heavyCritDMG = (int)Mathf.Ceil(((float)dmg * 1.25f) * critMult);

        if(goldText == null)
        {
            Debug.Log("goldText serialization missing in Stats");
        }

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

        ManaTracker();
    }

    public void Vitality()
    {
        if (vit >= 5 && vit <= 9)
        {
            vitMult = 0.1f;
        }
        else if (vit >= 10 && vit <= 14)
        {
            vitMult = 0.2f;
        }
        else if (vit >= 15 && vit <= 19)
        {
            vitMult = 0.5f;
        }
        else if (vit >= 20)
        {
            vitMult = 1f;
        }

        if (vitMult > 0)
        {
            hpScript.MaxHealth = (hpScript.startingHealth + (vit * 5f)) + (vitMult * (hpScript.startingHealth + (vit * 5f)));
        }
        else
        {
            hpScript.MaxHealth = hpScript.startingHealth + (vit * 5f);
        }
        hpScript.CurrentHealth = hpScript.MaxHealth;
        hpScript.healthBar.SetMaxHealth(hpScript.MaxHealth);
    }

    public void Strength()
    {
        if (str >= 5 && str <= 9)
        {
            strMult = 0.05f;
        }
        else if (str >= 10 && str <= 14)
        {
            strMult = 0.1f;
        }
        else if (str >= 15 && str <= 19)
        {
            strMult = 0.2f;
        }
        else if (str >= 20)
        {
            strMult = 0.5f;
        }

        if (strMult > 0)
        {
            dmg = (int)(BaseDMG + (3 * str) + (BaseDMG + (3 * str)) * strMult);
            critDMG = (int)Mathf.Ceil((float)dmg * critMult);
        }
        else
        {
            dmg = BaseDMG + (2 * str);
            critDMG = (int)Mathf.Ceil((float)dmg * critMult);
        }
        
    }

    public void Endurance()
    {
        if (end >= 5 && end <= 9)
        {
            endMult = 0.1f;
        }
        else if (end >= 10 && end <= 14)
        {
            endMult = 0.25f;
        }
        else if (end >= 15 && end <= 19)
        {
            endMult = 0.5f;
        }
        else if (end >= 20)
        {
            endMult = 1f;
        }

        if (endMult > 0)
        {
            movementScript.stamina = (movementScript.stamina + (end * 3f)) + (endMult * (movementScript.stamina + (end * 3f)));
            movementScript.MAXstamina = (movementScript.MAXstamina + (end * 3f)) + (endMult * (movementScript.MAXstamina + (end * 3f)));
        }
        else
        {
            movementScript.stamina = movementScript.stamina + (end * 3f);
            movementScript.MAXstamina = movementScript.MAXstamina + (end * 3f);
        }

        staminaBar.SetMaxStamina(movementScript.MAXstamina);
        staminaBar.SetStamina(movementScript.stamina);
    }

    public void Wisdom()
    {
        if (wis >= 5 && wis <= 9)
        {
            wisMult = 0.05f;
        }
        else if (wis >= 10 && wis <= 14)
        {
            wisMult = 0.1f;
        }
        else if (wis >= 15 && wis <= 19)
        {
            wisMult = 0.2f;
        }
        else if (wis >= 20)
        {
            wisMult = 0.5f;
        }
        else
        {
            wisMult = 0f;
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
        if (fort >= 5 && fort <= 9)
        {
            fortMult = 0.05f;
        }
        else if (fort >= 10 && fort <= 14)
        {
            fortMult = 0.1f;
        }
        else if (fort >= 15 && fort <= 19)
        {
            fortMult = 0.15f;
        }
        else if (fort >= 20)
        {
            fortMult = 0.2f;
        }

        if (fortMult > 0)
        {
            dmgRed = (fort * 1 + (fort * 1) * fortMult)/100;
        }
        else
        {
            dmgRed = (fort * 1 + (fort * 1) * fortMult) / 100;
        }
    }

    public void Dexterity()
    {
        if (dex >= 5 && dex <= 9)
        {
            dexMult = 0.05f;
        }
        else if (dex == 10 && dex <= 14)
        {
            dexMult = 0.1f;
        }
        else if (dex == 15 && dex <= 19)
        {
            dexMult = 0.15f;
        }
        else if (dex >= 20)
        {
            dexMult = 0.2f;
        }

        if (dexMult > 0)
        {
            critRange = (int)(BaseCritRange + (dex * 1) + (BaseCritRange + (dex * 1)) * dexMult);
            critMult = BaseCritDMG + (dex * 0.02f) + ((BaseCritDMG + (dex * 0.02f)) * dexMult);
        }
        else
        {
            critRange = BaseCritRange + (dex * 1);
            critMult = BaseCritDMG + (dex * 0.01f);
        }
    }

    public void Agility()
    {
        if (agil >= 5 && agil <= 9)
        {
            agilMult = 0.02f;
        }
        else if (agil == 10 && agil <= 14)
        {
            agilMult = 0.04f;
        }
        else if (agil >= 15 && agil <= 19)
        {
            agilMult = 0.06f;
        }
        else if (agil >= 20)
        {
            agilMult = 0.08f;
        }

        if (agilMult > 0)
        {
            movementScript.WalkSpeed = (movementScript.WalkSpeed + (agil * 0.25f)) + (agilMult * (movementScript.WalkSpeed + (agil * 0.25f)));
            movementScript.SprintSpeed = (movementScript.SprintSpeed + (agil * 0.33f)) + (agilMult * (movementScript.SprintSpeed + (agil * 0.33f)));
        }
        else
        {
            movementScript.WalkSpeed = movementScript.WalkSpeed + (agil * 0.25f);
            movementScript.SprintSpeed = movementScript.SprintSpeed + (agil * 0.33f);
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
            CallStats();
        }
    }

    public void CallStats()
    {
        Vitality();
        Strength();
        Endurance();
        Fortitude();
        Wisdom();
        Dexterity();
        Agility();
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


    public void ManaTracker()
    {
        manaBar.SetMana(mana);

        if (mana < 0)
        {
            mana = 0;
        }
        else if (mana > MAXmana)
        {
            mana = MAXmana;
        }
    }

    public void SetGold(int g)
    {
        gold += g * GoldMultiplier;
        goldText.text = gold.ToString();
        GetComponent<Player>().GetGold(gold);
    }

    public void SetOriginalGold(int g)
    {
        gold = g;
        goldText.text = gold.ToString();
    }

    public int GetGold()
    {
        return gold;
    }

    public void RemoveGold(int g)
    {
        gold -= g;
    }

    public void EquipKnightArtifact()
    {
        GoldMultiplier = BoostGoldMultiplier;
    }

    public void UnequipKnightArtifact()
    {
        GoldMultiplier = NormalGoldMultiplier;
    }
    public void EquipWeapon()
    {
        NormalDamage = dmg;
        dmg = dmg + BoostDamage;
    }

    public void UnequipWeapon()
    {
        dmg = NormalDamage;
    }

    public void SetXP(float x)
    {
        XP += (x * XPMultiplier);
        levelBar.SetXP(XP);
        
    }

    public void EquipRangerArtifact()
    {
        XPMultiplier = BoostXPMultiplier;
    }

    public void UnequipRangerArtifact()
    {
        XPMultiplier = NormalXPMultiplier;
    }
}
