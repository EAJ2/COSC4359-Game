using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float HealthValue;
    [SerializeField] private float MoveDistanceValue;
    [SerializeField] private Transform item;
    [SerializeField] private ExoV3Movement playerHealth;
    private bool MovingUp = true;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 MovePosition;
    private Vector3 OriginalPosition;

    private void Start()
    {
        MovePosition = item.position;
        MovePosition = new Vector3(MovePosition.x, MovePosition.y + MoveDistanceValue, MovePosition.z);
        OriginalPosition = item.position;
    }

    private void Update()
    {
        if(MovingUp)
        {
            if (item.position.y < MovePosition.y)
            {
                item.position = Vector3.MoveTowards(item.position, MovePosition, speed * Time.deltaTime);
            }
            else
            {
                MovingUp = false;
            }
        }
        else
        {
            if(item.position.y != OriginalPosition.y)
            {
                item.position = Vector3.MoveTowards(item.position, OriginalPosition, speed * Time.deltaTime);
            }
            else
            {
                MovingUp = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<ExoV3Movement>().AddHealth(HealthValue);
            gameObject.SetActive(false);
        }
    }
}
