using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float Radius;

    [SerializeField] private GameObject player;
    private Vector2 center;
    private float Angle;

    private void Start()
    {
        center = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = center;
    }

    private void Update()
    {
        center = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        Angle += RotateSpeed + Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)) * Radius;
        transform.position = center + offset;
    }
}
