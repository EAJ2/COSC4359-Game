using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float YAxisOffset = .2f;
    private bool bFollowPlayer = true;
    private bool bFollowPoint = false;
    private bool bInstantPanToPoint = false;
    private Vector3 pointPosition;

    //Follow Player
    [SerializeField] private Transform player;
    private bool bFollowPlayerHeight = true;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1 + YAxisOffset  , -1);

        //room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);

        /*
        if(bFollowPlayer)
        {
            //follow player
            if(bFollowPlayerHeight)
            {
                if (player.position.y > 0)
                {
                    transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(player.position.x, player.position.y * YAxisOffset, transform.position.z);
                }
            }
            else
            {
                if (player.position.y > 0)
                {
                    transform.position = new Vector3(player.position.x, pointPosition.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(player.position.x, pointPosition.y, transform.position.z);
                }
            }
        }
        if(bFollowPoint)
        {
            if(bInstantPanToPoint == true)
            {
                transform.position = new Vector3(pointPosition.x, pointPosition.y, transform.position.z);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(pointPosition.x, pointPosition.y, transform.position.z), speed * Time.deltaTime);
            }
        }
        */
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }

    public void EnableFollowPlayer()
    {
        bFollowPlayer = true;
        bFollowPoint = false;
        bFollowPlayerHeight = true;
    }

    public void SetFollowPlayerHeight(bool follow, Vector3 point)
    {
        bFollowPlayerHeight = follow;
        pointPosition = point;
    }
    public void DisableFollowPlayer()
    {
        bFollowPlayer = false;
    }

    public void MoveCameraToPoint(Vector3 point, bool bInstantPan)
    {
        bFollowPlayer = false;
        bFollowPoint = true;
        bInstantPanToPoint = bInstantPan;
        pointPosition = point;
        //transform.position = new Vector3(point.x, point.y, transform.position.z);
    }
}
