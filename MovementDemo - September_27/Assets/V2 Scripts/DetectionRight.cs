using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRight : MonoBehaviour
{

    public bool PlayerInAreaRight;
    public Transform Player;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerInAreaRight = true;
            Player = col.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerInAreaRight = false;
            Player = null;
        }
    }
}
