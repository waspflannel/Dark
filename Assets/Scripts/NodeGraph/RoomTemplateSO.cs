using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Dungeon/Room")]
public class RoomTemplateSO : ScriptableObject
{
    [HideInInspector] public string guid;
    public GameObject prefab;
    public GameObject previousPrefab;
    public RoomNodeTypeSO roomNodeType;

    public Vector2Int spawnLocation;

    public Vector2Int[] spawnPositionArray;


#if UNITY_EDITOR
    private void OnValidate()
    {
        if(prefab != previousPrefab || guid == "")
        {
            guid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);
        }
        HelperUtilities.ValidateCheckEnumerableValues(this , nameof(spawnPositionArray), spawnPositionArray);   
    }
#endif
}
