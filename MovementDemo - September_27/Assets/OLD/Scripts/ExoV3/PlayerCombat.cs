using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D body;
    private ExoV3Movement player;

    public Transform attackPoint;
    [SerializeField]public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private int DamageDealt = 1;
     
    [SerializeField] private float attackRate = 2f;
    private float nextAttackTime = 0;
    protected bool bCanAttack = true;

    [SerializeField] private float SnakeForce;
    [SerializeField] private float FlyForce;
    [SerializeField] private float GroundForce;
    [SerializeField] private float HorseForce;






    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        player = GetComponent<ExoV3Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime && bCanAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    
    void Attack()
    {
        //Play an attack animation
        if(!player.IsGrounded())
        {
            anim.SetTrigger("airAttack");
            attackRange = 3f;
        }
        else
        {
            anim.SetTrigger("attack");
            attackRange = 1f;
        }
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "SnakeEnemy")
            {
                enemy.GetComponent<SnakeEnemy>().TakeDamage(DamageDealt);
                if (enemy.GetComponent<SnakeEnemy>().PlayerLeft())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(SnakeForce, 0), ForceMode2D.Impulse);
                }
                else if (enemy.GetComponent<SnakeEnemy>().PlayerRight())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-SnakeForce, 0), ForceMode2D.Impulse);
                }
            }
            else if (enemy.tag == "FlyEnemy")
            {
                enemy.GetComponent<FlyEnemy>().TakeDamage(DamageDealt);
                if (enemy.GetComponent<FlyEnemy>().PlayerLeft())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(FlyForce, 0), ForceMode2D.Impulse);
                }
                else if (enemy.GetComponent<FlyEnemy>().PlayerRight())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-FlyForce, 0), ForceMode2D.Impulse);
                }
            }
            else if (enemy.tag == "GroundEnemy")
            {
                enemy.GetComponent<GroundEnemy>().TakeDamage(DamageDealt);
                if (enemy.GetComponent<GroundEnemy>().PlayerLeft())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(GroundForce, 0), ForceMode2D.Impulse);
                }
                else if (enemy.GetComponent<GroundEnemy>().PlayerRight())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-GroundForce, 0), ForceMode2D.Impulse);
                }
            }
            else if (enemy.tag == "AntEnemy")
            {
                enemy.GetComponent<AntEnemy>().TakeDamage(DamageDealt);
            }
            else if (enemy.tag == "DrillFlyEnemy")
            {
                enemy.GetComponent<DrillFlyEnemy>().TakeDamage(DamageDealt);
            }
            else if (enemy.tag == "HorseEnemy")
            {
                enemy.GetComponent<HorseEnemy>().TakeDamage(DamageDealt);
                if (enemy.GetComponent<HorseEnemy>().PlayerLeft())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorseForce, 0), ForceMode2D.Impulse);
                }
                else if (enemy.GetComponent<HorseEnemy>().PlayerRight())
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-HorseForce, 0), ForceMode2D.Impulse);
                }
            }
            else if (enemy.tag == "BossEnemy")
            {
                enemy.GetComponent<BossEnemy>().TakeDamage(DamageDealt);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void DisableAttack()
    {
        bCanAttack = false;
    }

    public void EnableAttack()
    {
        bCanAttack = true;
    }

    public void SetDamage(int _damage)
    {
        DamageDealt = _damage;
    }
}
