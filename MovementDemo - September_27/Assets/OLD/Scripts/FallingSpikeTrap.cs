using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeTrap : MonoBehaviour
{
    public List<Transform> points;
    private SpriteRenderer sr;

    [SerializeField] private float Speed;

    [SerializeField] private bool bFall = false;

    [SerializeField] private bool bKeepFalling = false;

    [SerializeField] private float Delay;
    private float FallTime;

    private void Awake()
    {
        transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    private void Update()
    {
        FallTime += Time.deltaTime;
        if(bFall)
        {
            if(!bKeepFalling)
            {
                sr.enabled = true;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, points[1].position.y, transform.position.z), Speed * Time.deltaTime);

                if (transform.position.y == points[1].position.y)
                {
                    sr.enabled = false;
                }
            }
            else
            {
                if (FallTime >= Delay)
                {
                    sr.enabled = true;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, points[1].position.y, transform.position.z), Speed * Time.deltaTime);
                }
                if (transform.position.y == points[1].position.y)
                {
                    sr.enabled = false;
                    transform.position = new Vector3(transform.position.x, points[0].position.y, transform.position.z);
                    FallTime = 0;
                }
            }
        }
    }

    public void SetToFall()
    {
        bFall = true;
    }

    public void DisableSpike()
    {
        bFall = false;
        sr.enabled = false;
        transform.position = new Vector3(transform.position.x, points[0].position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<ExoV3Movement>().TakeDamage(1);
            if (!bKeepFalling)
            {
                DisableSpike();
            }
            sr.enabled = false;
            FallTime = 0;
            transform.position = new Vector3(transform.position.x, points[0].position.y, transform.position.z);
        }
    }
}
