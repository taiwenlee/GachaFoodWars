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

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        exitGate.boxCollider.enabled = true;

        if (exitGate.triggered)
        {
            lc.BuildLevel();
            levelMap.currentVertex = lc.currentVertex;
            int x = (int)levelMap.currentVertex.x;
            int y = (int)levelMap.currentVertex.y;
            levelMap.currentRoom = lc.roomMatrix.cols[x].rows[y];
            levelMap.currentRoomLayout = levelMap.currentRoom.GenerateRandomEntryPoint();
            Initiate.Fade("Game", Color.black, 3.0f);
        }
    }
}
