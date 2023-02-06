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
    private bool bSprinting = false;
    private float HorizontalInput;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private float InAirMoveSpeed;
    private bool bIsFacingRight;
    [SerializeField] private float stamina;
    [SerializeField] private float MAXstamina;


    [Header("JumpParameters")]
    [SerializeField] private bool bCanJump = true;
    [SerializeField] private bool bCanFall = true;
    private bool bIsFalling;
    private bool bIsJumping;
    [SerializeField] private float GravityScale;
    [SerializeField] private float FallGravityScale;
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

    public Stats stats;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<Stats>();
        stats.Agility();
        stats.Endurance();
        if (stats.agilMult > 0)
        {
            WalkSpeed = (WalkSpeed + (stats.agil * 0.25f)) + (stats.agilMult * (WalkSpeed + (stats.agil * 0.25f)));
            SprintSpeed = (SprintSpeed + (stats.agil * 0.33f)) + (stats.agilMult * (SprintSpeed + (stats.agil * 0.33f)));
        }
        else
        {
            WalkSpeed = WalkSpeed + (stats.agil * 0.25f);
            SprintSpeed = SprintSpeed + (stats.agil * 0.33f);
        }

        if (stats.endMult > 0)
        {
            stamina = (stamina + (stats.end * 3f)) + (stats.endMult * (stamina + (stats.end * 3f)));
            MAXstamina = (MAXstamina + (stats.end * 3f)) + (stats.endMult * (MAXstamina + (stats.end * 3f)));
        }
        else
        {
            stamina = stamina + (stats.end * 3f);
            MAXstamina = MAXstamina + (stats.end * 3f);
        }
    }

    private void Update()
    {
        //Set bool for grounded here
        //anim.SetBool("grounded", IsGrounded());
        HorizontalInput = Input.GetAxis("Horizontal");


        if (HorizontalInput != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.StopPlayback();
            anim.SetBool("isMoving", false);
        }


        //If Dashing, nothing else runs until it ends
        if (bDashing)
        {
            return;
        }

        if(IsGrounded())
        {
            SetGravityScale(GravityScale);
        }

        //To Check if the player is sprinting or walking
        if(bCanMove)
        {
            if (bCanWalk)
            {
                if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
                {
                    SetIsSprinting(true);
                    StartCoroutine(StaminaDepletion());
                    StopCoroutine(StaminaRegeneration());
                }
                else
                {
                    SetIsSprinting(false);
                    StopCoroutine(StaminaDepletion());
                    StartCoroutine(StaminaRegeneration());
                }
            }
        }

        //Jump Code
        if(bCanMove)
        {
            if(bCanJump)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                else if(Input.GetKeyUp(KeyCode.Space))
                {
                    OnJumpUp();
                }
                if(IsJumping() || IsFalling() && bCanFall)
                {
                    rb.velocity = new Vector2(HorizontalInput * InAirMoveSpeed, rb.velocity.y);
                }

                if(IsGrounded())
                {
                    CoyoteCounter = CoyoteTime;
                    JumpCounter = ExtraJumps;
                }
                else
                {
                    CoyoteCounter -= Time.deltaTime;
                }
            }
        }

        Flip();
        IsJumping();
        IsFalling();
        //anim.SetBool("falling", IsFalling());
        //anim.SetBool("IsJumping", IsJumping());
    }

    private void FixedUpdate()
    {
        if(bDashing)
        {
            return;
        }

        Run();
    }

    private void Run()
    {
        if(bCanMove)
        {
            if(bCanWalk && IsGrounded() && !bDashing)
            {
                if(IsSprinting())
                {
                    rb.velocity = new Vector2(HorizontalInput * SprintSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(HorizontalInput * WalkSpeed, rb.velocity.y);
                }
            }
        }
    }

    public IEnumerator StaminaDepletion()
    {
        stamina -= 0.005f;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator StaminaRegeneration()
    {
        if (stamina < MAXstamina)
        {
            stamina += 0.003f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Flip()
    {
        if(bIsFacingRight && HorizontalInput < 0f || !bIsFacingRight && HorizontalInput > 0f)
        {
            Vector3 localScale = transform.localScale;
            bIsFacingRight = !bIsFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        if(bCanMove)
        {
            if(bCanJump && bCanFall)
            {
                if(CoyoteCounter <= 0 && JumpPower <= 0)
                {
                    return;
                }

                if (IsGrounded())
                {
                    SetGravityScale(GravityScale);
                    rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                    //anim.SetTrigger("Jump");
                }
                else
                {
                    if(CoyoteCounter > 0)
                    {
                        SetGravityScale(GravityScale);
                        rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                        //anim.SetTrigger("Jump");
                    }
                    else
                    {
                        if(JumpCounter > 0 && bCanDoubleJump)
                        {
                            SetGravityScale(GravityScale);
                            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                            //anim.SetTrigger("DoubleJump");
                            JumpCounter--;
                        }
                    }
                }
                CoyoteCounter = 0;
            }
        }
    }

    private void OnJumpUp()
    {
        if(bCanMove)
        {
            if(bCanJump && bCanFall)
            {
                if(rb.velocity.y > 0 && IsJumping())
                {
                    rb.AddForce(Vector2.down * rb.velocity.y * (1 - JumpCutMultiplier), ForceMode2D.Impulse);
                }
            }
        }
    }

    //Getters and Setters && Enablers and Disablers
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

    public bool IsFalling()
    {
        if (rb.velocity.y < -0.1f)
        {
            SetGravityScale(FallGravityScale);
            bIsFalling = true;
            return true;
        }
        else
        {
            bIsFalling = false;
            return false;
        }
    }

    public bool IsJumping()
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

    public bool IsSprinting()
    {
        return bSprinting;
    }

    private void SetIsSprinting(bool b)
    {
        bSprinting = b;
    }

    public float GetWalkSpeed()
    {
        return WalkSpeed;
    }

    public void SetWalkSpeed(float value)
    {
        WalkSpeed = value;
    }

    public float GetSprintSpeed()
    {
        return SprintSpeed;
    }

    public void SetSprintSpeed(float value)
    {
        SprintSpeed = value;
    }

    public bool IsFacingRight()
    {
        if(transform.localScale.x == 1)
        {
            bIsFacingRight = true;
            return true;
        }
        else
        {
            bIsFacingRight = false;
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

    public void EnableFall()
    {
        SetGravityScale(GravityScale);
        bCanFall = true;
    }

    public void DisableFall()
    {
        SetGravityScale(0f);
        bCanFall = false;
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
