using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerLevelProgression")]

/// Contains static data regarding the player's level progression
/// After the player clears all rooms in a level's room pool (defined in LevelStructure),
/// the corresponding boolean for that level is set to true.
/// During next combat, the game will load the next level's rooms.
/// 
/// Room Index is used in RoomManager to track which room has been cleared already.
/// Right now, the structure is linear

public class PlayerLevelProgression : ScriptableObject
{
    [Header("Base Metrics (do not modify)")]
    [SerializeField] bool gameStartInit = false;
    [SerializeField] bool level1CompleteInit = false;
    [SerializeField] bool level2CompleteInit = false;
    [SerializeField] bool level3CompleteInit = false;
    [SerializeField] bool level4CompleteInit = false;
    [SerializeField] bool level5CompleteInit = false;

    [Space(12)]
    [Header("In-Game Metrics")]
    public bool gameStarted;

    [Space(5)]
    [Header("Level 1")]
    public int level1RoomIndex;
    public bool level1Complete;

    [Space(5)]
    [Header("Level 2")]
    public int level2RoomIndex;
    public bool level2Complete;

    [Space(5)]
    [Header("Level 3")]
    public int level3RoomIndex;
    public bool level3Complete;

    [Space(5)]
    [Header("Level 4")]
    public int level4RoomIndex;
    public bool level4Complete;

    [Space(5)]
    [Header("Level 5")]
    public int level5RoomIndex;
    public bool level5Complete;

    private void OnEnable()
    {
        gameStarted = gameStartInit;

        level1Complete = level1CompleteInit;
        level2Complete = level2CompleteInit;
        level3Complete = level3CompleteInit;
        level4Complete = level4CompleteInit;
        level5Complete = level5CompleteInit;

        level1RoomIndex = 0;
        level2RoomIndex = 0;
        level3RoomIndex = 0;
        level4RoomIndex = 0;
        level5RoomIndex = 0;
    }

    public int GetLevelIndex()
    {
        if (!level1Complete) return 0;
        else if (!level2Complete) return 1;
        else if (!level3Complete) return 2;
        else if (!level4Complete) return 3;
        else if (!level5Complete) return 4;
        else return 5;
    }

    public void CompleteCurrentLevel(int levelIndex)
    {
        switch(levelIndex)
        {
            case 0: level1Complete = true; break;
            case 1: level2Complete = true; break;
            case 2: level3Complete = true; break;
            case 3: level4Complete = true; break;
            case 4: level5Complete = true; break;
            default: return;
        }
    }

    public int GetRoomIndex(int levelIndex)
    {
        return levelIndex switch
        {
            0 => level1RoomIndex,
            1 => level2RoomIndex,
            2 => level3RoomIndex,
            3 => level4RoomIndex,
            4 => level5RoomIndex,
            _ => -1,
        };
    }

    public void IncrementRoomIndex(int levelIndex)
    {
        switch(levelIndex)
        {
            case 0: level1RoomIndex++; break;
            case 1: level2RoomIndex++; break;
            case 2: level3RoomIndex++; break;
            case 3: level4RoomIndex++; break;
            case 4: level5RoomIndex++; break;
            default: return;
        }
    }
}
