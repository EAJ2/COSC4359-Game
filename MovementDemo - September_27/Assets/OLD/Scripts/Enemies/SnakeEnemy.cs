using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float PushForce;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;

    [Header("Dash")]
    [SerializeField] private float DashingPower = 24f;
    private float DashingCooldown = 1f;

    private Transform body;
    private Rigidbody2D rb;
    private Animator anim;
    private SnakePatrol snakePatrol;
    private bool bPlayerSeen = false;
    private Vector3 PlayerPosition;
    private bool bPlayerHit = false;


    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private float GetHitRange;
    [SerializeField] private float GetHitColliderDistance;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("Knockback")]
    private bool bHit = false;
    [SerializeField] private float waitAfterHitTime;

    [SerializeField] private GameObject healthPickup;
    [SerializeField] private ExoV3Movement player;

    [SerializeField] private bool bCanMove = true;

    private void Awake()
    {
        body = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        snakePatrol = GetComponentInParent<SnakePatrol>();
        currentHealth = startingHealth;

        if(!bCanMove)
        {
            snakePatrol.DisableMove();
        }
    }

    private void Update()
    {
        DashingCooldown += Time.deltaTime;

        if (bCanMove)
        {
            //Attack only when player in sight
            if (PlayerInSight() && !bPlayerSeen && !bHit)
            {
                if (DashingCooldown >= attackCooldown)
                {
                    //Attack
                    DashingCooldown = 0;
                    bPlayerSeen = true;

                    PlayerPosition = player.transform.position;
                    anim.SetTrigger("meleeAttack");
                }
            }
            if (bPlayerSeen)
            {
                anim.SetBool("attacking", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerPosition.x, transform.position.y, transform.position.z), DashingPower * Time.deltaTime);
            }
            if ((transform.position.x == PlayerPosition.x && bPlayerSeen) || bPlayerHit)
            {
                snakePatrol.ChangeDirection();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(0f, 0f);
                if (bPlayerHit)
                {
                    bPlayerHit = false;
                }
                bPlayerSeen = false;
                anim.SetBool("attacking", false);
            }

            //Knockback
            if (bHit)
            {
                StartCoroutine(WaitAfterHit());
            }

            //Disable patrol to attack
            if (snakePatrol != null)
            {
                if (bPlayerSeen || bHit)
                {
                    snakePatrol.enabled = false;
                }
                else
                {
                    snakePatrol.enabled = true;
                }
            }
        }
    }
    private IEnumerator WaitAfterHit()
    {
        yield return new WaitForSecondsRealtime(waitAfterHitTime);
        if(rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = Vector2.zero;
        }
        bHit = false;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * (-1* transform.localScale.x) * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    public bool PlayerLeft()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * GetHitRange * -1 * GetHitColliderDistance, new Vector3(boxCollider.bounds.size.x * GetHitRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    public bool PlayerRight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * GetHitRange * GetHitColliderDistance, new Vector3(boxCollider.bounds.size.x * GetHitRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * (-1 * transform.localScale.x) * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * GetHitRange * -1 * GetHitColliderDistance, new Vector3(boxCollider.bounds.size.x * GetHitRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * GetHitRange * GetHitColliderDistance, new Vector3(boxCollider.bounds.size.x * GetHitRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void TakeDamage(float _damage)
    {
        bPlayerSeen = false;
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
        GetComponentInParent<SnakePatrol>().enabled = false;
        this.enabled = false;
        dead = true;
        Vector2 healthPickUpVector = new Vector2(rb.position.x, rb.position.y);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.bodyType = RigidbodyType2D.Static;

            if (bPlayerSeen)
            {
                collision.gameObject.GetComponent<ExoV3Movement>().TakeDamage(damage);
                //player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x * PushForce, player.GetComponent<Rigidbody2D>().velocity.y);
                DashingCooldown = 0;
                bPlayerHit = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void EnableMove()
    {
        bCanMove = true;
        snakePatrol.EnableMove();
    }

    public void DisableMove()
    {
        bCanMove = false;
        snakePatrol.DisableMove();
    }
}
