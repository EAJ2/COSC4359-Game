using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTargetTurnOffCollision : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boxCollider.enabled = true;
        }
    }
}
