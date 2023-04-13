using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int dmg;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask attackMask;

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<V2Health>().TakeDmg(dmg);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
