using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLevels : MonoBehaviour
{
    public List<CheckTriggerCollision> Points;
    [SerializeField] private ExoV3Movement player;

    private Vector3 Point1Position;
    private Vector3 Point2Position;
    private Vector3 Point3Position;
    private Vector3 Point4Position;

    private void Awake()
    {
        Point1Position = Points[0].GetPosition();
        Point2Position = Points[1].GetPosition();
        Point3Position = Points[2].GetPosition();
        Point4Position = Points[3].GetPosition();
    }

    private void Update()
    {
        if(Points[0].IsTriggered() == true)
        {
            player.transform.position = new Vector3(Point2Position.x, Point2Position.y, player.transform.position.z);
        }
        if (Points[1].IsTriggered() == true)
        {
            player.transform.position = new Vector3(Point1Position.x, Point1Position.y, player.transform.position.z);
        }

        if (Points[2].IsTriggered() == true)
        {
            player.transform.position = new Vector3(Point4Position.x, Point4Position.y, player.transform.position.z);
        }
        if (Points[3].IsTriggered() == true)
        {
            player.transform.position = new Vector3(Point3Position.x, Point3Position.y, player.transform.position.z);
        }
    }
}
