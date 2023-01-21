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

    [Header("Collider Parameters")]
    [SerializeField] private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    [SerializeField] private float colliderRadius;
    [SerializeField] private float PlayerColliderRadius;

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

    [SerializeField] private GameObject healthPickup;
    [SerializeField] private ExoV3Movement player;
    private bool bPlayerHit = false;

    [SerializeField] private bool bCanMove = true;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        flyPatrol = GetComponentInParent<FlyPatrol>();
        currentHealth = startingHealth;
        rb.gravityScale = 0f;

        if(!bCanMove)
        {
            flyPatrol.DisableMove();
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (bCanMove)
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
            anim.SetTrigger("hurt");
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
        anim.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        GetComponentInParent<FlyPatrol>().enabled = false;
        this.enabled = false;
        dead = true;
        Vector2 healthPickUpVector = new Vector2(rb.position.x, rb.position.y);
        Instantiate(healthPickup, healthPickUpVector, Quaternion.identity);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void ReActivate()
    {
        gameObject.SetActive(true);
        GetComponent<Collider2D>().enabled = true;
        GetComponentInParent<FlyPatrol>().enabled = true;
        this.enabled = true;
        dead = false;
        currentHealth = 2;
    }

    public bool IsDead()
    {
        return dead;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.bodyType = RigidbodyType2D.Static;
            if(cooldownTimer >= attackCooldown)
            {
                collision.gameObject.GetComponent<ExoV3Movement>().TakeDamage(damage);
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
}
