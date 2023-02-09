using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public LayerMask enemyLayers;

    [SerializeField] public Stats stats;
    public V2PlayerMovement moveScript;

    public GameObject DMG_Text;
    public TextMesh dmgTextMesh;

    public GameObject CRIT_Text;
    public TextMesh critTextMesh;

    public AudioSource hitAudio;

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
            if (enemy.name == "Pyromaniac_Enemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();;
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    bool AttackIsPlaying()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Vagabond_LightAttack"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
