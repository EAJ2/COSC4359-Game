using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Animator anim;
    private Player player;
    private Transform body;
    private Rigidbody2D rb;

    [Header("Respawn Parameters")]
    [SerializeField] private float RespawnTime;
    private float RespawnTimer;
    private bool bRespawning = false;
    [SerializeField] private bool bCanRespawn = true;

    [Header("Rewards")]
    [SerializeField] private float xpValue;
    [SerializeField] private int goldValue;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float DealDamageRange;
    [SerializeField] private float rangeChase;
    [SerializeField] private float chaseCooldown;
    private float cooldownTimer = 0;
    private float chaseTimer = 0;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float colliderDistanceChase;

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


    private bool bPlayerSeen = false;
    private Vector3 PlayerPosition;
    private bool bPlayerHit = false;

    [SerializeField] private bool bCanMove = true;
    private GoblinPatrol goblinPatrol;
    //Ranger
    public bool inVolley = false;

    public GameObject DMG_Text;
    public TextMesh dmgTextMesh;
    private bool bDead = false;

    private bool bCanTakeDamage = true;

    public AudioSource hitAudio;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        goblinPatrol = GetComponentInParent<GoblinPatrol>();
        body = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        chaseTimer = chaseCooldown;
        RespawnTimer = 0;

        if (!bCanMove)
        {
            goblinPatrol.DisableMove();
        }
        anim.SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {

        cooldownTimer += Time.deltaTime;
        chaseTimer += Time.deltaTime;

        if (bCanMove)
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
                    chaseTimer = 0;
                    anim.SetTrigger("Attack");
                }
            }
            if (PlayerInSightChase() && !bPlayerSeen && !bHit && (chaseTimer > chaseCooldown))
            {
                bPlayerSeen = true;
                //PlayerPosition = player.transform.position;
                bPlayerSeen = true;
            }

            if (bPlayerSeen && !bHit)
            {
                PlayerPosition = player.transform.position;
                body.position = Vector3.MoveTowards(body.position, new Vector3(PlayerPosition.x, body.position.y, body.position.z), chaseSpeed * Time.deltaTime);
                if (body.position.x == PlayerPosition.x && !PlayerInSight())
                {
                    bPlayerSeen = false;
                    chaseTimer = 0;
                    goblinPatrol.ChangeDirection();
                }
            }

            if (bHit)
            {
                StartCoroutine(WaitAfterHit());
            }
            if (goblinPatrol != null)
            {
                if (bPlayerSeen || bHit)
                {
                    goblinPatrol.enabled = false;
                }
                else
                {
                    goblinPatrol.enabled = true;
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

    //For attack
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * DealDamageRange * (transform.localScale.x) * colliderDistance, new Vector3(boxCollider.bounds.size.x * DealDamageRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    //For Chasing
    private bool PlayerInSightChase()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangeChase * (transform.localScale.x) * colliderDistanceChase, new Vector3(boxCollider.bounds.size.x * rangeChase, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform.GetComponent<Player>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * DealDamageRange * (transform.localScale.x) * colliderDistance, new Vector3(boxCollider.bounds.size.x * DealDamageRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeChase * (transform.localScale.x) * colliderDistanceChase, new Vector3(boxCollider.bounds.size.x * rangeChase, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

    }

    public void TakeDMG(float _damage)
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

    public void Die()
    {
        bCanTakeDamage = false;
        anim.SetTrigger("Die");
        player.GetComponent<Stats>().SetXP(xpValue);
        player.GetComponent<Stats>().SetGold(goldValue);

        bDead = true;
        bCanMove = false;
    }

    private void Deactivate()
    {
        if(bCanRespawn)
        {
            bRespawning = true;
        }
        rb.bodyType = RigidbodyType2D.Static;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Respawn()
    {
        anim.SetTrigger("Respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        RespawnTimer = 0;
        bRespawning = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        GetComponentInParent<GoblinPatrol>().enabled = true;
        bCanMove = true;
        bCanTakeDamage = true;
        ResetHealth();
        bDead = false;
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            player.GetComponent<V2Health>().TakeDmg(damage);
            bPlayerHit = true;
            dmgTextMesh.text = damage.ToString();
            Instantiate(DMG_Text, new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z), Quaternion.identity);
        }
    }

    private IEnumerator ChangeDir()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        goblinPatrol.ChangeDirection();
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

    public bool IsDead()
    {
        return bDead;
    }

    public void EnableMove()
    {
        bCanMove = true;
    }

    public void DisableMove()
    {
        bCanMove = false;
        goblinPatrol.DisableMove();
    }

    private void ResetHealth()
    {
        currentHealth = startingHealth;
    }
}
