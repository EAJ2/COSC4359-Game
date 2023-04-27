using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPatrol : MonoBehaviour
{

    //Fly does not want to idle when reaching a waypoint
    [Header("Patrol Points")]
    public List<Transform> Waypoints;
    private int target = 0;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float BasicSpeed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private bool bCanMove = true;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("Moving", false);
    }

    private void Update()
    {
        if (bCanMove)
        {
            if (enemy.position == Waypoints[target].position)
            {
                DirectionChange();
            }
            else
            {
                MoveInDirection();
                enemy.position = Vector3.MoveTowards(enemy.position, Waypoints[target].position, BasicSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if (enemy.position == Waypoints[target].position)
        {
            target = Random.Range(0, 3);
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("Moving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection()
    {
        idleTimer = 0;
        anim.SetBool("Moving", true);
        //Fly flips right if the waypoint is even, flips left if odd
        // 0    3
        // 2    1
        if (target == 0 || target == 3)
        {
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
        }
        else if(target == 1 || target == 2)
        {
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y, initScale.z);
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
