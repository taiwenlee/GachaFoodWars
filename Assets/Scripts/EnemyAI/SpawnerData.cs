using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/SpawnerData", order = 1)]
public class SpawnerData : ScriptableObject
{
    public string LevelName;
    public string[] mobNames;
    public Vector3[] spawnPoints;
    // more data can be added about the mobs here
}
