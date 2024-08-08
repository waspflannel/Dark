using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AimWeaponEvent : MonoBehaviour
{
    public event Action<AimWeaponEvent, AimWeaponEventArgs> OnAimWeapon;

    public void CallAimWeaponEvent(AimDirection aimDirection, float aimAngle)
    {
        //Debug.Log("aimDirection: " + aimDirection + " aimAngle: " + aimAngle);
        OnAimWeapon?.Invoke(this, new AimWeaponEventArgs { aimDirection = aimDirection, aimAngle = aimAngle });
    }
}

public class AimWeaponEventArgs : EventArgs
{
    public AimDirection aimDirection;
    public float aimAngle;
}
