using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private Animator anim;
    private ExoV3Movement player;
    private Transform body;
    private Rigidbody2D rb;
    [SerializeField] private GameObject healthPickup;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float rangeChase;
    [SerializeField] private float rangePlayer;
    [SerializeField] private float chaseCooldown;
    private float cooldownTimer = 0;
    private float chaseTimer = 0;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float colliderDistanceChase;
    [SerializeField] private float colliderDistancePlayer;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("Knockback")]
    private bool bHit = false;
    [SerializeField] private float waitAfterHitTime;

    [Header("Movement Parameter")]
    [SerializeField] private float chaseSpeed;
    private Vector3 enemyPosition;

    [Header("Change Direction After Hit")]
    [SerializeField] private float waitTime;

    private GroundPatrol groundPatrol;

    private bool bPlayerSeen = false;
    private Vector3 PlayerPosition;
    private bool bPlayerHit = false;

    [SerializeField] private bool bCanMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        groundPatrol = GetComponentInParent<GroundPatrol>();
        body = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        chaseTimer = chaseCooldown;

        if(!bCanMove)
        {
            groundPatrol.DisableMove();
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        chaseTimer += Time.deltaTime;

        if(bCanMove)
        {

            if (bPlayerHit)
            {
                StartCoroutine(ChangeDir());
                bPlayerHit = false;
            }
            if (PlayerInSight() && !bHit)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    bPlayerSeen = false;
                    anim.SetBool("running", false);
                    chaseTimer = 0;
                    anim.SetTrigger("attack");
                }
            }
            if (PlayerInSightChase() && !bPlayerSeen && !bHit && (chaseTimer > chaseCooldown))
            {
                bPlayerSeen = true;
                anim.SetBool("running", true);
                PlayerPosition = player.transform.position;
                bPlayerSeen = true;
            }

            if (bPlayerSeen && !bHit)
            {
                body.position = Vector3.MoveTowards(body.position, new Vector3(PlayerPosition.x, body.position.y, body.position.z), chaseSpeed * Time.deltaTime);
                if (body.position.x == PlayerPosition.x && !PlayerInSight())
                {
                    bPlayerSeen = false;
                    anim.SetBool("running", false);
                    chaseTimer = 0;
                    groundPatrol.ChangeDirection();
                }
            }

            if (bHit)
            {
                StartCoroutine(WaitAfterHit());
            }
            if (groundPatrol != null)
            {
                if (bPlayerSeen || bHit)
                {
                    groundPatrol.enabled = false;
                }
                else
                {
                    groundPatrol.enabled = true;
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

    //For attack
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    
    //For Chasing
    private bool PlayerInSightChase()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangeChase * (transform.localScale.x) * colliderDistanceChase, new Vector3(boxCollider.bounds.size.x * rangeChase, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform.GetComponent<ExoV3Movement>();
        }

        return hit.collider != null;
    }

    public bool PlayerLeft()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangePlayer * -1 * colliderDistancePlayer, new Vector3(boxCollider.bounds.size.x * rangePlayer, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    public bool PlayerRight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangePlayer * colliderDistancePlayer, new Vector3(boxCollider.bounds.size.x * rangePlayer, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeChase * (transform.localScale.x) * colliderDistanceChase, new Vector3(boxCollider.bounds.size.x * rangeChase, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangePlayer * -1 * (transform.localScale.x) * colliderDistancePlayer, new Vector3(boxCollider.bounds.size.x * rangePlayer, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangePlayer * (transform.localScale.x) * colliderDistancePlayer, new Vector3(boxCollider.bounds.size.x * rangePlayer, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
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
        GetComponentInParent<GroundPatrol>().enabled = false;
        this.enabled = false;
        dead = true;
        Vector2 healthPickUpVector = new Vector2(body.position.x, body.position.y);
        Instantiate(healthPickup, healthPickUpVector, Quaternion.identity);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            player.TakeDamage(damage);
            bPlayerHit = true;
        }
    }

    private IEnumerator ChangeDir()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        groundPatrol.ChangeDirection();
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
        groundPatrol.EnableMove();
    }

    public void DisableMove()
    {
        bCanMove = false;
        groundPatrol.DisableMove();
    }
}
