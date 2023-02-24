using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public bool combo = false;

    public bool block = false;

    public LayerMask enemyLayers;

    [SerializeField] public Stats stats;
    public V2PlayerMovement moveScript;

    public GameObject DMG_Text;
    public TextMesh dmgTextMesh;

    public GameObject CRIT_Text;
    public TextMesh critTextMesh;

    public AudioSource hitAudio;

    public GameObject projectilePrefab;
    public float shootForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        stats.Strength();
        stats.Dexterity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Wait());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            Block();
        }

        if (AttackIsPlaying() == true)
        {
            moveScript.enabled = false;
            moveScript.rb.velocity = new Vector2(0f, 0f);
        }
        else
        {
            moveScript.enabled = true;
        }




    }

    void Attack()
    {
        anim.SetTrigger("Attacking");
        anim.SetBool("isMoving", false);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        int critCounter = Random.Range(1, 101);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "PyromaniacEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString(); ;
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z), Quaternion.identity);
                }
            }

            else if (enemy.tag == "GoblinEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    void Shoot()
    {
        anim.SetTrigger("Shoot");
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Block()
    {
        if (block == false)
        {
            anim.SetBool("isBlocking", true);
            block = true;
            moveScript.DisableMovement();
            stats.dmgRed += 0.2f;
        }
        else
        {
            anim.SetBool("isBlocking", false);
            block = false;
            moveScript.EnableMovement();
            stats.dmgRed -= 0.2f;
        }

    }

    bool AttackIsPlaying()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Ranger_LightAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Ranger_Shoot"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Wait()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.45f);
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y - 1.75f, transform.position.z), Quaternion.identity);
    }



}
