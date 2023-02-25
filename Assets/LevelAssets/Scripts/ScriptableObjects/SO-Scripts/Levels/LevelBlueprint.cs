using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rows<T>
{
    public List<T> rows;

    public Rows()
    {
        rows = new List<T>();
    }
    
}

[System.Serializable]
public class Matrix<T>
{
    public List<Rows<T>> cols;

    public Matrix()
    {
        cols = new List<Rows<T>>();
    }
}

[CreateAssetMenu(menuName = "Level Constructor")]

public class LevelBlueprint : ScriptableObject
{
    public Vector2 currentVertex;
    public Vector2 startingLocation;
    public Vector2 endingLocation;

    [Space(20)]
    public Matrix<bool> levelBoolMap;
    public Matrix<SpawnerData> levelSpawnerData;
    public Matrix<RoomBlueprint> roomMatrix;

    public void OnEnable()
    {
        currentVertex = Vector2.zero;
        int x = (int)startingLocation.x;
        int y = (int)startingLocation.y;
        if (levelBoolMap.cols[x].rows[y] == false ) 
        {
            Debug.LogError("INVALID START POINT GIVEN");
            Debug.LogError($"Level Build: {name}");
            Debug.Break();
        }
        x = (int)endingLocation.x;
        y = (int)endingLocation.y;
        if (levelBoolMap.cols[x].rows[y] == false)
        {
            Debug.LogError("INVALID END POINT GIVEN");
            Debug.LogError($"Level Build: {name}");
            Debug.Break();
        }
    }

    public void OnDisable()
    {
        currentVertex = Vector2.zero;
    }

    public void BuildLevel()
    {
        roomMatrix= new Matrix<RoomBlueprint>();
        for (int c = 0; c < levelBoolMap.cols.Count; c++)
        {
            Rows<bool> boolColumn = levelBoolMap.cols[c];
            roomMatrix.cols.Add(new Rows<RoomBlueprint>());

            for (int r = 0; r < boolColumn.rows.Count; r++)
            {
                roomMatrix.cols[c].rows.Add(new RoomBlueprint(new Vector2(c, r)));

                // axiom represents the room in question
                var axiom = roomMatrix.cols[c].rows[r];

                // if the bool value for levelBoolMap[c][r] is false
                // do not build a room at that location
                if (!boolColumn.rows[r])
                    continue;
                else
                    axiom.IsActive = true;

                // connect axiom to neighboring rooms
                ConnectAxiomRooms(c, r, ref axiom);
            }
        }

        currentVertex = new Vector2(
            startingLocation.x, 
            startingLocation.y
        );
    }

    // first checks if the neighboring rooms are active
    // then connects them based on north-south or east-west cardinality
    private void ConnectRooms(bool isNS, ref RoomBlueprint rb1, ref RoomBlueprint rb2)
    {
        if (!rb1.IsActive || !rb2.IsActive)
            return;

        // if isNS is true
        // rb1 = north, rb2 = south
        // link north room's south connector to the south room
        // then repeat vice versa

        // if isNS is false
        // rb1 = east, rb2 = west
        // link east room's west connector to the west room
        // then repeat vice versa

        if (isNS)
        {
            rb1.South = rb2;
            rb1.roomLayout.z = -1;
            rb2.North = rb1;
            rb2.roomLayout.x = -1;
            
        }
        else
        {
            rb1.West = rb2;
            rb1.roomLayout.w = -1;
            rb2.East = rb1;
            rb2.roomLayout.y = -1;
        }
    }

    private void ConnectAxiomRooms(int c, int r, ref RoomBlueprint axiom)
    {
        // check if axiom has a room to the north
        if (r - 1 > -1)
        {
            RoomBlueprint rb = roomMatrix.cols[c].rows[r - 1];
            ConnectRooms(true, ref rb, ref axiom);
        }

        // check if axiom has a room to the south
        if (r + 1 < roomMatrix.cols[c].rows.Count)
        {
            RoomBlueprint rb = roomMatrix.cols[c].rows[r + 1];
            ConnectRooms(true, ref axiom, ref rb);
        }

        // check if axiom has a room to the west
        if (c - 1 > -1)
        {
            RoomBlueprint rb = roomMatrix.cols[c - 1].rows[r];
            ConnectRooms(false, ref axiom, ref rb);
        }

        // check if axiom has a room to the east
        if (c + 1 < roomMatrix.cols.Count)
        {
            RoomBlueprint rb = roomMatrix.cols[c + 1].rows[r];
            ConnectRooms(false, ref rb, ref axiom);
        }
    }

    public RoomBlueprint GetRoomFromMatrix(int x, int y)
    {
        return roomMatrix.cols[x].rows[y];
    }
}
