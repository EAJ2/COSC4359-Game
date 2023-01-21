using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite2Directions : MonoBehaviour
{
    [SerializeField] private Transform Object;
    public List<Transform> waypoints;
    [SerializeField] private float speed;
    [SerializeField] private bool bMove_Right_Or_Down_First = true;
    [SerializeField] private bool bMoveHorizontal = true;
    [SerializeField] private bool bObjectHasCollider = true;

    [SerializeField] private bool bWaitToMoveAgain = false;
    private int target;
    private bool bSwitch = false;
    [SerializeField] private bool bActivate = false;

    private void Awake()
    {
       if(bMove_Right_Or_Down_First)
       {
            target = 1;
       }
       else
       {
            target = 0;
       }

       if(!bObjectHasCollider)
       {
            Object.GetComponent<BoxCollider2D>().enabled = false;
       }
       else if (bObjectHasCollider)
       {
            Object.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void Update()
    {
        if(Object != null && waypoints != null && bActivate) 
        {
            if (bMoveHorizontal)
            {
                Object.position = Vector3.MoveTowards(Object.position, new Vector3(waypoints[target].position.x, Object.position.y, Object.position.z), speed * Time.deltaTime);
            }
            else if (!bMoveHorizontal)
            {
                Object.position = Vector3.MoveTowards(Object.position, new Vector3(Object.position.x, waypoints[target].position.y, Object.position.z), speed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if(bMoveHorizontal)
        {
            if(bWaitToMoveAgain)
            {
                if(bSwitch)
                {
                    ChangeTargetX();
                    bSwitch = false;
                }
            }
            else
            {
                ChangeTargetX();
            }
        }
        else if (!bMoveHorizontal)
        {
            if(bWaitToMoveAgain)
            {
                if(bSwitch)
                {
                    ChangeTargetY();
                    bSwitch = false;
                }
            }
            else
            {
                ChangeTargetY();
            }
        }
    }

    private void ChangeTargetX()
    {
        if (Object.position.x == waypoints[target].position.x)
        {
            if (target == 0)
            {
                target = 1;
            }
            else if (target == 1)
            {
                target = 0;
            }
        }
    }

    private void ChangeTargetY()
    {
        if (Object.position.y == waypoints[target].position.y)
        {
            if (target == 0)
            {
                target = 1;
            }
            else if (target == 1)
            {
                target = 0;
            }
        }
    }

    public void Switch()
    {
        bSwitch = true;
    }

    public void Activate()
    {
        bActivate = true;
    }

    public bool IsAtLeftUpPosition()
    {
        if(bMoveHorizontal)
        {
            if(Object.position.x == waypoints[0].position.x)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(Object.position.y == waypoints[0].position.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool IsAtRightDownPosition()
    {
        if (bMoveHorizontal)
        {
            if (Object.position.x == waypoints[1].position.x)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Object.position.y == waypoints[1].position.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
