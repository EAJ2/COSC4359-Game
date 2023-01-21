using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseEnemy : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private ExoV3Movement player;
    [SerializeField] private Transform body;
    [SerializeField] private GameObject healthPickup;
    private Rigidbody2D rb;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float jumpAttackCooldown;
    [SerializeField] private float kickattackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float kickDistance;
    [SerializeField] private float kickRange;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float colliderDistanceKick;

    [Header("Layers")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float cooldownTimer = Mathf.Infinity;
    private float cooldownTimerKick = Mathf.Infinity;
    private Vector3 playerPosition;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;
    [SerializeField] private float waitTime;

    [Header("Movement Parameters")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;

    [Header("Knockback")]
    [SerializeField] private float colliderDistancePlayer;
    [SerializeField] private float rangePlayer;
    [SerializeField] private float waitAfterHitTime;
    private bool bHit = false;

    private HorsePatrol horsePatrol;
    private bool bPlayerHit = false;
    private bool bPlayerSeen = false;

    [SerializeField] private bool bCanMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        horsePatrol = GetComponentInParent<HorsePatrol>();
        cooldownTimerKick = kickattackCooldown;
        rb = GetComponent<Rigidbody2D>();

        if(!bCanMove)
        {
            horsePatrol.DisableMove();
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        cooldownTimerKick += Time.deltaTime;

        if(bCanMove)
        {
            if (PlayerInKickZone() && !bHit)
            {
                if (cooldownTimerKick >= kickattackCooldown)
                {
                    anim.SetTrigger("kick");
                }
            }
            if (PlayerInSight() && !bHit && !bPlayerSeen && (cooldownTimer >= attackCooldown))
            {
                anim.SetBool("walking", false);
                anim.SetBool("running", true);
                bPlayerSeen = true;
                playerPosition = player.transform.position;
            }

            if (bPlayerSeen)
            {
                body.position = Vector3.MoveTowards(body.position, new Vector3(playerPosition.x, body.position.y, body.position.z), chaseSpeed * Time.deltaTime);
                if (body.position.x == playerPosition.x || bPlayerHit)
                {
                    horsePatrol.ChangeDirection();
                    if (bPlayerHit)
                    {
                        bPlayerHit = false;
                        cooldownTimerKick = cooldownTimerKick / 2;
                    }
                    bPlayerSeen = false;
                    anim.SetBool("walking", true);
                    anim.SetBool("running", false);
                }
            }

            if (bHit)
            {
                StartCoroutine(WaitAfterHit());
            }
            if (horsePatrol != null)
            {
                if (bPlayerSeen || bHit)
                {
                    horsePatrol.enabled = false;
                }
                else
                {
                    horsePatrol.enabled = true;
                    bPlayerSeen = false;
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
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance * -1, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform.GetComponent<ExoV3Movement>();
        }

        return hit.collider != null;
    }

    private bool PlayerInKickZone()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * kickRange * (transform.localScale.x) * colliderDistanceKick, new Vector3(boxCollider.bounds.size.x * kickRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

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

    private IEnumerator ChangeDir()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        horsePatrol.ChangeDirection();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance * -1, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * kickRange * (transform.localScale.x) * colliderDistanceKick, new Vector3(boxCollider.bounds.size.x * kickRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

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
        GetComponentInParent<HorsePatrol>().enabled = false;
        this.enabled = false;
        dead = true;
        Vector2 healthPickUpVector = new Vector2(body.position.x, body.position.y);
        Instantiate(healthPickup, healthPickUpVector, Quaternion.identity);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void KickPlayer()
    {
        player.GetKicked(kickDistance);
        player.TakeDamage(damage);
        cooldownTimerKick = 0;
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

            if (cooldownTimer >= attackCooldown)
            {
                collision.gameObject.GetComponent<ExoV3Movement>().TakeDamage(damage);
                //player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x * PushForce, player.GetComponent<Rigidbody2D>().velocity.y);
                cooldownTimer = 0;
                bPlayerHit = true;
                anim.SetBool("walking", true);
                anim.SetBool("running", false);
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
        horsePatrol.EnableMove();
    }

    public void DisableMove()
    {
        bCanMove = false;
        horsePatrol.DisableMove();
    }
}
