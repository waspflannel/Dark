using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehavoiur<GameManager>
{
    [SerializeField] private List<DungeonLevelSO> LevelList;

    [SerializeField] private int currentLevelIndex = 0;
    private GameState gameState;


    public int getCurrentLevelIndex()
    {
        return currentLevelIndex;
    }

    public List<DungeonLevelSO> GetLeveList()
    {
        return LevelList;
    }
    private void Start()
    {
        gameState = GameState.gameStareted;

    }

    private void Update()
    {
        HandleGameState();
    }

    private void HandleGameState()
    {
        switch (gameState)
        {
            case GameState.gameStareted:
                PlayLevel(currentLevelIndex);
                break;
        }
    }

    private void PlayLevel(int levelIndex)
    {

    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(LevelList), LevelList);
    }
#endif
}
