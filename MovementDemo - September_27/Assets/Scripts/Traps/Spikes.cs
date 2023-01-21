using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool bRespawn = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(bRespawn)
            {
                collision.GetComponent<ExoV3Movement>().TakeDamage(damage);
                collision.GetComponent<ExoV3Movement>().SpawnInRespawnPoint();
            }
            else
            {
                collision.GetComponent<ExoV3Movement>().TakeDamage(damage);
            }
        }
    }
}
