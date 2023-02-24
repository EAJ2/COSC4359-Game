using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTrapAttack : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;

    public List<Transform> points;
    [SerializeField] private bool bStartLeft = true;
    [SerializeField] private float speed;
    [SerializeField] private bool bMove = false;
    [SerializeField] private bool bContinuous = false;

    private Vector3 EdgeL;
    private Vector3 EdgeR;

    private int target;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        sr.enabled = false;

        EdgeL = points[0].position;
        EdgeR = points[1].position;

        if(bStartLeft)
        {
            target = 0;
            transform.position = new Vector3(EdgeL.x, EdgeL.y, transform.position.z);
        }
        else
        {
            target = 1;
            transform.position = new Vector3(EdgeR.x, EdgeR.y, transform.position.z);
        }
    }

    private void Update()
    {
        if(bMove)
        {
            if(bContinuous)
            {
                if (bStartLeft)
                {
                    sr.enabled = true;
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(EdgeR.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                    if (transform.position.x == EdgeR.x)
                    {
                        transform.position = new Vector3(EdgeL.x, EdgeL.y, transform.position.z);
                    }
                }
                else
                {
                    sr.enabled = true;
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(EdgeL.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                    if (transform.position.x == EdgeL.x)
                    {
                        transform.position = new Vector3(EdgeR.x, EdgeR.y, transform.position.z);
                    }
                }
            }
            else
            {
                if (bStartLeft)
                {
                    sr.enabled = true;
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(EdgeR.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                    if (transform.position.x == EdgeR.x)
                    {
                        sr.enabled = false;
                    }
                }
                else
                {
                    sr.enabled = true;
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(EdgeL.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                    if (transform.position.x == EdgeL.x)
                    {
                        sr.enabled = false;
                    }
                }
            }
        }
    }

    public void Activate()
    {
        bMove = true;
        sr.enabled = true;
        boxCollider.enabled = true;
    }

    public void Deactivate()
    {
        bMove = false;
        sr.enabled = false;
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<ExoV3Movement>().TakeDamage(1);
            if(!bContinuous)
            {
                Deactivate();
            }
            sr.enabled = false;
            if (bStartLeft)
            {
                transform.position = new Vector3(EdgeL.x, EdgeL.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(EdgeR.x, EdgeR.y, transform.position.z);
            }
        }
    }
}
