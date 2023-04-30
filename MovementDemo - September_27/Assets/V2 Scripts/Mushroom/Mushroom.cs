using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private Player player;
    [SerializeField] private bool bMoveRightFirst = false;

    [Header("Patrol Points")]
    public List<Transform> Waypoints;
    private int target = 0;

    [Header("Respawn Parameters")]
    [SerializeField] private float RespawnTime;
    [SerializeField] private bool bCanRespawn = true;
    private float RespawnTimer;
    private bool bRespawning = false;

    [Header("Rewards")]
    [SerializeField] private int xpValue;
    [SerializeField] private int goldValue;
    private LevelUpBar xpBar;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float DealDamageRange;
    private float cooldownTimer = 0;

    [Header("Collider Parameters")]
    private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float colliderDistanceChase;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("Movement Parameter")]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpPower;
    private bool movingLeft;
    private Vector3 initScale;

    private bool bPlayerHit = false;

    [SerializeField] private bool bCanMove = true;



    private void Awake()
    {
        if (player == null)
        {
            Debug.Log("Player missing in the Mushroom");
        }
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        RespawnTimer = 0;
        xpBar = player.GetComponent<LevelUpBar>();
        initScale = transform.localScale;
        if(bMoveRightFirst)
        {
            target = 1;
        }
        else
        {
            target = 0;
        }
        anim.SetBool("Run", true);
    }

    private void Update()
    {
        if (bCanMove)
        {
            cooldownTimer += Time.deltaTime;
            if(IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            }
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(Waypoints[target].position.x, transform.position.y, transform.position.z), Speed * Time.deltaTime);
            if(target == 0)
            {
                rb.velocity = new Vector2(-1 * Speed, rb.velocity.y);
                ChangePosition();
                MoveInDirection();
            }
            else
            {
                rb.velocity = new Vector2(1 * Speed, rb.velocity.y);
                ChangePosition();
                MoveInDirection();
            }
        }
    }

    public void ChangeTargetIfHit()
    {
        if (target == 0)
        {
            target = 1;
        }
        else
        {
            target = 0;
        }
        bPlayerHit = false;
    }

    private void ChangePosition()
    {
        if (target == 0)
        {
            if(transform.position.x <= Waypoints[0].position.x)
            {
                target = 1;
            }
        }
        else if (target == 1)
        {
            if(transform.position.x >= Waypoints[1].position.x)
            {
                target = 0;
            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        //anim.SetBool("isGrounded", true);
        //Debug.Log("On the Ground");
        return raycastHit.collider != null;
    }

    private void MoveInDirection()
    {
        if(target == 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y, initScale.z);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("TakeDmg");
        }
        else
        {
            //player dead
            if (!dead)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        //GetComponent<Collider2D>().enabled = false;

        player.GetComponent<Stats>().XP += xpValue;
        xpBar.SetXP(player.GetComponent<Stats>().XP);

        player.GetComponent<Stats>().gold += goldValue;

        
        dead = true;
        bCanMove = false;
    }

    private void Deactivate()
    {
        if (bCanRespawn)
        {
            bRespawning = true;
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void Respawn()
    {
        anim.SetTrigger("Respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        RespawnTimer = 0;
        bRespawning = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        dead = false;
        bCanMove = true;
        currentHealth = startingHealth;
    }

    public bool IsDead()
    {
        return dead;
    }

    public void EnableMove()
    {
        bCanMove = true;
    }

    public void DisableMove()
    {
        bCanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(cooldownTimer >= attackCooldown)
            {
                anim.SetTrigger("Attack2");
                player.GetComponent<V2Health>().TakeDmg(damage);
                cooldownTimer = 0;
            }
        }
    }
}
