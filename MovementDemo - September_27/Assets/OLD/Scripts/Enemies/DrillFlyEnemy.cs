using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillFlyEnemy : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Player player;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float rangeX;
    [SerializeField] private float rangeY;
    private bool bHit = false;

    [Header("Rewards")]
    [SerializeField] private int xpValue;
    [SerializeField] private int goldValue;
    private LevelUpBar xpBar;

    [Header("Movement Points")]
    public List<Transform> Waypoints;
    private Vector3 HeightPosition;
    private Vector3 AttackPosition;

    [Header("Collider Parameters")]
    [SerializeField] private Transform body;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("Movement Parameter")]
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float resetSpeed;

    private DrillFlyPatrol drillFlyPatrol;

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
        if (player == null)
        {
            Debug.Log("Player missing in the DrillFly");
        }

        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        drillFlyPatrol = GetComponentInParent<DrillFlyPatrol>();
        HeightPosition = new Vector3(Waypoints[0].position.x, Waypoints[0].position.y, Waypoints[0].position.z);
        AttackPosition = new Vector3(Waypoints[1].position.x, Waypoints[1].position.y, Waypoints[0].position.z);
        body.position = HeightPosition;

        xpBar = player.GetComponent<LevelUpBar>();
        RespawnTimer = 0;

        if (!bCanMove)
        {
            drillFlyPatrol.DisableMove();
        }
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (bCanMove)
        {
            if (PlayerInSight() && !bHit && cooldownTimer >= attackCooldown)
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Moving", false);
                HeightPosition = new Vector3(body.position.x, Waypoints[0].position.y, body.position.z);
                AttackPosition = new Vector3(body.position.x, Waypoints[1].position.y, body.position.z);
                MoveToAttackPosition();
            }
            if (body.position.y != HeightPosition.y && bHit)
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Moving", true);
                MoveToHeightPosition();
            }
            if (drillFlyPatrol != null)
            {
                if(PlayerInSight() == true && cooldownTimer >= attackCooldown)
                {
                    drillFlyPatrol.enabled = false;
                }
                else
                {
                    drillFlyPatrol.enabled = true;
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

    private void MoveToAttackPosition()
    {
        body.position = Vector3.MoveTowards(body.position, AttackPosition, chaseSpeed * Time.deltaTime);
        if (body.position.y == AttackPosition.y)
        {
            cooldownTimer = 0;
        }
    }

    private void MoveToHeightPosition()
    {
        body.position = Vector3.MoveTowards(body.position, HeightPosition, resetSpeed * Time.deltaTime);
        if (body.position == HeightPosition)
        {
            bHit = false;
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.up * rangeX * (transform.localScale.y) * colliderDistance * -1, new Vector3(boxCollider.bounds.size.x * rangeX, boxCollider.bounds.size.y * rangeY, boxCollider.bounds.size.z), 0, Vector2.down, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform.GetComponent<Player>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.up * rangeX * (transform.localScale.y) * colliderDistance * -1, new Vector3(boxCollider.bounds.size.x * rangeX, boxCollider.bounds.size.y * rangeY, boxCollider.bounds.size.z));
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
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


        this.GetComponentInParent<DrillFlyPatrol>().enabled = false;
        dead = true;
        bCanMove = false;
    }

    private void Deactivate()
    {
        if(bCanRespawn)
        {
            bRespawning = true;
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Respawn()
    {
        anim.SetTrigger("Respawn");
        RespawnTimer = 0;
        bRespawning = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponentInParent<DrillFlyPatrol>().enabled = true;
        dead = false;
        bCanMove = true;
        currentHealth = startingHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && bHit == false)
        {
            collision.gameObject.GetComponent<V2Health>().TakeDmg(damage);
            cooldownTimer = 0;
            bHit = true;
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    public void EnableMove()
    {
        bCanMove = true;
        drillFlyPatrol.EnableMove();
    }

    public void DisableMove()
    {
        bCanMove = false;
        drillFlyPatrol.DisableMove();
    }
}
