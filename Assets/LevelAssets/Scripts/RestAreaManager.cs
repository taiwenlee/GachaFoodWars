using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestAreaManager : MonoBehaviour
{
    [SerializeField] Gate exitGate;
    [SerializeField] int numberOfLevels;
    [SerializeField] int numberOfRooms;

    public LevelMap levelMap;
    public LevelConstructor lc;
    public PlayerLevelProgression plp;

    // Update is called once per frame
    void Update()
    {
        if (exitGate.triggered)
        {
            lc.BuildLevel();
            levelMap.currentVertex = lc.currentVertex;
            int x = (int)levelMap.currentVertex.x;
            int y = (int)levelMap.currentVertex.y;
            levelMap.currentRoom = lc.roomMatrix.cols[x].rows[y];
            levelMap.currentRoomLayout = levelMap.currentRoom.GenerateRoomEntryPoint();
            Initiate.Fade("Game", Color.black, 3.0f);
        }
    }
}
