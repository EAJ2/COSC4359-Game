using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private ExoV3Movement player;
    private Vector3 SpawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SpawnLocation = point.position;
            player.SetRespawnLocation(SpawnLocation);
        }
    }
}
