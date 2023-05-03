using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    //Player transform
    [SerializeField] public Player player;
    public float detectRange = 5f;
    public float attackRange = 1f;
    public float attackTimer;
    public bool inRange = false;
    public bool moving = false;
    public float moveSpeed = 2f;
    public float xpValue;
    public int goldValue;
    public int maxHP;
    public int currentHP;
    private Rigidbody2D rb;
    SpriteRenderer sr;
    public Animator anim;
    public Animator playerAnim;

    [SerializeField] private bool bCanMove = true;

    [Header("Respawn Parameters")]
    [SerializeField] private float RespawnTime;
    private float RespawnTimer;
    private bool bRespawning = false;
    [SerializeField] private bool bCanRespawn = true;

    public int dmg;

    [SerializeField] public V2Health hp;

    [SerializeField] public DetectionRight detectR;
    [SerializeField] public DetectionLeft detectL;
    [SerializeField] public AttackDetection ad;

    private bool bCanTakeDamage = true;

    public GameObject DMG_Text;
    public TextMesh dmgTextMesh;
    private bool bDead = false;

    public AudioSource hitAudio;

    public bool inVolley = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hp = player.GetComponent<V2Health>();

        if(player == null)
        {
            Debug.Log("Player missing in the Goblin");
        }

        RespawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(bCanMove)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }

            if (detectR.PlayerInAreaRight == true && ad.inAttackRange == false)
            {
                rb.velocity = new Vector2(moveSpeed, 0f);
                sr.flipX = false;
                anim.SetBool("isMoving", true);
            }
            else if (detectL.PlayerInAreaLeft == true && ad.inAttackRange == false)
            {
                rb.velocity = new Vector2(-moveSpeed, 0f);
                sr.flipX = true;
                anim.SetBool("isMoving", true);
            }
            else if (detectR.PlayerInAreaRight == false && detectL.PlayerInAreaLeft == false || ad.inAttackRange == true)
            {
                rb.velocity = new Vector2(0f, 0f);
                anim.SetBool("isMoving", false);
            }

            if (ad.inAttackRange == true)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Goblin_Attack1"))
            {
                if (attackTimer <= 0)
                {
                    Attack();
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

    public void Attack()
    {
        attackTimer = 0.25f;
        if (hp.CurrentHealth > 0)
        {
            hp.TakeDmg((float)dmg);
        }
        dmgTextMesh.text = dmg.ToString();
        Instantiate(DMG_Text, new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z), Quaternion.identity);
    }

    public void TakeDMG(int dmg)
    {
        if (bCanTakeDamage)
        {
            currentHP = Mathf.Clamp(currentHP - dmg, 0, maxHP);
            anim.SetTrigger("Hit");
        }

        if (currentHP <= 0)
        {
            Die();
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
        this.GetComponentInChildren<DetectionLeft>().enabled = false;
        this.GetComponentInChildren<DetectionRight>().enabled = false;
    }

    private void Respawn()
    {
        anim.SetTrigger("Respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        RespawnTimer = 0;
        bRespawning = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponentInChildren<DetectionLeft>().enabled = true;
        this.GetComponentInChildren<DetectionRight>().enabled = true;
        bCanMove = true;
        bCanTakeDamage = true;
        currentHP = maxHP;
        bDead = false;
    }

    public bool IsDead()
    {
        return bDead;
    }

    public void EnableMove()
    {
        bCanMove = true;
    }
}
