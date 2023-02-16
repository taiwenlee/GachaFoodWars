using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBlueprint
{
    public const int CARDINALS = 4;
    public Vector2 matrixPosition;
    public Vector2 roomLocation;
    public Vector4 roomLayout;
    public bool IsActive { get; set; }
    public RoomBlueprint North { get; set; }
    public RoomBlueprint East { get; set; }
    public RoomBlueprint South { get; set; }
    public RoomBlueprint West { get; set; }

    public RoomBlueprint(Vector2 matrixPosition)
    {
        IsActive = false;
        roomLocation = Vector2.zero;
        roomLayout = Vector4.zero;
        this.matrixPosition = matrixPosition;
    }

    public RoomBlueprint(Vector2 matrixPosition, Vector2 loc)
    {
        IsActive = true;
        roomLocation = loc;
        roomLayout = Vector4.zero;
        this.matrixPosition = matrixPosition;
    }

    // generate entry terminal without a known exit terminal
    // this changes the room's layout permanently so it should only be used for
    // the starting room of a level
    public Vector4 GenerateRoomEntryPoint()
    {
        List<int> stack = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            if (roomLayout[i] == 0)
            {
                stack.Add(i);
            }
        }

        int index = stack[Random.Range(0, stack.Count)];
        roomLayout[index] = 1;
        return roomLayout;
    }
    
    // generate entry terminal with a known exit terminal
    public Vector4 GenerateRoomEntryPoint(int exitIndex)
    {
        Vector4 layout = roomLayout;
        switch (exitIndex)
        {
            // coming from north entering from south
            case 0:
                layout.z = 1;
                break;
            // coming from east entering from west
            case 1:
                layout.w = 1;
                break;
            // coming from south entering from north
            case 2:
                layout.x = 1;
                break;
            // coming from west entering from east
            case 3:
                layout.y = 1;
                break;
            default:
                return layout;
        }

        return layout;
    }
}
