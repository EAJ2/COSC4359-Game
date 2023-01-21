using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillFlyEnemy : MonoBehaviour
{
    private Animator anim;
    private ExoV3Movement player;
    [SerializeField] private GameObject healthPickup;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float rangeX;
    [SerializeField] private float rangeY;
    private bool bHit = false;

    [Header("Movement Points")]
    public List<Transform> Waypoints;
    private Vector3 HeightPosition;
    private Vector3 AttackPosition;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Transform body;
    [SerializeField] private float colliderDistance;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        drillFlyPatrol = GetComponentInParent<DrillFlyPatrol>();
        HeightPosition = new Vector3(Waypoints[0].position.x, Waypoints[0].position.y, Waypoints[0].position.z);
        AttackPosition = new Vector3(Waypoints[1].position.x, Waypoints[1].position.y, Waypoints[0].position.z);
        body.position = HeightPosition;

        if(!bCanMove)
        {
            drillFlyPatrol.DisableMove();
        }
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(bCanMove)
        {
            if (PlayerInSight() && !bHit)
            {
                HeightPosition = new Vector3(body.position.x, Waypoints[0].position.y, body.position.z);
                AttackPosition = new Vector3(body.position.x, Waypoints[1].position.y, body.position.z);
                MoveToAttackPosition();
            }
            if (body.position.y != HeightPosition.y && bHit)
            {
                MoveToHeightPosition();
            }
            if (drillFlyPatrol != null)
            {
                drillFlyPatrol.enabled = !PlayerInSight();
            }
        }
    }

    private void MoveToAttackPosition()
    {
        anim.SetTrigger("attack");
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
            player = hit.transform.GetComponent<ExoV3Movement>();
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
            anim.SetTrigger("hurt");
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
        GetComponentInParent<DrillFlyPatrol>().enabled = false;
        this.enabled = false;
        dead = true;
        Vector2 healthPickUpVector = new Vector2(body.position.x, body.position.y);
        Instantiate(healthPickup, healthPickUpVector, Quaternion.identity);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && bHit == false)
        {
            collision.GetComponent<ExoV3Movement>().TakeDamage(damage);
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
