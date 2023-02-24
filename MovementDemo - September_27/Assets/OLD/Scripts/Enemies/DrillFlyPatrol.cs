using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillFlyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public List<Transform> Waypoints;
    private int target = 0;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float patrolSpeed;
    private bool movingLeft;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private bool bCanMove = true;

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if(bCanMove)
        {
            if (enemy.position == Waypoints[target].position)
            {
                DirectionChange();
            }
            else
            {
                MoveInDirection();
                enemy.position = Vector3.MoveTowards(enemy.position, Waypoints[target].position, patrolSpeed * Time.deltaTime);
                //Height
                Waypoints[2].position = Vector3.MoveTowards(Waypoints[2].position, new Vector3(enemy.position.x, Waypoints[2].position.y, enemy.position.z), patrolSpeed * Time.deltaTime);
                //Attack
                Waypoints[3].position = Vector3.MoveTowards(Waypoints[3].position, new Vector3(enemy.position.x, Waypoints[3].position.y, enemy.position.z), patrolSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if(enemy.position == Waypoints[target].position)
        {
            if(target == 0)
            {
                target = 1;
            }
            else if(target == 1)
            {
                target = 0;
            }
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection()
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
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
