using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryLocation
{
    player,
    count
}

public enum Orientation
{
    east,
    west,
}

public enum AimDirection
{
    Left,
    Right
}

public enum GameState
{
    gameStareted,
    playingLevel,
    engagingEnemies,
    bossStage,
    engagingBoss,
    levelCompleted,
    gamePaused,
    gameWon,
    gameLost,
    restartGame,
}

public enum ItemType
{
    buffItem,
    consumableItem,
    essenceItem,    
    none,
    count
}
