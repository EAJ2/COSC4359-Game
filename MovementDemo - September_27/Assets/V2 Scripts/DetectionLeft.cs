using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionLeft : MonoBehaviour
{

    public bool PlayerInAreaLeft;
    public Transform Player;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerInAreaLeft = true;
            Player = col.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerInAreaLeft = false;
            Player = null;
        }
    }
}
