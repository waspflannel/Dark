using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{

    public event Action<AttackEvent , AttackEventArgs> OnAttack;

    public void CallAttackEvent(bool isAttacking)
    {
        Debug.Log("AttackEvent Called");  
        OnAttack?.Invoke(this , new AttackEventArgs { isAttacking = isAttacking });
    }
}

public class AttackEventArgs : EventArgs
{

    public bool isAttacking;
}
