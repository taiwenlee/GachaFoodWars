using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Map")]

public class LevelMap : ScriptableObject
{
    public Vector2 startVertex;
    public Vector2 currentVertex;
    public RoomBlueprint currentRoom;
    public Vector4 currentRoomLayout;
}
