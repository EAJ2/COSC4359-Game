using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public KnightCombat comb;

    public float sustainTime;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Delete());
        comb = GameObject.Find("Knight").GetComponent<KnightCombat>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoblinEnemy"))
        {
            collision.gameObject.GetComponent<Goblin>().TakeDMG(comb.lightningDMG);
        }
        else if (collision.gameObject.CompareTag("PyromaniacEnemy"))
        {
            collision.gameObject.GetComponent<EvilWizard>().TakeDMG(comb.lightningDMG);
        }
        else if (collision.gameObject.CompareTag("MushroomEnemy"))
        {
            collision.gameObject.GetComponent<Mushroom>().TakeDamage(comb.lightningDMG);
        }
        else if (collision.gameObject.CompareTag("GroundEnemy"))
        {
            collision.gameObject.GetComponent<Skeleton>().TakeDMG(comb.lightningDMG);
        }
        else if (collision.gameObject.CompareTag("FlyEnemy"))
        {
            collision.gameObject.GetComponent<Mushroom>().TakeDamage(comb.lightningDMG);
        }
        else if (collision.gameObject.CompareTag("DrillFlyEnemy"))
        {
            collision.gameObject.GetComponent<Mushroom>().TakeDamage(comb.lightningDMG);
        }
        else if (collision.gameObject.CompareTag("Reaper_Boss"))
        {
            collision.gameObject.GetComponent<Boss_Health>().TakeDMG(comb.lightningDMG);
        }

    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(sustainTime);
        Destroy(gameObject);
    }
}
