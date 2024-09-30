using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class IdleEvent : MonoBehaviour
{
    public event Action<IdleEvent , IdleEventArgs> OnIdle;

    public void CallIdleEvent(bool IsFacingRight)
    {
        OnIdle?.Invoke(this , new IdleEventArgs { IsFacingRight = IsFacingRight } );
    }



}
public class IdleEventArgs : EventArgs
{
    public bool IsFacingRight;
}
