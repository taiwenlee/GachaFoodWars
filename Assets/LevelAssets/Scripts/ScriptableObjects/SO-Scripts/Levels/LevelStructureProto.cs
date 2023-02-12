using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelStructureProto")]

public class LevelStructureProto : ScriptableObject
{
    //public int numberOfRooms;
    [Header("Scene String Names")]
    public string restAreaName;
    public string combatAreaName;

    [Space(20)]
    [Header("Set Of Rooms Per Level")]

    [Space(10)]
    public List<Vector4> levelOneRooms;

    [Space(10)]
    public List<Vector4> levelTwoRooms;

    private List<List<Vector4>> matrixRooms;

    private void OnEnable()
    {
        matrixRooms = new List<List<Vector4>>
        {
            levelOneRooms,
            levelTwoRooms
        };
    }

    private bool ValidateVector(Vector4 vec)
    {
        int plusOneCount = 0;
        int minusOneCount = 0;

        for (int i = 0; i < 4; i++)
        {
            if (vec[i] > 1 || vec[i] < -1)
            {
                return false;
            }

            plusOneCount += vec[i] == 1 ? 1 : 0;
            minusOneCount += vec[i] == -1 ? 1 : 0;
        }

        if (plusOneCount != 1) 
        {
            return false;
        }

        if (minusOneCount > 3 || minusOneCount < 1) 
        {
            return false;
        }

        return true;
    }

    public List<List<Vector4>> GetMatrix() { return matrixRooms; }

    public bool GetRoomVector(out Vector4 vector, int levelIndex, int roomIndex)
    {
        if (!ValidateVector(matrixRooms[levelIndex][roomIndex]))
        {
            vector = Vector4.zero;
            return false;
        }
        else
        {
            vector = matrixRooms[levelIndex][roomIndex];
            return true;
        }
    }
}
