using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;
    private SpriteRenderer spriteRenderer;
                        
    private void Awake()
    {

        player = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void OnEnable()
    {
      
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        player.idleEvent.OnIdle += IdleEvent_OnIdle;
        player.jumpEvent.OnJump += JumpEvent_OnJump;
        player.fallingEvent.OnFallEvent += FallingEvent_OnFallEvent;
    }

    private void OnDisable()
    {
        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;
        player.jumpEvent.OnJump -= JumpEvent_OnJump;
        player.fallingEvent.OnFallEvent -= FallingEvent_OnFallEvent;
    }

    private void FallingEvent_OnFallEvent(FallingEvent fallingEvent, FallingEventArgs fallingEventArgs)
    {
        if (fallingEventArgs._isJumpFalling)
            SetJumpFallAnimationParameters();
    }

    public void JumpEvent_OnJump(JumpEvent jumpEvent, JumpEventArgs jumpEventArgs)
    {
        SetJumpAnimationParameters();
    }

    public void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
    {
        if (movementByVelocityEventArgs.IsFacingRight)
        {
            SetMovementAnimationParameters();
           
        }
        else
        {
            SetLeftMovementAnimationParameters();
        }

    }

    public void IdleEvent_OnIdle(IdleEvent idleEvent , IdleEventArgs idleEventArgs)
    {
        if (idleEventArgs.IsFacingRight)
            SetIdleAnimationParameters();
        else
            SetLeftIdleAnimationParameters();
    }



    private void SetJumpAnimationParameters()
    {

        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isIdle, false);
        player.animator.SetBool(Settings.isJumping, true);
        player.animator.SetBool(Settings.isFalling, false);

    }
    private void SetMovementAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, true);
        player.animator.SetBool(Settings.isJumping, false);
        player.animator.SetBool(Settings.isIdle, false);
        player.animator.SetBool(Settings.isFalling, false);
        player.animator.SetBool(Settings.isFacingRight, true);

    }
    private void SetLeftMovementAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, true);
        player.animator.SetBool(Settings.isJumping, false);
        player.animator.SetBool(Settings.isIdle, false);
        player.animator.SetBool(Settings.isFalling, false);
        player.animator.SetBool(Settings.isFacingRight, false);

    }

    private void SetJumpFallAnimationParameters()
    {
 
        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isIdle, false);
        player.animator.SetBool(Settings.isJumping, false);
        player.animator.SetBool(Settings.isFalling, true);

    }

    private void SetIdleAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isJumping, false);
        player.animator.SetBool(Settings.isIdle, true);
        player.animator.SetBool(Settings.isFalling, false);
        player.animator.SetBool(Settings.isFacingRight, true);
    }
    private void SetLeftIdleAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isJumping, false);
        player.animator.SetBool(Settings.isIdle, true);
        player.animator.SetBool(Settings.isFalling, false);
        player.animator.SetBool(Settings.isFacingRight, false);
    }


}