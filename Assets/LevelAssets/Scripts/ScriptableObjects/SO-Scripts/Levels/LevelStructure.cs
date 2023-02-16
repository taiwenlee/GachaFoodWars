using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelStructure")] 


public class LevelStructure : ScriptableObject
{
    //public int numberOfRooms;
    [Header("Scene String Names")]
    public string restAreaName;
    public string combatAreaName;

    
}