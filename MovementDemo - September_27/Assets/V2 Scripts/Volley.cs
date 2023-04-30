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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoblinEnemy"))
        {
            collision.GetComponent<Goblin>().inVolley = false;
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
