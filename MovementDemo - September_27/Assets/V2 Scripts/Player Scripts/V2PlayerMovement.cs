using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D cc;


    [SerializeField] private bool bCanMove = true;


    [Header("Walk Run Parameters")]
    [SerializeField] private bool bCanWalk = true;
    private bool bIsGrounded;
    private float HorizontalInput;
    [SerializeField] private float Speed;
    private bool bIsFacingRight;

    [Header("JumpParameters")]
    [SerializeField] private bool bCanJump = true;
    [SerializeField] private bool bCanFall = true;
    private bool bIsFalling;
    private bool bIsJumping;
    [SerializeField] private float JumpPower;
    [SerializeField] private float JumpCutMultiplier;

    [Header("Coyote Timer")]
    [SerializeField] private float CoyoteTime;
    private float CoyoteCounter;


    [Header("Double Jumping Parameters")]
    [SerializeField] private bool bCanDoubleJump = true;
    [SerializeField] private int ExtraJumps;
    private int JumpCounter;

    [Header("Dash Parameters")]
    [SerializeField] private bool bCanDash = true;
    private bool bDashing = false;
    [SerializeField] private float DashingPower;
    [SerializeField] private float DashingTimer;
    [SerializeField] private float DashingCooldown;

    [Header("Layers Masks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        //Set bool for grounded here
        //anim.SetBool("grounded", IsGrounded());
        HorizontalInput = Input.GetAxis("Horizontal");
        Debug.Log(HorizontalInput);

        if(bDashing)
        {
            return;
        }
    }

    public void EnableMovement()
    {
        bCanMove = true;
        EnableJump();
        EnableDash();
    }

    public void DisableMovement()
    {
        bCanMove = false;
        DisableJump();
        DisableDash();
    }

    public bool IsGrounded()
    {
        
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(cc.bounds.center, cc.bounds.size, CapsuleDirection2D.Vertical, 0.0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool IsFalling()
    {
        if (rb.velocity.y < -0.1f)
        {
            bIsFalling = true;
            return true;
        }
        else
        {
            bIsFalling = false;
            return false;
        }
    }

    private bool IsJumping()
    {
        if (rb.velocity.y > 0)
        {
            bIsJumping = true;
            return true;
        }
        else
        {
            bIsJumping = false;
            return false;
        }
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public void SetSpeed(float value)
    {
        Speed = value;
    }

    public bool IsFacingRight()
    {
        if(HorizontalInput == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EnableWalk()
    {
        bCanWalk = true;
    }

    public void DisableWalk()
    {
        bCanWalk = false;
    }

    public void EnableJump()
    {
        bCanJump = true;
    }
    public void DisableJump()
    {
        bCanJump = false;
    }

    public bool CanJump()
    {
        return bCanJump;
    }

    public void EnableDoubleJump()
    {
        bCanDoubleJump = true;
    }

    public void DisableDoubleJump()
    {
        bCanDoubleJump = false;
    }

    public bool CanDoubleJump()
    {
        return bCanDoubleJump;
    }

    public void SetGravityScale(float gs)
    {
        rb.gravityScale = gs;
    }

    public int GetNumberOfExtraJumps()
    {
        return ExtraJumps;
    }

    public void SetNumberOfExtraJumps(int val)
    {
        ExtraJumps = val;
    }

    public void EnableDash()
    {
        bCanDash = true;
    }

    public void DisableDash()
    {
        bCanDash = false;
    }

    public bool CanDash()
    {
        return bCanDash;
    }

    public bool IsDashing()
    {
        return bDashing;
    }
}
