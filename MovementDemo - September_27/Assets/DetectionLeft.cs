using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionLeft : MonoBehaviour
{

    public bool PlayerInAreaLeft;
    public Transform Player;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerInAreaLeft = true;
            Player = col.gameObject.transform;
            Debug.Log("Player entered range");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerInAreaLeft = false;
            Player = null;
            Debug.Log("Player left range");
        }
    }
}
