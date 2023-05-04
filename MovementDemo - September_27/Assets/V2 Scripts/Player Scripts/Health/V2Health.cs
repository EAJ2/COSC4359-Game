using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2Health : MonoBehaviour
{
    private V2PlayerMovement pm;

    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float MaxHealth;
    public float CurrentHealth;
    private Animator anim;
    private bool bDead = false;
    [SerializeField] private float BoostMaxHealth;
    private float NormalMaxHealth;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int NumberOfFlashes;
    private SpriteRenderer sr;

    public Stats stats;
    public PlayerHealthBar healthBar;
    public V2PlayerCombat comb;
    public V2PlayerMovement mov;
    public RangerCombat rc;

    [Header("SpawnPoints")]
    private Vector3 RespawnPoint;

    private bool bCanTakeDamage = true;
    private void Awake()
    {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pm = GetComponentInParent<V2PlayerMovement>();
        healthBar.SetMaxHealth(MaxHealth);

        NormalMaxHealth = MaxHealth;
    }

    void Update()
    {
        Die();
        HealthCheck();
    }


    public void TakeDmg(float dmg)
    {
        if (bCanTakeDamage == true)
        {
            CurrentHealth -= dmg - (dmg * stats.dmgRed);
            healthBar.SetHealth(CurrentHealth);
            if (CurrentHealth > 0)
            {
                if (GetComponent<Player>().GetClassName() == "Vagabond" || GetComponent<Player>().GetClassName() == "Ranger")
                {
                    anim.SetTrigger("Hit");
                }
            }
            else
            {
                bDead = true;
                Die();
            }
        }
    }

    public void PlayHitAnim()
    {
        anim.SetTrigger("Hit");
    }

    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, startingHealth);
        healthBar.SetHealth(CurrentHealth);
    }

    public void SetHealth(float value)
    {
        CurrentHealth = value;
    }


    public float GetHealth()
    {
        return CurrentHealth;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < NumberOfFlashes; i++)
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
        RespawnAtPoint();
        this.GetComponent<V2PlayerMovement>().EnableMovement();
        this.GetComponent<V2PlayerMovement>().ResetStamina();
        CurrentHealth = MaxHealth;
        healthBar.SetHealth(CurrentHealth);
        bCanTakeDamage = true;
        bDead = false;
        comb.enabled = true;
        rc.enabled = true;
    }

    public void Die()
    {
        if (CurrentHealth <= 0)
        {
            bCanTakeDamage = false;
            bDead = true;
            if(GetComponent<Player>().GetClassName() == "Vagabond" || GetComponent<Player>().GetClassName() == "Ranger")
            {
                anim.SetTrigger("Dead");
                this.enabled = false;
                this.GetComponent<V2PlayerMovement>().DisableMovement();
                comb.enabled = false;
                rc.enabled = false;
            }
            else
            {
                Reset();
            }
        }
    }

    public void HealthCheck()
    {
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public bool IsDead()
    {
        return bDead;
    }

    public void SetNewRespawnPoint(Vector3 position)
    {
        RespawnPoint = position;
    }

    private void RespawnAtPoint()
    {
        transform.position = new Vector3(RespawnPoint.x, RespawnPoint.y, transform.position.z);
    }

    public void WarriorChestEquip()
    {
        MaxHealth = BoostMaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(CurrentHealth);
    }

    public void WarriorChestUnequip()
    {
        MaxHealth = NormalMaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        if(CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        healthBar.SetHealth(CurrentHealth);
    }
}