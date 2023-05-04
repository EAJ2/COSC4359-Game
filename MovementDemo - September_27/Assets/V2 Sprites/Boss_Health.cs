using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Animator anim;


    public bool isDead = false;
    public bool Stage2 = false;

    public Slider healthBar;
    public PlayerHealthBar enemyHealthBarScript;

    public GameObject stage2Text;
    public GameObject endGameText;

    public GameObject Door;
    public GameObject SceneChanger;

    //Ranger
    public bool inVolley = false;

    private bool bDead = false;

    // Update is called once per frame
    void Update()
    {
        enemyHealthBarScript.SetMaxHealth(maxHealth);
        enemyHealthBarScript.SetHealth(health);

        if (health <= 0 && Stage2 == false)
        {
            Stage2 = true;
            maxHealth = 30;
            health = 30;
            anim.SetBool("Stage 2", true);
            stage2Text.SetActive(true);
        }

        DeathCheck();
    }


    public void TakeDMG(int dmg)
    {
        health -= dmg;
        anim.SetTrigger("Hurt");
    }

    void DeathCheck()
    {
        if (health <= 0 && isDead == false && Stage2 == true)
        {
            bDead = true;
            Door.SetActive(false);
            anim.SetBool("isDead", true);
            isDead = true;
            //endGameText.SetActive(true);
            SceneChanger.SetActive(true);
        }
    }

    public bool IsDead()
    {
        return bDead;
    }
}
