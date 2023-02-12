using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room Blueprint")]

public class RoomBlueprint : ScriptableObject
{
    public Vector2 roomLocation;
    public Vector4 roomLayout;
}
