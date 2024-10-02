using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Detail", menuName = "Scriptable Objects/Weapon/Weapon")]

public class WeaponDetailsSO : ScriptableObject
{
    public string weaponName;
    public int baseWeaponDamage;
    public float weaponAttackBufferTime = 0.4f;
}

