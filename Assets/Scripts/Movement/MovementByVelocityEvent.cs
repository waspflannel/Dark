using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementByVelocityEvent : MonoBehaviour
{

    public event Action<MovementByVelocityEvent , MovementByVelocityEventArgs> OnMovementByVelocity;

    public void CallMovementByVelocityEvent(Vector2 moveDirection, float moveSpeed, bool isJumping, bool _isJumpFalling, float LastOnGroundTime, bool IsFacingRight)
    {
        OnMovementByVelocity?.Invoke(this, new MovementByVelocityEventArgs { moveDirection = moveDirection,
            moveSpeed = moveSpeed, isJumping = isJumping, _isJumpFalling = _isJumpFalling, LastOnGroundTime = LastOnGroundTime,
            IsFacingRight = IsFacingRight
        });
    }
}
public class MovementByVelocityEventArgs : EventArgs
{
    public Vector2 moveDirection;
    public float moveSpeed;
    public bool isJumping;
    public bool _isJumpFalling;
    public float LastOnGroundTime;
    public bool IsFacingRight;
}
