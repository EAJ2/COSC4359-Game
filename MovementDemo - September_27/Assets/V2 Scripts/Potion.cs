using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string potionType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthPotion(float hp)
    {
        hp += 30;
    }
    
    public void StaminaPotion(float stamina)
    {
        stamina += 20;
    }

    public void ManaPotion(float mana)
    {
        mana += 15;
    }

}
