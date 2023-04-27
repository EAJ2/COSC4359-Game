using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyTriggerCollision : MonoBehaviour
{
    private bool bTriggered = false;

    public bool IsTriggered()
    {
        return bTriggered;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bTriggered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bTriggered = false;
        }
    }
}