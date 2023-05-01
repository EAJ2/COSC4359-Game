using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvilWizard : MonoBehaviour
{
    //Player transform
    [SerializeField]public Transform player;
    public float detectRange = 5f;
    public float attackRange = 1f;
    public int dmg;
    public float attackTimer;
    public bool inRange = false;
    public bool moving = false;
    public float moveSpeed = 2f;
    public int xpValue;
    public int goldValue;
    public int maxHP;
    public int currentHP;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public Animator anim;
    public Animator playerAnim;

    [Header("Respawn Parameters")]
    [SerializeField] private float RespawnTime;
    private float RespawnTimer;
    private bool bRespawning = false;
    [SerializeField] private bool bCanRespawn = true;

    [SerializeField] public GameObject Player;
    [SerializeField] public V2Health hp;
    [SerializeField] public LevelUpBar xpBar;

    [SerializeField] public DetectionRight detectR;
    [SerializeField] public DetectionLeft detectL;
    [SerializeField] public AttackDetection ad;

    public PlayerHealthBar playerHealth;

    public GameObject DMG_Text;
    public TextMesh dmgTextMesh;

    [SerializeField] private bool bCanMove = true;
    private bool bCanTakeDamage = true;

    public AudioSource hitAudio;
    private bool bDead = false;

    //Ranger
    public bool inVolley = false;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.Log("Player missing in the Wizard");
        }

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<Transform>();
        hp = Player.GetComponent<V2Health>();
        playerAnim = Player.GetComponent<Animator>();
        xpBar = GameObject.FindGameObjectWithTag("XPBAR").GetComponent<LevelUpBar>();
        playerHealth = GameObject.FindGameObjectWithTag("HEALTHBAR").GetComponent<PlayerHealthBar>();

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

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pyromaniac_Attack"))
            {
                if (attackTimer <= 0)
                {
                    Attack();
                }
            }

            if (rb.velocity.x == 0)
            {
                anim.SetBool("Idle", true);
            }
            else
            {
                anim.SetBool("Idle", false);
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
        Instantiate(DMG_Text, new Vector3(player.position.x, player.position.y + 3, player.position.z), Quaternion.identity);
    }

    public void TakeDMG(int dmg)
    {
        if(bCanTakeDamage)
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
        anim.SetTrigger("Die");
        Player.GetComponent<Stats>().XP += xpValue;
        Player.GetComponent<Stats>().gold += goldValue;

        xpBar.SetXP(Player.GetComponent<Stats>().XP);

        bDead = true;
        bCanMove = false;
        bCanTakeDamage = false;
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
        bDead = false;
        currentHP = maxHP;
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
