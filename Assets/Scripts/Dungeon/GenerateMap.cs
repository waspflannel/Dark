using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : SingletonMonobehavoiur<GenerateMap>
{
    private List<DungeonLevelSO> LevelList;
    private int currentLevelIndex;

    private List<RoomTemplateSO> currentLevelMaps;
    private RoomNodeTypeListSO roomNodeTypeList;
    private bool shouldGenerateEntrance = true;
    [SerializeField] private GameObject player;

    [SerializeField]private ScreenFade screenFade;
    private RoomTemplateSO currentRoomTemplate;

    [SerializeField] private GameObject currentRoomPrefab;


 
    private void OnEnable()
    {
        currentRoomPrefab =Instantiate(currentRoomPrefab, new Vector3(5,0,0), Quaternion.identity);
        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
        LevelList = GameManager.Instance.GetLeveList();
        currentLevelIndex = GameManager.Instance.getCurrentLevelIndex();
        currentLevelMaps = LevelList[currentLevelIndex].roomTemplateList;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(GetRandomMap(currentLevelMaps));
        }
    }

    private IEnumerator GetRandomMap(List<RoomTemplateSO> listOfLevelRooms)
    {
        //if(shouldGenerateEntrance)
        //{
        //    RoomTemplateSO entranceRoom = listOfLevelRooms.Find(x => x.roomNodeType.isEntrance);
        //    shouldGenerateEntrance = false;
        //}
        yield return StartCoroutine(screenFade.FadeIn(0.5f));

        int randomIndex = Random.Range(0, listOfLevelRooms.Count);
        RoomTemplateSO randomRoom = listOfLevelRooms[randomIndex];
        currentRoomTemplate = randomRoom;
        InstantiatePlayer();
        Destroy(currentRoomPrefab);
        currentRoomPrefab = Instantiate(currentRoomTemplate.prefab, new Vector3(5, 0, 0), Quaternion.identity);
        StartCoroutine(screenFade.FadeOut(0.3f));
        
    }

    private void InstantiatePlayer()
    {
        if (player != null)
        {
            Debug.Log("here 1");
            player.transform.position = new Vector3(currentRoomTemplate.spawnLocation.x, currentRoomTemplate.spawnLocation.y, 0);
        }
        else
        {
            Debug.Log("here 2");
            Instantiate(player, new Vector3(currentRoomTemplate.spawnLocation.x, currentRoomTemplate.spawnLocation.y, 0), Quaternion.identity);

        }
    }


    private RoomTemplateSO getRandomRoom(List<RoomTemplateSO> listOfLevelRooms)
    {
        int randomIndex = Random.Range(0, listOfLevelRooms.Count);
        RoomTemplateSO randomRoom = listOfLevelRooms[randomIndex];
        return randomRoom;
    }


}

