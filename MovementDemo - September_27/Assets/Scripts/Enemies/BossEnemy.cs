using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public List<Transform> points;
    public List<Transform> Attack3Points;
    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private bool bCanMove = true;
    private bool bMoveToNextTarget = false;
    private int target;

    [Header("Health Parameters")]
    [SerializeField] private float StartingHealth;
    private float damage = 1;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("Falling Spikes")]
    public List<FallingSpikeTrap> SpikeTraps;
    [SerializeField] private float SpikeAttackDuration;
    private float SpikeAttackDurationTimer;

    [Header("Horizontal Attack")]
    public List<HorizontalTrapAttack> HorizontalTraps;
    [SerializeField] private float HorizontalAttackDuration;
    private float HorizontalAttackDurationTimer;

    [Header("Attack3")]
    [SerializeField] private float Attack3Duration;
    private float Attack3Timer;
    [SerializeField] private float SideToSideSpeed;
    [SerializeField] private float AttackDownSpeed;
    [SerializeField] private float ResetSpeed;
    private int RandomTime;
    private float RandomTimer;
    private bool bAttack3Attacking = false;
    private bool bInAttack3 = false;
    private bool bAttack3Down = false;
    private bool bAttack3 = false;
    private int Attack3Target = 0;

    [Header("Attack4")]
    public List<FlyEnemy> Flies;
    [SerializeField] private float Attack4Duration;
    private float Attack4Timer;
    private bool bFliesSummoned = false;


    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float IdleTime;
    private float IdleTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform.position = new Vector3(points[4].position.x, points[4].position.y, transform.position.z);
        currentHealth = StartingHealth;
        foreach(FlyEnemy fly in Flies)
        {
            fly.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(bCanMove)
        {
            //Idle at center
            if (transform.position == new Vector3(points[4].position.x, points[4].position.y, transform.position.z))
            {
                IdleTimer += Time.deltaTime;
                boxCollider.enabled = true;
            }
            if (IdleTimer >= IdleTime)
            {
                IdleTimer = 0;
                bMoveToNextTarget = true;
                anim.SetBool("moving", true);
                NextTarget();
            }

            //Move To next target
            if (bMoveToNextTarget)
            {
                boxCollider.enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(points[target].position.x, points[target].position.y, transform.position.z), speed * Time.deltaTime);
                if (transform.position.x < points[target].position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }

            //Attacks Occur Here
            if (transform.position == new Vector3(points[target].position.x, points[target].position.y, transform.position.z) && !bAttack3)
            {
                bMoveToNextTarget = false;
                anim.SetBool("moving", false);
                if (target == 0)
                {
                    if (!(SpikeAttackDurationTimer >= SpikeAttackDuration))
                    {
                        SpikeAttackDurationTimer += Time.deltaTime;
                        ActivateSpikeAttack();
                        anim.SetBool("attacking", true);
                    }
                    else
                    {
                        SpikeAttackDurationTimer = 0;
                        DeactivateSpikeAttack();
                        anim.SetBool("attacking", false);

                        target = 4;
                        bMoveToNextTarget = true;
                        anim.SetBool("moving", true);
                    }
                }
                if (target == 1)
                {
                    if (!(HorizontalAttackDurationTimer >= HorizontalAttackDuration))
                    {
                        HorizontalAttackDurationTimer += Time.deltaTime;
                        ActivateHorizontalTraps();
                        anim.SetBool("attacking", true);
                    }
                    else
                    {
                        HorizontalAttackDurationTimer = 0;
                        DeactivateHorizontalTraps();
                        anim.SetBool("attacking", false);

                        target = 4;
                        bMoveToNextTarget = true;
                        anim.SetBool("moving", true);
                    }
                }
                if (target == 2)
                {
                    bAttack3 = true;
                    boxCollider.enabled = true;
                }
                if (target == 3)
                {
                    if (!(Attack4Timer >= Attack4Duration))
                    {
                        Attack4Timer += Time.deltaTime;
                        anim.SetBool("attacking", true);
                        if (!bFliesSummoned)
                        {
                            foreach (FlyEnemy fly in Flies)
                            {
                                fly.ReActivate();
                            }
                            bFliesSummoned = true;
                        }
                    }
                    else
                    {
                        Attack4Timer = 0;
                        anim.SetBool("attacking", false);

                        target = 4;
                        bMoveToNextTarget = true;
                        anim.SetBool("moving", true);
                        foreach (FlyEnemy fly in Flies)
                        {
                            fly.Deactivate();
                        }
                        bFliesSummoned = false;
                    }
                }
            }

            if (bAttack3)
            {

                Attack3();
            }
        }


    }

    private void Attack3()
    {
        if (!(Attack3Timer >= Attack3Duration))
        {
            Attack3Timer += Time.deltaTime;
            //Get The Range of when to attack
            if (!bInAttack3)
            {
                RandomTime = Random.Range(1, 4);
                bInAttack3 = true;
                RandomTimer = 0;
            }
            else
            {
                //Move to the height position
                if (transform.position.y != Attack3Points[0].position.y && !bAttack3Attacking)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Attack3Points[0].position.y, transform.position.z), ResetSpeed * Time.deltaTime);
                    //If at the height position, start the attack
                    if (transform.position.y == Attack3Points[0].position.y)
                    {
                        bAttack3Attacking = true;
                    }
                }
                //if in the attack, 
                if (bAttack3Attacking)
                {
                    RandomTimer += Time.deltaTime;
                    //Move side to side
                    if (transform.position.x != Attack3Points[Attack3Target].position.x && !bAttack3Down)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Attack3Points[Attack3Target].position.x, transform.position.y, transform.position.z), SideToSideSpeed * Time.deltaTime);
                        if (transform.position.x == Attack3Points[Attack3Target].position.x)
                        {
                            if (Attack3Target == 0)
                            {
                                Attack3Target = 1;
                            }
                            else if (Attack3Target == 1)
                            {
                                Attack3Target = 0;
                            }
                        }
                    }
                    if (RandomTimer >= RandomTime)
                    {
                        bAttack3Down = true;
                        if (transform.position.y != Attack3Points[2].position.y)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Attack3Points[2].position.y, transform.position.z), AttackDownSpeed * Time.deltaTime);
                            if (transform.position.y == Attack3Points[2].position.y)
                            {
                                RandomTime = Random.Range(1, 4);
                                bAttack3Down = false;
                                bAttack3Attacking = false;
                                RandomTimer = 0;
                            }
                        }
                    }
                }
            }

        }
        else
        {
            Attack3Timer = 0;
            anim.SetBool("attacking", false);
            bAttack3 = false;
            bInAttack3 = false;
            bAttack3Down = false;
            bAttack3Attacking = false;
            boxCollider.enabled = false;

            target = 4;
            bMoveToNextTarget = true;
            anim.SetBool("moving", true);
        }
    }

    private void ActivateSpikeAttack()
    {
        foreach(FallingSpikeTrap trap in SpikeTraps)
        {
            trap.SetToFall();
        }
    }

    private void DeactivateSpikeAttack()
    {
        foreach (FallingSpikeTrap trap in SpikeTraps)
        {
            trap.DisableSpike();
        }
    }

    private void ActivateHorizontalTraps()
    {
        foreach(HorizontalTrapAttack trap in HorizontalTraps)
        {
            trap.Activate();
        }
    }

    private void DeactivateHorizontalTraps()
    {
        foreach (HorizontalTrapAttack trap in HorizontalTraps)
        {
            trap.Deactivate();
        }
    }

    private void NextTarget()
    {
        target = Random.Range(0, 4);
    }

    public void TakeDamage(float _damage)
    {
        if (!bInAttack3)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, StartingHealth);
            if (currentHealth > 0)
            {
                anim.SetTrigger("hurt");
                IdleTimer = IdleTime;
            }
            else
            {
                if (!dead)
                {
                    Die();
                    anim.SetTrigger("die");
                }
            }
        }
    }

    private void Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        dead = true;
    }

    public bool IsDead()
    {
        return dead;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ExoV3Movement>().TakeDamage(damage);
        }
    }

    public void EnableMove()
    {
        bCanMove = true;
    }

    public void DisableMove()
    {
        bCanMove = false;
    }
}
