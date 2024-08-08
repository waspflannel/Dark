using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;

public class RoomNodeGraphEditor : EditorWindow
{

    #region member variables
    private GUIStyle roomNodeStyle;
    private static RoomNodeGraphSO currentRoomNodeGraph;
    private RoomNodeTypeListSO roomNodeTypeList;
    private RoomNodeSO currentRoomNode = null;

    #endregion member variables

    #region editor/window methods

    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
    public static void OpenWindow()
    {
        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");

    }
    private void OnEnable()
    {
        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
        roomNodeStyle = new GUIStyle();
        roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        roomNodeStyle.normal.textColor = Color.white;
        roomNodeStyle.padding = new RectOffset(20, 20, 20, 20);
        roomNodeStyle.border = new RectOffset(12, 12, 12, 12);
    }


    [OnOpenAsset(0)]
    public static bool OnDoubleClickAsset(int instanceID , int line)
    {
        RoomNodeGraphSO roomNodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RoomNodeGraphSO;
        if(roomNodeGraph != null)
        {
            OpenWindow();
            currentRoomNodeGraph = roomNodeGraph;
            return true;
        }
        return false;
    }

    private void OnGUI()
    {
        if(currentRoomNodeGraph != null)
        {
            ProcessEvents(Event.current);
            DrawRoomNodes();

        }

        if (GUI.changed)
        {
            Repaint();
        }
    }
    #endregion editor/window methods


    #region window event methods
    private void ProcessEvents(Event currentEvent)
    {
        if (currentRoomNode == null || currentRoomNode.isLeftClickDragging == false) {
            currentRoomNode = isMouseOverRoomNode(currentEvent);
        }
        if(currentRoomNode == null)
        {
            ProcessRoomNodeGraphEvents(currentEvent);
        }
        else
        {
            currentRoomNode.ProcessEvents(currentEvent);
        }
        
    }
    private void ProcessRoomNodeGraphEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;

            default:
                break;
        }
    }
    private void ProcessMouseDownEvent(Event currentEvent)
    {
        if(currentEvent.button ==1)
        {
            ShowContextMenu(currentEvent.mousePosition);
        }

    }

    private RoomNodeSO isMouseOverRoomNode(Event currentEvent)
    {
        for(int i = 0; i < currentRoomNodeGraph.roomNodes.Count; i++)
        {
            if (currentRoomNodeGraph.roomNodes[i].rect.Contains(currentEvent.mousePosition))
            {
                return currentRoomNodeGraph.roomNodes[i];
            }
        }
        return null;
    }

    #endregion window event methods

    #region display methods

    private void ShowContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Add Room Node"), false , CreateRoomNode , mousePosition);
        menu.ShowAsContext();
    }


    private void CreateRoomNode(object mousePositionObject)
    {
        CreateRoomNode(mousePositionObject , roomNodeTypeList.list.Find(x => x.isNone));
    }

    private void CreateRoomNode(object mousePositionObject , RoomNodeTypeSO roomNodeType)
    {
        Vector2 mousePosition = (Vector2)mousePositionObject;
        RoomNodeSO roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();
        currentRoomNodeGraph.roomNodes.Add(roomNode);   
        roomNode.Initialise(new Rect(mousePosition, new Vector2(200,150)), currentRoomNodeGraph , roomNodeType);
        AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);
        AssetDatabase.SaveAssets();
    }


    private void DrawRoomNodes()
    {
        foreach(RoomNodeSO roomNode in currentRoomNodeGraph.roomNodes)
        {
            roomNode.Draw(roomNodeStyle);
        }
    }
    #endregion display methods

}
