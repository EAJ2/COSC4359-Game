using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Volley : MonoBehaviour
{
    public RangerCombat comb;


    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Delete());
        comb = GameObject.Find("Ranger").GetComponent<RangerCombat>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoblinEnemy"))
        {
            collision.GetComponent<Goblin>().inVolley = true;
            StartCoroutine(DMG(collision));
        }
        else if (collision.gameObject.CompareTag("PyromaniacEnemy"))
        {
            collision.gameObject.GetComponent<EvilWizard>().inVolley = true;
            StartCoroutine(DMG(collision));
        }
        else if (collision.gameObject.CompareTag("MushroomEnemy"))
        {
            collision.gameObject.GetComponent<Mushroom>().inVolley = true;
            StartCoroutine(DMG(collision));
        }
        else if (collision.gameObject.CompareTag("GroundEnemy"))
        {
            collision.gameObject.GetComponent<Skeleton>().inVolley = true;
            StartCoroutine(DMG(collision));
        }
        else if (collision.gameObject.CompareTag("FlyEnemy"))
        {
            collision.gameObject.GetComponent<Mushroom>().inVolley = true;
            StartCoroutine(DMG(collision));
        }
        else if (collision.gameObject.CompareTag("DrillFlyEnemy"))
        {
            collision.gameObject.GetComponent<Mushroom>().inVolley = true;
            StartCoroutine(DMG(collision));
        }
        else if (collision.gameObject.CompareTag("Reaper_Boss"))
        {
            collision.gameObject.GetComponent<Boss_Health>().inVolley = true;
            StartCoroutine(DMG(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoblinEnemy"))
        {
            collision.GetComponent<Goblin>().inVolley = false;
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("PyromaniacEnemy"))
        {
            collision.gameObject.GetComponent<EvilWizard>().inVolley = false;
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("MushroomEnemy"))
        {
            collision.gameObject.GetComponent<Mushroom>().inVolley = false;
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("GroundEnemy"))
        {
            collision.gameObject.GetComponent<Skeleton>().inVolley = false;
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("FlyEnemy"))
        {
            collision.gameObject.GetComponent<FlyEnemy>().inVolley = false;
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("DrillFlyEnemy"))
        {
            collision.gameObject.GetComponent<DrillFlyEnemy>().inVolley = false;
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("Reaper_Boss"))
        {
            collision.gameObject.GetComponent<Boss_Health>().inVolley = false;
            StopAllCoroutines();
        }
    }

    IEnumerator DMG(Collider2D target)
    {
        if (target.gameObject.CompareTag("GoblinEnemy"))
        {
            while (target.GetComponent<Goblin>().inVolley)
            {
                yield return new WaitForSeconds(0.1f);
                target.gameObject.GetComponent<Goblin>().TakeDMG(comb.volleyDMG);
            }
        }
        else if (target.gameObject.CompareTag("PyromaniacEnemy"))
        {
            while (target.GetComponent<EvilWizard>().inVolley)
            {
                yield return new WaitForSeconds(0.1f);
                target.gameObject.GetComponent<EvilWizard>().TakeDMG(comb.volleyDMG);
            }
        }
        else if (target.gameObject.CompareTag("MushroomEnemy"))
        {
            while (target.GetComponent<Mushroom>().inVolley)
            {
                yield return new WaitForSeconds(0.1f);
                target.gameObject.GetComponent<Mushroom>().TakeDamage(comb.volleyDMG);
            }
        }
        else if (target.gameObject.CompareTag("GroundEnemy"))
        {
            while (target.GetComponent<Skeleton>().inVolley)
            {
                yield return new WaitForSeconds(0.1f);
                target.gameObject.GetComponent<Skeleton>().TakeDMG(comb.volleyDMG);
            }
        }
        else if (target.gameObject.CompareTag("FlyEnemy"))
        {
            while (target.GetComponent<FlyEnemy>().inVolley)
            {
                yield return new WaitForSeconds(0.1f);
                target.gameObject.GetComponent<FlyEnemy>().TakeDamage(comb.volleyDMG);
            }
        }
        else if (target.gameObject.CompareTag("DrillFlyEnemy"))
        {
            while (target.GetComponent<DrillFlyEnemy>().inVolley)
            {
                yield return new WaitForSeconds(0.1f);
                target.gameObject.GetComponent<DrillFlyEnemy>().TakeDamage(comb.volleyDMG);
            }
        }
        else if (target.gameObject.CompareTag("Reaper_Boss"))
        {
            while (target.GetComponent<Boss_Health>().inVolley)
            {
                yield return new WaitForSeconds(0.1f);
                target.gameObject.GetComponent<Boss_Health>().TakeDMG(comb.volleyDMG);
            }
        }
        else
        {
            yield break;
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
