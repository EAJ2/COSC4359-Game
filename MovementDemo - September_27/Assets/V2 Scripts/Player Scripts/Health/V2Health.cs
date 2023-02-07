using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2Health : MonoBehaviour
{
    private V2PlayerMovement pm;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    private float MaxHealth;
    public float CurrentHealth;
    private Animator anim;
    private bool bDead;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int NumberOfFlashes;
    private SpriteRenderer sr;

    public Stats stats;
    public PlayerHealthBar healthBar;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        stats.Vitality();
        MaxHealth = startingHealth + (stats.vit * 5);
        if (stats.vitMult > 0)
        {
            MaxHealth = (startingHealth + (stats.vit * 5)) + (stats.vitMult * (startingHealth + (stats.vit * 5)));
        }
        else
        {
            MaxHealth = startingHealth + (stats.vit * 5);
        }
        CurrentHealth = MaxHealth;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pm = GetComponentInParent<V2PlayerMovement>();
        healthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDmg(float dmg)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - dmg, 0, startingHealth);

        if (CurrentHealth > 0)
        {

        }
        else
        {
            if(!bDead)
            {
                pm.enabled = false;
                bDead = true;
            }
        }
    }

    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, startingHealth);
    }

    public void SetHealth(float value)
    {
        CurrentHealth = value;
    }


    public float GetHealth()
    {
        return CurrentHealth;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = startingHealth;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for(int i = 0; i < NumberOfFlashes; i++)
        {
            sr.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (NumberOfFlashes * 2));
            sr.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (NumberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public void Reset()
    {
        CurrentHealth = startingHealth;
        bDead = false;
        pm.enabled = true;
    }
}
