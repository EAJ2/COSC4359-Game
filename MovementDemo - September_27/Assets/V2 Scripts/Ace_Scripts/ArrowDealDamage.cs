using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDealDamage : MonoBehaviour
{
    [SerializeField] private int Damage;
    private int ArrowNumber;
    [SerializeField] private ArrowAbility AA;

    private void Start()
    {
        ArrowNumber = int.Parse(gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GroundEnemy")
        {
            collision.gameObject.GetComponent<Skeleton>().TakeDMG(Damage);
            AA.ResetArrow(ArrowNumber);
        }
        if (collision.gameObject.tag == "PyromaniacEnemy")
        {
            collision.gameObject.GetComponent<EvilWizard>().TakeDMG((int)Damage);
            AA.ResetArrow(ArrowNumber);
        }
        if (collision.gameObject.tag == "GoblinEnemy")
        {
            collision.gameObject.GetComponent<Goblin>().TakeDMG((int)Damage);
            AA.ResetArrow(ArrowNumber);
        }
        if (collision.gameObject.tag == "BatEnemy")
        {
            collision.gameObject.GetComponent<Bat>().TakeDMG((int)Damage);
            AA.ResetArrow(ArrowNumber);
        }
        if (collision.gameObject.tag == "Reaper_Boss")
        {
            collision.gameObject.GetComponent<Boss_Health>().TakeDMG((int)Damage);
            AA.ResetArrow(ArrowNumber);
        }
    }
}
