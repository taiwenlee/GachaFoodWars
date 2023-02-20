using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class RoomBlueprint
{
    public Vector2 matrixPosition;
    public Vector2 roomLocation;
    public Vector4 roomLayout;
    public bool IsActive { get; set; }
    public bool HasVisited { get; set; }
    public RoomBlueprint North { get; set; }
    public RoomBlueprint East { get; set; }
    public RoomBlueprint South { get; set; }
    public RoomBlueprint West { get; set; }

    public RoomBlueprint(Vector2 matrixPosition)
    {
        IsActive = false;
        HasVisited = false;
        roomLocation = Vector2.zero;
        roomLayout = Vector4.zero;
        this.matrixPosition = matrixPosition;
    }

    public RoomBlueprint(Vector2 matrixPosition, Vector2 loc)
    {
        IsActive = true;
        HasVisited = false;
        roomLocation = loc;
        roomLayout = Vector4.zero;
        this.matrixPosition = matrixPosition;
    }

    // generate entry terminal without a known exit terminal
    public Vector4 GenerateRandomTerminal(bool isEntry)
    {
        Vector4 layout = roomLayout;
        List<int> stack = new();

        for (int i = 0; i < 4; i++)
        {
            if (layout[i] == 0)
            {
                stack.Add(i);
            }
        }

        int index = stack[Random.Range(0, stack.Count)];
        layout[index] = isEntry == true ? 1 : -1;
        return layout;
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
