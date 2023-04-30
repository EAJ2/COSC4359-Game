using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    //Player transform
    [SerializeField] public Transform player;
    public float detectRange = 5f;
    public float attackRange = 1f;
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

    public int dmg;

    [SerializeField] public GameObject Player;
    [SerializeField] public V2Health hp;
    [SerializeField] public LevelUpBar xpBar;

    [SerializeField] public DetectionRight detectR;
    [SerializeField] public DetectionLeft detectL;
    [SerializeField] public AttackDetection ad;

    public PlayerHealthBar playerHealth;

    public GameObject DMG_Text;
    public TextMesh dmgTextMesh;


    public AudioSource hitAudio;


    //Ranger
    public bool inVolley = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<Transform>();
        playerAnim = Player.GetComponent<Animator>();
        hp = Player.GetComponent<V2Health>();
        xpBar = GameObject.FindGameObjectWithTag("XPBAR").GetComponent<LevelUpBar>();
        playerHealth = GameObject.FindGameObjectWithTag("HEALTHBAR").GetComponent<PlayerHealthBar>();
    }

    // Update is called once per frame
    void Update()
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
        else
        {
        }

    }

    public void Attack()
    {
        attackTimer = 0.25f;
        hp.TakeDmg((float)dmg);
        playerHealth.SetHealth(hp.CurrentHealth);
        if (hp.CurrentHealth > 0)
        {
            hitAudio.pitch = Random.RandomRange(0.7f, 1.2f);
            hitAudio.Play();
            playerAnim.Play("Player_Vagabond_Hit", -1, 0f);
        }
        dmgTextMesh.text = dmg.ToString();
        Instantiate(DMG_Text, new Vector3(player.position.x, player.position.y + 3, player.position.z), Quaternion.identity);
    }

    public void TakeDMG(int dmg)
    {
        currentHP -= dmg;
        anim.SetTrigger("Hit");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Player.GetComponent<Stats>().XP += xpValue;
        Player.GetComponent<Stats>().gold += goldValue;
        xpBar.SetXP(Player.GetComponent<Stats>().XP);
        anim.SetBool("Death", true);
        rb.velocity = new Vector2(0f, 0f);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
