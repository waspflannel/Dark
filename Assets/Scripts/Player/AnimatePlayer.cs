using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;
                        
    private void Awake()
    {

        player = GetComponent<Player>();
    }
    private void OnEnable()
    {
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        player.idleEvent.OnIdle += IdleEvent_OnIdle;
        player.aimWeaponEvent.OnAimWeapon += AimWeaponEvent_OnAimWeapon;
        player.jumpEvent.OnJump += JumpEvent_OnJump;
        player.fallingEvent.OnFallEvent += FallingEvent_OnFallEvent;
    }

    private void OnDisable()
    {
        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;
        player.aimWeaponEvent.OnAimWeapon -= AimWeaponEvent_OnAimWeapon;
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
        if (movementByVelocityEventArgs.isBackwards)
        {
            SetMovementAnimationParameters();

        }
        else
        {
            SetBackwardsMovementAnimationParametrs();
        }

    }

    public void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        SetIdleAnimationParameters();
    }

    public void AimWeaponEvent_OnAimWeapon(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs)
    {
        InitializeAimAnimationParameters();
        SetAimWeaponAnimationParameters(aimWeaponEventArgs.aimDirection);
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

    }

    private void SetBackwardsMovementAnimationParametrs()
    {
        player.animator.SetBool(Settings.isMoving, true);
        player.animator.SetBool(Settings.isJumping, false);
        player.animator.SetBool(Settings.isIdle, false);
        player.animator.SetBool(Settings.isFalling, false);
        player.animator.SetBool(Settings.isBackwards, true);
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
        player.animator.SetBool(Settings.isBackwards, false);
    }

    private void InitializeAimAnimationParameters()
    {
        player.animator.SetBool(Settings.aimLeft, false);
        player.animator.SetBool(Settings.aimRight, false);
    }




    private void SetAimWeaponAnimationParameters(AimDirection aimDirection)
    {
        switch (aimDirection)
        {
            case AimDirection.Left:
                player.animator.SetBool(Settings.aimLeft, true);
                break;
            case AimDirection.Right:
                player.animator.SetBool(Settings.aimRight, true);
                break;
            default:
                player.animator.SetBool(Settings.aimRight, true);
                break;
        }
    }

}