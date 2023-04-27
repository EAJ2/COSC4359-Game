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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && block == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

            if (Input.GetMouseButtonDown(1) && moveScript.stamina >= 10)
            {
                HeavyAttack();
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

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Vagabond_LightAttack") && Input.GetMouseButtonDown(0) && combo == false)
        {
            ComboAttack();
            StartCoroutine(ComboWaitTime());
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
            hitAudio.pitch = Random.Range(0.7f, 1.2f);
            if (enemy.tag == "PyromaniacEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();;
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }

            else if (enemy.tag == "GoblinEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString(); 
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }

            else if (enemy.tag == "BatEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Bat>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Bat>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "Reaper_Boss")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Boss_Health>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Boss_Health>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "GroundEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Skeleton>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Skeleton>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "FlyEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<FlyEnemy>().TakeDamage(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<FlyEnemy>().TakeDamage(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "DrillFlyEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<DrillFlyEnemy>().TakeDamage(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<DrillFlyEnemy>().TakeDamage(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    void ComboAttack()
    {
        combo = true;
        anim.SetTrigger("ComboAttack");
        anim.SetBool("isMoving", false);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        int critCounter = Random.Range(1, 101);

        foreach (Collider2D enemy in hitEnemies)
        {
            hitAudio.pitch = Random.Range(0.7f, 1.2f);
            if (enemy.tag == "PyromaniacEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString(); ;
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "GoblinEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "Reaper_Boss")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Boss_Health>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Boss_Health>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    void HeavyAttack()
    {
        anim.SetTrigger("HeavyAttack");
        anim.SetBool("isMoving", false);
        moveScript.stamina -= 10;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        int critCounter = Random.Range(1, 101);

        foreach (Collider2D enemy in hitEnemies)
        {
            hitAudio.pitch = Random.Range(0.7f, 0.9f);
            if (enemy.tag == "PyromaniacEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.heavyCritDMG);
                    critTextMesh.text = stats.heavyCritDMG.ToString(); ;
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<EvilWizard>().TakeDMG(stats.heavyDMG);
                    dmgTextMesh.text = stats.heavyDMG.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "GoblinEnemy")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.heavyCritDMG);
                    critTextMesh.text = stats.heavyCritDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Goblin>().TakeDMG(stats.heavyDMG);
                    dmgTextMesh.text = stats.heavyDMG.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
            }
            else if (enemy.tag == "Reaper_Boss")
            {
                hitAudio.Play();
                if (critCounter <= stats.critRange)
                {
                    enemy.GetComponent<Boss_Health>().TakeDMG(stats.critDMG);
                    critTextMesh.text = stats.critDMG.ToString();
                    Instantiate(CRIT_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                }
                else
                {
                    enemy.GetComponent<Boss_Health>().TakeDMG(stats.dmg);
                    dmgTextMesh.text = stats.dmg.ToString();
                    Instantiate(DMG_Text, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Vagabond_LightAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Vagabond_HeavyAttack"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator ComboWaitTime()
    {
        yield return new WaitForSeconds(0.7f);
        combo = false;
    }
}
