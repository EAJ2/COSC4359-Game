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
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(sustainTime);
        Destroy(gameObject);
    }
}
