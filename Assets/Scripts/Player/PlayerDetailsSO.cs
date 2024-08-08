using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerDetails" , menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    public string playerName;
    public GameObject playerPrefab;
    public RuntimeAnimatorController runtimeAnimator;
    public int playerHealthAmount;
    
}
