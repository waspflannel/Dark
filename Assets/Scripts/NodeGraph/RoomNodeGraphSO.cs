using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RoomNodeGraph", menuName = "Scriptable Objects/Dungeon/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector] public RoomNodeTypeSO roomNodeTypeList;
    [HideInInspector] public List<RoomNodeSO> roomNodes = new();
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionary = new();
}
