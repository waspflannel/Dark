using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{

    public event Action<AttackEvent , AttackEventArgs> OnAttack;

    public void CallAttackEvent(bool isAttacking, bool isSpearActivel, bool isSwordActive)
    {
        Debug.Log("AttackEvent Called");  
        OnAttack?.Invoke(this , new AttackEventArgs { isAttacking = isAttacking , isSpearActive = isSpearActivel , isSwordActive = isSwordActive });
    }
}

public class AttackEventArgs : EventArgs
{

    public bool isAttacking;
    public bool isSpearActive;
    public bool isSwordActive;
}
