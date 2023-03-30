using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    #region Singleton
    public static StatusManager instance;
    public static Stats stats;
    public static GameObject player;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player");
            stats = player.GetComponent<Stats>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion


    public void UpdateCharacterStatus(Equipment newItem, Equipment oldItem)
    {
        if(oldItem != null) 
        {
            stats.SetVitality(1);
            stats.SetStrength(1);
            stats.SetEndurance(1); 
            stats.SetWisdom(1);
            stats.SetFortitude(1);
            stats.SetDexterity(1); 
            stats.SetAgility(1);
        }

        stats.SetVitality(1 + newItem.vitModifier);
        stats.SetStrength(1 + newItem.strModifier);
        stats.SetEndurance(1 + newItem.endModifier); 
        stats.SetWisdom(1 + newItem.wisModifier);
        stats.SetFortitude(1 + newItem.fortModifier);
        stats.SetDexterity(1 + newItem.dexModifier); 
        stats.SetAgility(1 + newItem.agilModifier);  
    }
}