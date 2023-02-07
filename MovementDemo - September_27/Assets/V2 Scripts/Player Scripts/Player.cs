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
    private ClassStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        pm = GetComponent<V2PlayerMovement>();
        health = GetComponent<V2Health>();
        stats = GetComponent<ClassStats>();
    }

    //Save Game
    public void SavePlayer()
    {
        SaveClassInformation.SavePlayer(this);
    }

    
    public void LoadPlayer()
    {
        /*
        V2PlayerData data = SaveClassInformation.LoadPlayer();
        if (data == null)
        {
            return;
        }
        else
        {
<<<<<<< Updated upstream
=======
            
>>>>>>> Stashed changes
            stats.SetClass(data.ClassName);
            stats.SetIntelligence(data.Intelligence);
            stats.SetStrength(data.Strength);
            stats.SetDexterity(data.Dexterity);
            stats.SetSneaky(data.Sneaky);

            health.SetHealth(data.Health);
<<<<<<< Updated upstream

            if(data.bDash == true)
=======
            
            if (data.bDash == true)
>>>>>>> Stashed changes
            {
                pm.EnableDash();
            }

<<<<<<< Updated upstream
            if(data.bJump == true)
=======
            if (data.bJump == true)
>>>>>>> Stashed changes
            {
                pm.EnableJump();
            }
        }
        
    }
<<<<<<< Updated upstream
=======
        

    private void SetClassData()
    {

    }
>>>>>>> Stashed changes
        */
}
