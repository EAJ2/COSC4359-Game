using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite1Direction : MonoBehaviour
{
    [SerializeField] private Transform Object;
    [SerializeField] private Transform Target;
    [SerializeField] private float speed;
    [SerializeField] private bool bHorizontal = true;
    [SerializeField] private bool bDeactiveBoxColliderWhenActivated = false;
    [SerializeField] private bool bDeactivateBoxColliderLast = true;
    [SerializeField] private bool bActivate = false;
    private bool bAtPosition = false;

    private void Awake()
    {
        if(bDeactivateBoxColliderLast)
        {
            bDeactiveBoxColliderWhenActivated = false;
        }
    }

    private void Update()
    {
        if(Object != null && Target != null && bActivate)
        {
            if(bHorizontal)
            {
                if (Object.position.x != Target.position.x)
                {
                    Object.position = Vector3.MoveTowards(Object.position, new Vector3(Target.position.x, Object.position.y, Object.position.z), speed * Time.deltaTime);
                }
                if (Object.position.x == Target.position.x)
                {
                    if (bDeactivateBoxColliderLast)
                    {
                        Object.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    bAtPosition = true;
                }
            }
            else
            {
                if (Object.position.y != Target.position.y)
                {
                    Object.position = Vector3.MoveTowards(Object.position, new Vector3(Object.position.x, Target.position.y, Object.position.z), speed * Time.deltaTime);
                }
                if (Object.position.y == Target.position.y)
                {
                    if (bDeactivateBoxColliderLast)
                    {
                        Object.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    bAtPosition = true;
                }
            }
        }
    }

    public void ActivateObject()
    {
        bActivate = true;
        if(bDeactiveBoxColliderWhenActivated)
        {
            Object.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public bool IsAtPosition()
    {
        return bAtPosition;
    }
}
