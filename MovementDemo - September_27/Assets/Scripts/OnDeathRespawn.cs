using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathRespawn : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private ExoV3Movement player;
    private Vector3 RespawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RespawnLocation = point.position;
            player.SetOnDeathRespawnPoint(RespawnLocation);
        }
    }
}
