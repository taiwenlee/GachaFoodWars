using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelStructure")] 

public class LevelStructure : ScriptableObject
{
    public int currentLevel;

    public int currentRoom;
    public int numberOfRooms;

    public string restAreaName;
    public string combatAreaName;

    public List<GameObject> levelOneRooms;
    public List<GameObject> levelTwoRooms;

    public List<List<GameObject>> matrixRooms;

    private void OnEnable()
    {
        matrixRooms = new List<List<GameObject>>
        {
            levelOneRooms,
            levelTwoRooms
        };
    }
}