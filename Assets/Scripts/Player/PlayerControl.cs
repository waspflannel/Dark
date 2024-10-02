using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Player player;
    [SerializeField] private MovementDetailsSO movementDetails;
    private float moveSpeed;

    public Rigidbody2D rb { get; private set; }
    public bool IsFacingRight { get; private set; } = true;
    public bool IsAttacking { get; private set; } = false;
    public bool IsJumping { get; private set; }
    public bool isInAir { get; private set; }
    public float LastOnGroundTime { get; private set; }
    public float LastPressedJumpTime { get; private set; }


    private bool _isJumpFalling;

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private LayerMask _groundLayer;

    private Vector2 direction;

    private float horizontalMovement;
    private float verticalMovement;

    private void Awake()
    {
        player = GetComponent<Player>();
        moveSpeed = movementDetails.getMoveSpeed();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetGravityScale(movementDetails.gravityScale);
        IsFacingRight = true;
        isInAir = false;
    }

    private void Update()
    {
        UpdateTimeVariables();
        ProcessMovementInput();
    }

    private void UpdateTimeVariables()
    {
        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
    }

    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }

    private void FixedUpdate()
    {
        direction = new Vector2(horizontalMovement, 0);
        IsFacingRight = CheckFaceDirection(horizontalMovement);
        Debug.Log(IsFacingRight);
        if (direction != Vector2.zero && !isInAir && !IsAttacking)
        {
            player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed, IsJumping, _isJumpFalling, LastOnGroundTime , IsFacingRight);
        }
        else if (!isInAir)
        {
            
            player.idleEvent.CallIdleEvent(IsFacingRight);
        }
    }


    private void ProcessMovementInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnAttackInput();
        }

        CheckGroundStatus();

        CheckParamatersAndInitiateJump();

        AdjustGravityAndVelocity();
    }

    private void CheckParamatersAndInitiateJump()
    {
        if (IsJumping && rb.velocity.y < 0)
        {
            _isJumpFalling = true;
            player.fallingEvent.CallFallingEvent(_isJumpFalling);
            IsJumping = false;
        }

        if (LastOnGroundTime > 0 && !IsJumping)
        {
            _isJumpFalling = false;
        }

        if (CanJump() && LastPressedJumpTime > 0)
        {
            PerformJump();
        }
    }

    private bool CheckFaceDirection(float horizontalMovement)
    {
        if (horizontalMovement > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (horizontalMovement < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
        return IsFacingRight;
    }

    private void CheckGroundStatus()
    {
        if (!IsJumping && Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
        {
            isInAir = false;
            LastOnGroundTime = movementDetails.coyoteTime;
        }
    }



    private void AdjustGravityAndVelocity()
    {
        if (rb.velocity.y < 0 && verticalMovement < 0)
        {
            SetGravityScale(movementDetails.gravityScale * movementDetails.fastFallGravityMult);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -movementDetails.maxFastFallSpeed));
        }
        else if ((IsJumping || _isJumpFalling) && Mathf.Abs(rb.velocity.y) < movementDetails.jumpHangTimeThreshold)
        {
            SetGravityScale(movementDetails.gravityScale * movementDetails.jumpHangGravityMult);
        }
        else if (rb.velocity.y < 0)
        {
            SetGravityScale(movementDetails.gravityScale * movementDetails.fallGravityMult);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -movementDetails.maxFallSpeed));
        }
        else
        {
            SetGravityScale(movementDetails.gravityScale);
        }
    }






    private bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;
    }


    public void OnJumpInput()
    {
        LastPressedJumpTime = movementDetails.jumpInputBufferTime;
        player.jumpEvent.CallJumpEvent(IsJumping, _isJumpFalling, LastOnGroundTime);
    }
    public void OnAttackInput()
    {
        IsAttacking = true;
        player.attackEvent.CallAttackEvent(IsAttacking);
    }


    private void OnAttackFinished()
    {
        IsAttacking = false;
    }

    private void PerformJump()
    {
        IsJumping = true;
        _isJumpFalling = false;
        Jump();
    }
    private void Jump()
    {
        isInAir = true;
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        float force = movementDetails.jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }


  
}
