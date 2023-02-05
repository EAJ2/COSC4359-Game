using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : MonoBehaviour
{

    
    //Player transform
    [SerializeField]public Transform player;
    public float detectRange = 5f;
    public float attackRange = 1f;
    public float attackTimer;
    public bool inRange = false;
    public bool moving = false;
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public Animator anim;

    [SerializeField] public GameObject Player;
    [SerializeField] public V2Health hp;

    [SerializeField] public DetectionRight detectR;
    [SerializeField] public DetectionLeft detectL;
    [SerializeField] public AttackDetection ad;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
        else if(detectL.PlayerInAreaLeft == true && ad.inAttackRange == false)
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

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Pyromaniac_Attack"))
        {
            //Debug.Log("Attacking Right Now");
            if (attackTimer <= 0)
            {
                Attack();
            }
        }
        else
        {
            //Debug.Log("Not Currently Attacking");
            
        }
        
    }

    public void Attack()
    {
        attackTimer = 0.25f;
        hp.CurrentHealth -= 1f;
    }

}
