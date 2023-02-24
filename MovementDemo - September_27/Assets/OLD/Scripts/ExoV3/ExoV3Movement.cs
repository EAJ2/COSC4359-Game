using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoV3Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    private PlayerCombat pc;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private GameObject healthBar;

    private float HorizontalInput;
    private bool bFalling = false;
    private bool bJumping = false;
    private bool bFacingRight = true;
    private bool bKicked = false;

    [Header("Dash Parameters")]
    private bool bDashing = false;
    private float DashingPower = 24f;
    private float DashingTime = 0.2f;
    [SerializeField] private float DashingCooldown = 1f;
    [SerializeField] private float NormalDashingPower;
    [SerializeField] private float NormalDashingTime;
    [SerializeField] private float BoostDashingPower;
    [SerializeField] private float BoostDashingTime;

    [Header("Layers Masks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private float normalSpeed;
    [SerializeField] private float JumpPower;
    [SerializeField] private float gravityScale = 2f;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private float TimeToMoveAfterDeath;
    [SerializeField] private float FallGravityScale;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int ExtraJumps;
    private int JumpCounter;

    private bool bCanMove = true;
    public bool bCanJump = true;
    public bool bCanDash = true;
    private bool bCanFall = true;
    private bool bCanDoubleJump = false;

    private bool bCouldJump = false;
    private bool bCouldDash = false;

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float IFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Chests")]
    public List<ChestReward> Chests;
    private int ChestIndex;

    private float kickDistance;
    public Transform StartingPoint;
    private Vector3 RespawnLocation;
    private Vector3 OnDeathRespawnLocation;
    private float moveTimer = 0;
    float time = 0;
    private bool bInsideChest = false;
    public int minutes { get; private set; }
    public int hours { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerCombat>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        LoadPlayer();
        normalSpeed = speed;

        DashingPower = NormalDashingPower;
        DashingTime = NormalDashingTime;
    }

    // Update is called once per frame
    void Update()
    {

        //Time for Menu
        time += Time.deltaTime;
        if(dead)
        {
            moveTimer += Time.deltaTime;
            ResetMovementsAfterDeath();
        }
        if(time >= 60)
        {
            minutes += 1;
            time = 0;
        }
        if(minutes == 60)
        {
            hours += 1;
            minutes = 0;
        }

        anim.SetBool("grounded", IsGrounded());
        HorizontalInput = Input.GetAxis("Horizontal");

        if (bDashing)
        {
            return;
        }

        if(IsGrounded())
        {
            SetGravityScale(2f);
        }

        //Dashing Code
        if (Input.GetKey(KeyCode.LeftShift) && bCanDash)
        {
            if(body.velocity.y == 0)
            {
                bFalling = false;
                bJumping = false;
                StartCoroutine(Dash());
            }
            if(body.velocity.y > 0 || body.velocity.y < 0)
            {
                bFalling = false;
                bJumping = false;
                StartCoroutine(Dash());
                bFalling = true;
            }
        }

        //Jump Code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUp();  
        }

        if(IsJumping() || IsFalling() && bCanJump && bCanFall)
        {
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);
        }

        //WallJump
        if(!OnWall() && bCanJump)
        {
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);
            if(IsGrounded())
            {
                coyoteCounter = coyoteTime;
                JumpCounter = ExtraJumps;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }

        //UnlockGear
        if(bInsideChest && Chests[ChestIndex].IsChestLocked() == true)
        {
            if(Input.GetKey(KeyCode.F))
            {
                Chests[ChestIndex].UnlockReward();
                DisableFullMovement();
            }
        }


        //Get Kicked code
        if (bKicked)
        {
            body.velocity = new Vector2(0f, kickDistance);
            //body.position = Vector3.MoveTowards(body.position, new Vector3(body.position.x + kickDistance, body.position.y, transform.localPosition.z), speed * Time.deltaTime);
            anim.SetTrigger("hurt");
        }
        bKicked = false;

        Flip();
        IsJumping();
        IsFalling();
        anim.SetBool("falling", bFalling);
        anim.SetBool("IsJumping", bJumping);
    }

    private void FixedUpdate()
    {
        if(bDashing)
        {
            return;
        }

        Run();
    }

    //Movement
    private void Flip()
    {
        if(bFacingRight && HorizontalInput < 0f || !bFacingRight && HorizontalInput > 0f)
        {
            Vector3 localScale = transform.localScale;
            bFacingRight = !bFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void Run()
    {
        if(bCanMove && IsGrounded() && !bDashing)
        {
            anim.SetBool("run", HorizontalInput != 0);
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }
    public bool IsGrounded()
    {
        //RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, CapsuleDirection2D.Vertical, 0.0f, Vector2.down, 0.1f, groundLayer);
        //RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f);
        return raycastHit.collider != null;
    }

    private bool IsFalling()
    {
        if (body.velocity.y < -0.1f)
        {
            SetGravityScale(FallGravityScale);
            bFalling = true;
        }
        else
        {
            bFalling = false;
        }
        return bFalling;
    }
    private bool IsJumping()
    {
        if(body.velocity.y > 0)
        {
            bJumping = true;
        }
        else
        {
            bJumping = false;
        }
        return bJumping;
    }
    private IEnumerator Dash()
    {
        if(bCanMove && bCanJump && bCanFall && bCanDash)
        {
            bCanDash = false;
            DisableMove();
            bDashing = true;
            SetGravityScale(0f);
            body.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
            tr.emitting = true;
            anim.SetTrigger("dash");
            anim.SetBool("dashing", bDashing);
            yield return new WaitForSeconds(DashingTime);
            tr.emitting = false;
            SetGravityScale(gravityScale);
            bDashing = false;
            EnableMove();
            anim.SetBool("dashing", bDashing);
            yield return new WaitForSeconds(DashingCooldown);
            bCanDash = true;
        }
    }
    private void EnableMove()
    {
        bCanMove = true;
        bCanJump = true;
        bCanFall = true;
    }
    public void EnableWalkingMovement()
    {
        bCanMove = true;
    }
    public void EnableJumpingMovement()
    {
        bCanJump = true;
        bCanFall = true;
    }
    public void EnableDashMovement()
    {
        bCanDash = true;
    }
    public void EnableMovement()
    {
        bCanMove = true;
    }
    public void DisableMovement()
    {
        bCanMove = false;
        body.velocity = new Vector2(0, 0);
    }
    private void SetSpeed(float _speed)
    {
        speed = _speed;
    }
    private void DisableMove()
    {
        bCanFall = false;
        bCanJump = false;
        bCanMove = false;
        bCanDash = false;
    }
    public void SetRespawnLocation(Vector3 sp)
    {
        RespawnLocation = sp;
    }
    public void SpawnInRespawnPoint()
    {
        if(!dead)
        {
            anim.SetTrigger("hurt");
            body.position = RespawnLocation;
        }
    }

    public float GetLocalScaleX()
    {
        //1 == right
        //-1 == left
        return transform.localScale.x;
    }
    public void SetOnDeathRespawnPoint(Vector3 sp)
    {
        OnDeathRespawnLocation = sp;
    }
    public void SpawnInDeathRespawnPoint()
    {
        if(dead)
        {
            body.position = OnDeathRespawnLocation;
            currentHealth = startingHealth / 2;
            anim.SetTrigger("GetUp");
            ResetMovementsAfterDeath();
        }
    }
    public void EnableFullMovement()
    {
        bCanMove = true;
        bCanJump = true;
        bCanDash = true;
    }
    public void DisableFullMovement()
    {
        bCanMove = false;
        bCanJump = false;
        bCanDash = false;
        body.velocity = new Vector2(0f, body.velocity.y);
    }
    private void ResetMovementsAfterDeath()
    {
        if (moveTimer >= TimeToMoveAfterDeath)
        {
            dead = false;
            bCanMove = true;
            if (bCouldJump)
            {
                bCanJump = true;
                bCanFall = true;
            }
            if (bCouldDash)
            {
                bCanDash = true;
            }
            moveTimer = 0;
        }
    }



    //Jump
    private void Jump()
    {
        if(bCanJump)
        {
            if(coyoteCounter <= 0 && !OnWall() && JumpPower <= 0)
            {
                return;
            }

            if (IsGrounded())
            {
                SetGravityScale(2f);
                body.velocity = new Vector2(body.velocity.x, JumpPower);
                anim.SetTrigger("Jump");
            }
            else
            {
                if (coyoteCounter > 0)
                {
                    SetGravityScale(2f);
                    body.velocity = new Vector2(body.velocity.x, JumpPower);
                    anim.SetTrigger("Jump");
                }
                else
                {
                    if (JumpCounter > 0 && bCanDoubleJump)
                    {
                        SetGravityScale(2f);
                        body.velocity = new Vector2(body.velocity.x, JumpPower);
                        anim.SetTrigger("DoubleJump");
                        JumpCounter--;
                    }
                }
            }
            coyoteCounter = 0;
            /*if (IsGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, JumpPower);
                anim.SetTrigger("Jump");
            }*/
        }
    }
    private void OnJumpUp()
    {
        if(bCanJump)
        {
            if (body.velocity.y > 0 && IsJumping())
            {
                body.AddForce(Vector2.down * body.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            }
        }    
    }
    public void EnableJump()
    {
        bCanJump = true;
    }
    public void DisableJump()
    {
        bCanJump = false;
    }
    public void SetGravityScale(float scale)
    {
        body.gravityScale = scale;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }


    //Save
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data == null)
        {
            body.position = StartingPoint.position;
            DisableMove();
            DisableAttack();
            healthBar.SetActive(false);
            body.gravityScale = .5f;
            anim.SetTrigger("FallFromSpace");
            return;
        }
        bCanDash = data.bDash;
        bCanJump = data.bJump;
        SetGravityScale(2f);
        currentHealth = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        minutes = data.minutesPlayed;
        hours = data.hoursPlayed;
        healthBar.SetActive(true);
    }


    //Enable Gear 1 - Double Jump
    public void EnableDoubleJump()
    {
        bCanDoubleJump = true;
        ExtraJumps = 1;
    }
    public void DisableDoubleJump()
    {
        bCanDoubleJump = false;
        ExtraJumps = 0;
    }


    //Power Chips
    public void EnablePower1()
    {
        DashingTime = BoostDashingTime;
        DashingPower = BoostDashingPower;
    }
    public void DisablePower1()
    {
        DashingTime = NormalDashingTime;
        DashingPower = NormalDashingPower;
    }

    public void EnablePower2()
    {
        pc.SetDamage(2);
    }
    public void DisablePower2()
    {
        pc.SetDamage(1);
    }

    public void EnablePower3()
    {
        SetSpeed(12);
    }
    public void DisablePower3()
    {
        SetSpeed(normalSpeed);
    }


    //Health and Damage
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            //player dead
            if (!dead)
            {
                dead = true;
                anim.SetTrigger("dead");

                if(bCanJump)
                {
                    bCouldJump = true;
                }
                if(bCanDash)
                {
                    bCouldDash = true;
                }
                body.velocity = new Vector2(0, 0);
                DisableMove();
                /*GetComponent<ExoV3Movement>().enabled = false;
                GetComponent<PlayerCombat>().DisableAttack();
                DisableMove();
                dead = true;     */
                SpawnInDeathRespawnPoint();
            }
        }
    }
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }
    public void EnableHealthBar()
    {
        healthBar.SetActive(true);
    }
    public void DisableHealthBar()
    {
        healthBar.SetActive(false);
    }
    public void GetKicked(float distance)
    {
        bKicked = true;
        kickDistance = distance;
    }
    private IEnumerator Invulnerability()
    {
        Physics.IgnoreLayerCollision(8, 9, true);
        //invulnerability duration;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IFrameDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(IFrameDuration / (numberOfFlashes * 2));
        }

        Physics.IgnoreLayerCollision(8, 9, false);
    }
    public void EnableAttack()
    {
        PlayerCombat pc = GetComponent<PlayerCombat>();
        pc.EnableAttack();
    }
    public void DisableAttack()
    {
        PlayerCombat pc = GetComponent<PlayerCombat>();
        pc.DisableAttack();
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


    //Chests
    public void SetChestIndex(int index)
    {
        ChestIndex = index;
    }
    public void IsPlayerInsideChest(bool b)
    {
        bInsideChest = b;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded();
        }
    }



}

