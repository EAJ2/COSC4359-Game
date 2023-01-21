using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorsePatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public List<Transform> Waypoints;
    private int target = 0;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private bool bMoveRightFirst = false;
    private Vector3 initScale;
    private bool bMoving = true;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Layers Masks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private bool bCanMove = true;

    private void Awake()
    {
        initScale = enemy.localScale;
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        if (bMoveRightFirst)
        {
            target = 1;
        }
        else
        {
            target = 0;
        }
    }

    private void OnDisable()
    {
        anim.SetBool("walking", false);
    }

    private void Update()
    {
        if(bCanMove)
        {
            if (!IsGrounded())
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            else if (IsGrounded())
            {
                if (bMoving)
                {
                    MoveInDirection();
                    enemy.position = Vector3.MoveTowards(enemy.position, new Vector3(Waypoints[target].position.x, enemy.position.y, enemy.position.z), speed * Time.deltaTime);
                }
                if (!bMoving)
                {
                    Idle();
                }
                if (OnWall())
                {
                    ChangeDirection();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (enemy.position.x == Waypoints[target].position.x)
        {
            bMoving = false;
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

    private void Idle()
    {
        anim.SetBool("walking", false);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            bMoving = true;
            idleTimer = 0;
        }
    }

    private void MoveInDirection()
    {
        if (IsGrounded() && bMoving)
        {
            anim.SetBool("walking", true);
            if (target == 1)
            {
                enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
            }
            else
            {
                enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y, initScale.z);
            }
        }
        else
        {
            anim.SetBool("walking", false);
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

    public void ChangeDirection()
    {
        if (target == 1)
        {
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
            target = 0;
        }
        else
        {
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y, initScale.z);
            target = 1;
        }
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
