using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Map")]

public class LevelMap : ScriptableObject
{
    public Vector2 startVertex;

    public Vector2 endVertex;
    public RoomBlueprint endRoom;
    public Vector4 endRoomLayout;

    public Vector2 currentVertex;
    public RoomBlueprint currentRoom;
    public Vector4 currentRoomLayout;

    public void OnEnable()
    {
        startVertex = Vector2.zero;
        endVertex = Vector2.zero;
        currentVertex = Vector2.zero;

        endRoom = null;
        currentRoom = null;
    }

    public void OnDisable()
    {
        startVertex = Vector2.zero;
        endVertex = Vector2.zero;
        currentVertex = Vector2.zero;

        endRoom = null;
        currentRoom = null;
    }

    public bool RoomTransition(Gate gate, ref bool inTransition)
    {
        if (inTransition)
        {
            return false;
        }

        RoomBlueprint rb;
        int exit;
        Vector2 current = currentVertex;

        if (gate.terminal.x == 1)
        {
            current.y -= 1;
            exit = 0;
            rb = currentRoom.North;
        }
        else if (gate.terminal.y == 1)
        {
            current.x += 1;
            exit = 1;
            rb = currentRoom.East;
        }
        else if (gate.terminal.z == 1)
        {
            current.y += 1;
            exit = 2;
            rb = currentRoom.South;
        }
        else
        {
            current.x -= 1;
            exit = 3;
            rb = currentRoom.West;
        }

        if (currentVertex == endVertex &&
            rb == null)
        {
            Initiate.Fade("Rest", Color.black, 3.0f);
        }
        else if (rb == null)
        {
            return false;
        }
        else
        {
            currentVertex = current;
            currentRoom = rb;
            currentRoomLayout = currentRoom.GenerateRoomEntryPoint(exit);
            inTransition = true;
        }

        return true;
    }
}
