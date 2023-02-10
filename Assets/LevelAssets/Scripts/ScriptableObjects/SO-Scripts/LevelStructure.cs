using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelStructure")] 

/// LevelStructure holds all level-related data included assets/prefabs
/// 
/// matrixRooms is a 2D List[x][y] containing a List of Lists of room prefabs
/// The rooms that are available for a particular level are declared in the 1D Lists

public class LevelStructure : ScriptableObject
{
    //public int numberOfRooms;
    [Header("Scene String Names")]
    public string restAreaName;
    public string combatAreaName;

    [Space(20)]
    [Header("Set Of Rooms Per Level")]

    [Space(10)]
    public List<GameObject> levelOneRooms;

    [Space(10)]
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