using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform TeleportPoint;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sp;
    private ExoV3Movement player;
    private Vector2 MousePosition;
    private Vector2 WorldPosition;
    private bool bCanTeleport = false; 

    [Header("Cooldown")]
    [SerializeField] private float TeleportCooldown;
    private float cooldownTimer;

    private void Awake()
    {
        player = GetComponent<ExoV3Movement>();
        cooldownTimer = TeleportCooldown;
        anim.enabled = false;
        sp.enabled = false;
    }

    private void Update()
    {
        if(bCanTeleport)
        {
            cooldownTimer += Time.deltaTime;

            //Move Teleport Position to Desired location
            MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            WorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);
            if(!Input.GetKey(KeyCode.Alpha2))
            {
                if(Input.GetKeyUp(KeyCode.Alpha2) && (cooldownTimer >= TeleportCooldown))
                {
                    player.transform.position = TeleportPoint.position;
                    cooldownTimer = 0;
                    anim.enabled = false;
                    sp.enabled = false;
                }
                else
                {
                    TeleportPoint.position = player.transform.position;
                    anim.enabled = false;
                    sp.enabled = false;
                }
            }
            if (Input.GetKey(KeyCode.Alpha2) && (cooldownTimer >= TeleportCooldown))
            {
                anim.enabled = true;
                sp.enabled = true;
                TeleportPoint.position = Vector3.MoveTowards(TeleportPoint.position, WorldPosition, speed * Time.deltaTime);
            }
            /*if (Input.GetKeyUp(KeyCode.Q) && (cooldownTimer >= TeleportCooldown))
            {
                player.transform.position = TeleportPoint.position;
                cooldownTimer = 0;
            }*/
        }
    }

    public void EnableTeleport()
    {
        bCanTeleport = true;
    }

    public void DisableTeleport()
    {
        bCanTeleport = false;
    }
}
