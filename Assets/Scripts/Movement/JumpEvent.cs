using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEvent : MonoBehaviour
{
    public event Action<JumpEvent, JumpEventArgs> OnJump;

    public void CallJumpEvent(bool isJumping, bool _isJumpFalling, float LastOnGroundTime)
    {
        OnJump?.Invoke(this, new JumpEventArgs { isJumping = isJumping, _isJumpFalling = _isJumpFalling, LastOnGroundTime = LastOnGroundTime });
    }
}
public class JumpEventArgs : EventArgs
{
    public bool isJumping;
    public bool _isJumpFalling;
    public float LastOnGroundTime;
}
