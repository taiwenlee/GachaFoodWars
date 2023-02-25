using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestAreaManager : MonoBehaviour
{
    [SerializeField] Gate exitGate;
    [SerializeField] int numberOfLevels;
    [SerializeField] int numberOfRooms;
    [SerializeField] LevelBlueprint[] levelBuildPool;

    [Space(20)]
    public LevelMap levelMap;
    public PlayerLevelProgression plp;

    [Space(20)]
    [Header("Player Stats")]
    [Tooltip("Reset the player's health while in Rest")]
    [SerializeField] PlayerStats playerStats;

    private LevelBlueprint lc;

    private void Start()
    {
        playerStats.playerHealthData.ResetPlayerHealth();
        lc = levelBuildPool[Random.Range(0, levelBuildPool.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        exitGate.boxCollider.enabled = true;

        if (exitGate.triggered)
        {
            lc.BuildLevel();
            levelMap.spawnerMatrix = lc.levelSpawnerData;
            levelMap.roomMatrix = lc.roomMatrix;
            levelMap.currentVertex = lc.currentVertex;
            int x = (int)levelMap.currentVertex.x;
            int y = (int)levelMap.currentVertex.y;
            levelMap.currentRoom = lc.roomMatrix.cols[x].rows[y];
            levelMap.currentRoomLayout = levelMap.currentRoom.GenerateRandomTerminal(true);
            x = (int)lc.endingLocation.x;
            y = (int)lc.endingLocation.y;
            levelMap.endVertex = lc.endingLocation;
            levelMap.endRoom = lc.roomMatrix.cols[x].rows[y];
            levelMap.endRoom.roomLayout = levelMap.endRoom.GenerateRandomTerminal(false);
            lc.roomMatrix.cols[x].rows[y] = levelMap.endRoom;
            Initiate.Fade("Game", Color.black, 3.0f);
        }
    }
}
