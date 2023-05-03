using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    public bool inAttackRange;
    public Transform player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            inAttackRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            inAttackRange = false;
        }
    }
}
