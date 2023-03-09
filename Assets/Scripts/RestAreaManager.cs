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
    public PlayerProgression pprog;

    [Space(20)]
    [Header("Player Stats")]
    [Tooltip("Reset the player's health while in Rest")]
    [SerializeField] PlayerStats playerStats;

    [Space(20)]
    [Header("Scene Transitions")]
    [SerializeField] string combatScene;
    [SerializeField] string restScene;

    private LevelBlueprint lc;

    private void Start()
    {
        playerStats.playerHealthData.ResetPlayerHealth();
        lc = levelBuildPool.Length > 0 ? 
            levelBuildPool[Random.Range(0, levelBuildPool.Length)] :
            null;
    }

    // Update is called once per frame
    void Update()
    {
        exitGate.boxCollider.enabled = true;

        if (exitGate.triggered)
        {
            bool prepped = LevelMapPrep();

            string nextScene = prepped == true ? 
                combatScene : 
                restScene;
            Initiate.Fade(nextScene, Color.black, 3.0f);
        }
    }

    private bool LevelMapPrep()
    {
        if (lc == null)
            return false;

        lc.BuildLevel();
        levelMap.spawnerMatrix = lc.levelSpawnerData;
        levelMap.roomMatrix = lc.roomMatrix;
        levelMap.currentVertex = lc.currentVertex;
        BoolMapToClearedDeepCopy();

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
        return true;
    }

    private void BoolMapToClearedDeepCopy()
    {
        levelMap.clearedMatrix = new Matrix<bool>();

        for (int c = 0; c < lc.levelBoolMap.cols.Count; c++)
        {
            Rows<bool> boolColumn = lc.levelBoolMap.cols[c];
            levelMap.clearedMatrix.cols.Add(new Rows<bool>());
            
            for (int r = 0; r < boolColumn.rows.Count; r++)
            {
                levelMap.clearedMatrix.cols[c].rows.Add(new());
                levelMap.clearedMatrix.cols[c].rows[r] = boolColumn.rows[r] ? false : true; 
            }
        }
    }
}
