using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerCollision : MonoBehaviour
{
    private Transform Object;
    [SerializeField] private Transform PositionToTeleportTo;

    private bool bTriggered = false;

    private void Awake()
    {
        Object = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bTriggered = false;
        }
    }

    public bool IsTriggered()
    {
        return bTriggered;
    }

    public Vector3 GetPosition()
    {
        return PositionToTeleportTo.transform.position;
    }
}
