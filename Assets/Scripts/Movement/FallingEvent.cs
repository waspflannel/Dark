using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEvent : MonoBehaviour
{
    public event Action<FallingEvent, FallingEventArgs> OnFallEvent;

    public void CallFallingEvent(bool _isJumpFalling)
    {
        OnFallEvent?.Invoke(this, new FallingEventArgs
        {
            _isJumpFalling = _isJumpFalling
        });
    }

}
public class FallingEventArgs : EventArgs
{
    public bool _isJumpFalling;
}
