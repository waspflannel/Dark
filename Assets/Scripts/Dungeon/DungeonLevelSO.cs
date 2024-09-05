using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dungeon Level", menuName = "Scriptable Objects/Dungeon/Dungeon Level")]
public class DungeonLevelSO : ScriptableObject
{
    public string levelName;


    public List<RoomTemplateSO> roomTemplateList;//all room templates that are used in this level



#if UNITY_EDITOR
    public void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this,nameof(roomTemplateList),roomTemplateList);
    }

#endif
}
