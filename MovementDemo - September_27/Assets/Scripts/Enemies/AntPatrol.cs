using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public List<Transform> Waypoints;
    private int target = 0;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Rigidbody2D body;
    private BoxCollider2D boxCollider;

    [Header("Movement Parameters")]
    [SerializeField] private float BasicSpeed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private bool bUpsideDown = false;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Layers Masks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    
    private bool bCanMove = true;

    private void Awake()
    {
        initScale = enemy.localScale;
        enemy.position = Waypoints[0].position;
        boxCollider = enemy.GetComponent<BoxCollider2D>();
        if(bUpsideDown)
        {
            Destroy(body);
        }
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if(!bUpsideDown && !IsGrounded())
        {
            body.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (!bUpsideDown && IsGrounded())
        {
            body.bodyType = RigidbodyType2D.Static;
        }
        
        if(bCanMove)
        {
            MoveInDirection();
            enemy.position = Vector3.MoveTowards(enemy.position, new Vector3(Waypoints[target].position.x, enemy.position.y, enemy.position.z), BasicSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (enemy.position.x == Waypoints[target].position.x || OnWall())
        {
            if (target == 1)
            {
                target = 0;
            }
            else
            {
                target = 1;
            }
        }
    }


    private void MoveInDirection()
    {

        if(IsGrounded() || bUpsideDown)
        {
            anim.SetBool("moving", true);
            // 0    1

            if (bUpsideDown)
            {
                if (target == 1)
                {
                    enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y * -1, initScale.z);
                }
                else if (target == 0)
                {
                    enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y * -1, initScale.z);
                }
            }
            else if (!bUpsideDown)
            {
                if (target == 1)
                {
                    enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y, initScale.z);
                }
                else if (target == 0)
                {
                    enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
                }
            }
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public void EnableMove()
    {
        bCanMove = true;
    }

    public void DisableMove()
    {
        bCanMove = false;
    }
}
