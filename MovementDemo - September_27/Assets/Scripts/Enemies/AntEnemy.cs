using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEnemy : MonoBehaviour
{
    private Animator anim;
    private ExoV3Movement player;
    [SerializeField] private Transform body;
    [SerializeField] private GameObject healthPickup;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Health")]
    [SerializeField] private float startingHealth;

    [SerializeField] private bool bCanMove = true;

    public float currentHealth { get; private set; }
    private bool dead;
    

    private AntPatrol antPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        antPatrol = GetComponentInParent<AntPatrol>();
        currentHealth = startingHealth;

        if(!bCanMove)
        {
            antPatrol.DisableMove();
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //boxCollider.isTrigger = true;
            if(cooldownTimer >= attackCooldown)
            {
                collision.gameObject.GetComponent<ExoV3Movement>().TakeDamage(damage);
                cooldownTimer = 0;
            }
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if(!dead)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        anim.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        GetComponentInParent<AntPatrol>().enabled = false;
        this.enabled = false;
        dead = true;
        Vector2 healthPickUpVector = new Vector2(body.position.x, body.position.y);
        Instantiate(healthPickup, healthPickUpVector, Quaternion.identity);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return dead;
    }

    public void EnableMove()
    {
        bCanMove = true;
        antPatrol.EnableMove();
    }

    public void DisableMove()
    {
        bCanMove = false;
        antPatrol.DisableMove();
    }
}
