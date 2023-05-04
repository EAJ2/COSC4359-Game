using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float PlayerRange;

    [Header("Rewards")]
    [SerializeField] private int xpValue;
    [SerializeField] private int goldValue;
    private LevelUpBar xpBar;

    [Header("Collider Params")]
    [SerializeField] private float colliderRadius;
    [SerializeField] private float PlayerColliderRadius;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("Movement Parameters")]
    [SerializeField] private float ChaseMovingSpeed;

    private Animator anim;
    private FlyPatrol flyPatrol;

    [Header("Knockback")]
    private bool bHit = false;
    [SerializeField] private float waitAfterHitTime;

    [SerializeField] private Player player;
    private bool bPlayerHit = false;

    [SerializeField] private bool bCanMove = true;

    [Header("Respawn Parameters")]
    [SerializeField] private float RespawnTime;
    private float RespawnTimer;
    private bool bRespawning = false;
    [SerializeField] private bool bCanRespawn = true;

    //Ranger
    public bool inVolley = false;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.Log("Player missing in the FlyEnemy Bat");
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        flyPatrol = GetComponentInParent<FlyPatrol>();
        circleCollider = GetComponent<CircleCollider2D>();
        currentHealth = startingHealth;
        rb.gravityScale = 0f;

        xpBar = player.GetComponent<LevelUpBar>();
        RespawnTimer = 0;

        if(!bCanMove)
        {
            flyPatrol.DisableMove();
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (bCanMove && !dead)
        {
            if (PlayerInSight() && !bHit && !bPlayerHit)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    ChasePlayer();
                }
            }
            if (bHit)
            {
                StartCoroutine(WaitAfterHit());
            }
            if (flyPatrol != null)
            {
                if (PlayerInSight() && !bPlayerHit)
                {
                    flyPatrol.enabled = false;
                }
                else if (bPlayerHit)
                {
                    flyPatrol.enabled = true;
                    if (bPlayerHit && cooldownTimer >= attackCooldown)
                    {
                        bPlayerHit = false;
                    }
                }
                else if (!PlayerInSight())
                {
                    flyPatrol.enabled = true;
                }
            }
        }

        if (bRespawning)
        {
            if (RespawnTimer >= RespawnTime)
            {
                Respawn();
            }
            else
            {
                RespawnTimer += Time.deltaTime;
            }
        }
    }

    private IEnumerator WaitAfterHit()
    {
        yield return new WaitForSecondsRealtime(waitAfterHitTime);
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = Vector2.zero;
        }
        bHit = false;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.CircleCast(circleCollider.bounds.center + transform.right * range * (-1 * transform.localScale.x) * colliderRadius, colliderRadius, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    public bool PlayerLeft()
    {
        RaycastHit2D hit = Physics2D.CircleCast(circleCollider.bounds.center + transform.right * PlayerRange * -1 * PlayerColliderRadius, PlayerColliderRadius, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    public bool PlayerRight()
    {
        RaycastHit2D hit = Physics2D.CircleCast(circleCollider.bounds.center + transform.right * PlayerRange * 1 * PlayerColliderRadius, PlayerColliderRadius, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCollider.bounds.center + transform.right * range * (-1 * transform.localScale.x) * colliderRadius, colliderRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(circleCollider.bounds.center + transform.right * PlayerRange * -1 * PlayerColliderRadius, PlayerColliderRadius);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(circleCollider.bounds.center + transform.right * PlayerRange * 1 * PlayerColliderRadius, PlayerColliderRadius);
    }


    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, ChaseMovingSpeed * Time.deltaTime);
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
            bHit = true;
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

        player.GetComponent<Stats>().SetXP(xpValue);
        xpBar.SetXP(player.GetComponent<Stats>().XP);
        player.GetComponent<Stats>().SetGold(goldValue);

        GetComponentInParent<FlyPatrol>().enabled = false;
        dead = true;
        bCanMove = false;
    }

    public void Deactivate()
    {
        if(bCanRespawn)
        {
            bRespawning = true;
        }
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void Respawn()
    {
        anim.SetTrigger("Respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        RespawnTimer = 0;
        bRespawning = false;
        this.GetComponent<CircleCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponentInParent<FlyPatrol>().enabled = true;
        dead = false;
        bCanMove = true;
        ResetHealth();
    }

    public bool IsDead()
    {
        return dead;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Attack");
            rb.bodyType = RigidbodyType2D.Static;
            if(cooldownTimer >= attackCooldown)
            {
                collision.gameObject.GetComponent<V2Health>().TakeDmg(damage);
                cooldownTimer = 0;
                bPlayerHit = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0f;
        }
    }

    public void EnableMove()
    {
        bCanMove = true;
        flyPatrol.EnableMove();
    }

    public void DisableMove()
    {
        bCanMove = false;
        flyPatrol.DisableMove();
    }

    private void ResetHealth()
    {
        currentHealth = startingHealth;
    }
}
