using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Flip()
    {
        if (transform.position.x > player.position.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (transform.position.x < player.position.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossLimiter"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

}
