using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPatrol : MonoBehaviour
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
        rb = GetComponentInChildren<Rigidbody2D>();
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
    }

    private void Update()
    {
        if (bCanMove)
        {
            if (!IsGrounded())
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(0f, rb.velocity.y);

            }
            else if (IsGrounded())
            {
                MoveInDirection();
                enemy.position = Vector3.MoveTowards(enemy.position, new Vector3(Waypoints[target].position.x, enemy.position.y, enemy.position.z), speed * Time.deltaTime);
            }
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
            if (!OnWall())
            {
                Idle();
            }
            else
            {
                MoveInDirection();
            }
        }
    }

    private void Idle()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            bMoving = true;
            idleTimer = 0;
            MoveInDirection();
        }
    }

    private void MoveInDirection()
    {
        if (IsGrounded() && bMoving)
        {
            if (target == 1)
            {
                enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y, initScale.z);
            }
            else
            {
                enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
            }
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
