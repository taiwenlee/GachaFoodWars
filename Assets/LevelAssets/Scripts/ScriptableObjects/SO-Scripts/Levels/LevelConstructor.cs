using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rows
{
    public RoomBlueprint[] rows;
}

[System.Serializable]
public class RoomMatrix
{
    public Rows[] cols;
}

[CreateAssetMenu(menuName = "Level Constructor")]

public class LevelConstructor : ScriptableObject
{
    public int depth;
    public int maxPathways;
    public Vector2 currentLocation;
    public RoomBlueprint startingRoom;

    public RoomMatrix roomMatrix;
}
