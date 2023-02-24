using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraPoint;

    private float normalSize;
    private bool bInside = false;
    private bool bOutside = false;
    private Vector3 cameraPointPosition;

    [Header("Camera Options")]
    [SerializeField] private bool bIncreaseCameraSize = true;
    [SerializeField] private bool bFollowPlayer = true;
    [SerializeField] private float speed;
    [SerializeField] private bool bInstantPanToRoom = false;
    [SerializeField] private bool bFollowPlayerHeight = true;

    [Header("Increase Camera Size")]
    [SerializeField] private float IncreaseSize;

    [Header("Decrease Camera Size")]
    [SerializeField] private float DecreaseSize;

    private BossRoom BR;
    private bool bPanToRoom = false;

    private void Awake()
    {
        normalSize = cam.orthographicSize;
        cameraPointPosition = cameraPoint.position;
        BR = GetComponentInParent<BossRoom>();
    }

    private void Update()
    {
        if(bInside)
        {
            if (bIncreaseCameraSize)
            {
                cam.orthographicSize += 1 * Time.deltaTime * speed;
                if (cam.orthographicSize >= IncreaseSize)
                {
                    cam.orthographicSize = IncreaseSize;
                }
            }
            else if(!bIncreaseCameraSize)
            {
                cam.orthographicSize -= 1 * Time.deltaTime * speed;
                if (cam.orthographicSize <= DecreaseSize)
                {
                    cam.orthographicSize = DecreaseSize;
                }
            }
        }
        else if (bOutside)
        {
            if (bIncreaseCameraSize)
            {
                cam.orthographicSize -= 1 * Time.deltaTime * speed;
                if (cam.orthographicSize <= normalSize)
                {
                    cam.orthographicSize = normalSize;
                    bOutside = false;
                }
            }
            else if(!bIncreaseCameraSize)
            {
                cam.orthographicSize += cam.orthographicSize + 1 * Time.deltaTime * speed;
                if (cam.orthographicSize >= normalSize)
                {
                    cam.orthographicSize = normalSize;
                    bOutside = false;
                }
            }
        }
        if(bInstantPanToRoom && bPanToRoom)
        {
            if (bIncreaseCameraSize)
            {
                cam.orthographicSize += 1 * Time.deltaTime * speed;
                if (cam.orthographicSize >= IncreaseSize)
                {
                    cam.orthographicSize = IncreaseSize;
                    bPanToRoom = false;
                }
            }
            else if (!bIncreaseCameraSize)
            {
                cam.orthographicSize -= 1 * Time.deltaTime * speed;
                if (cam.orthographicSize <= DecreaseSize)
                {
                    cam.orthographicSize = DecreaseSize;
                    bPanToRoom = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            bInside = true;
            bOutside = false;
            if(!bFollowPlayer)
            {
                cam.GetComponent<CameraController>().DisableFollowPlayer();
                cam.GetComponent<CameraController>().MoveCameraToPoint(cameraPointPosition, bInstantPanToRoom);
            }
            if(bFollowPlayerHeight && bFollowPlayer)
            {
                cam.GetComponent<CameraController>().SetFollowPlayerHeight(true, cameraPointPosition);
            }
            else
            {
                cam.GetComponent<CameraController>().SetFollowPlayerHeight(false, cameraPointPosition);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bOutside = true;
            bInside = false;
            cam.GetComponent<CameraController>().EnableFollowPlayer();
        }
    }

    public bool InsideCameraZoom()
    {
        return bInside;
    }

    public bool IsIntantPanToRoom()
    {
        return bInstantPanToRoom;
    }

    public void PanToPoint()
    {
        bPanToRoom = true;
        cam.GetComponent<CameraController>().MoveCameraToPoint(cameraPointPosition, bInstantPanToRoom);
    }
}
  