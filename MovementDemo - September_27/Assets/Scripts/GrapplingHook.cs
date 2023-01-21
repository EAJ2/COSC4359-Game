using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private ExoV3Movement player;
    [SerializeField] private GameObject TargetCollider;
    private SpriteRenderer sp;
    private Vector2 MousePosition;
    private Vector2 WorldPosition;
    private bool bCanGrapple = false;
    private bool bHookOnTarget = false;
    private bool bGetMousePosition = true;
    private bool bHookReturnToPlayer = false;
    private bool bTargetOnPlayer = true;
    private bool bKeyPressed = false;
    private bool bPointReachedTarget = false;
    private bool bPointCollidedWithCollider = false;
    [SerializeField] private float PointSpeed;
    [SerializeField] private float PlayerToTargetSpeed;
    [SerializeField] private float GrappleShotSpeed;
    private Vector3 TargetPosition;
    private Vector3 Vec3WorldPosition;

    private List<GameObject> colliders = new List<GameObject>();

    [Header("Cooldown")]
    [SerializeField] private float GrappleCooldown;
    private float cooldownTimer;

    private void Awake()
    {
        //player = GetComponent<ExoV3Movement>();
        cooldownTimer = GrappleCooldown;
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (bCanGrapple)
        {
            //Hook starts from player
            if (bTargetOnPlayer)
            {
                point.position = player.transform.position;
            }

            //Get Mouse Position to Hook to
            if(bGetMousePosition)
            {
                //Mouse Position
                MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                WorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);
                Vec3WorldPosition = new Vector3(WorldPosition.x, WorldPosition.y, point.position.z);
            }

            //Shoot hook
            if (Input.GetKey(KeyCode.Alpha3) && cooldownTimer >= GrappleCooldown && !bKeyPressed)
            {
                colliders.Add(Instantiate(TargetCollider, Vec3WorldPosition, Quaternion.identity));

                bKeyPressed = true;
                sp.enabled = true;
                bTargetOnPlayer = false;
                bGetMousePosition = false;
                player.DisableMovement();
                player.DisableJump();
            }

            //Shoot hook if cooldown time is ready and Cannot activate hook because it can hook again
            if (bKeyPressed && !bPointReachedTarget)
            {
                point.position = Vector3.MoveTowards(point.position, Vec3WorldPosition, GrappleShotSpeed * Time.deltaTime);
                if (bPointCollidedWithCollider && !bHookOnTarget)
                {
                    bHookReturnToPlayer = true;
                    bPointReachedTarget = true;
                    cooldownTimer = 0;
                    foreach (GameObject x in colliders)
                    {
                        Destroy(x);
                    }
                }
            }

            //return the hook to the player
            if (bHookReturnToPlayer)
            {
                point.position = Vector3.MoveTowards(point.position, player.transform.position, GrappleShotSpeed * Time.deltaTime);
            }

            //if hook is supposed to return to player and the hook is at the player position, enable stuff, turn off sprite
            if(bHookReturnToPlayer && bPointCollidedWithCollider && (point.position == player.transform.position))
            {
                bPointCollidedWithCollider = false;
                bHookReturnToPlayer = false;
                player.EnableMovement();
                player.EnableJump();
                bTargetOnPlayer = true;
                sp.enabled = false;
                bGetMousePosition = true;
                bKeyPressed = false;
                bPointReachedTarget = false;
            }

            //if hook hit target, move the player to position
            if(bHookOnTarget)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, TargetPosition, PlayerToTargetSpeed * Time.deltaTime);
            }

            //if the player is at the target position, enable stuff
            if (player.transform.position == TargetPosition)
            {
                player.EnableMovement();
                player.EnableJump();
                cooldownTimer = 0;
                bHookOnTarget = false;
                point.position = player.transform.position;
                bTargetOnPlayer = true;
                sp.enabled = false;
                bGetMousePosition = true;
                bKeyPressed = false;
                bPointReachedTarget = false;
            }
        }
    }

    public void EnableGrapple()
    {
        bCanGrapple = true;
    }

    public void DisableGrapple()
    {
        bCanGrapple = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HookTarget")
        {
            bHookOnTarget = true;
            TargetPosition = collision.transform.position;
        }
        if(collision.tag == "TargetCollider")
        {
            bPointCollidedWithCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "HookTarget")
        {
            bHookOnTarget = false;
        }
    }
}
