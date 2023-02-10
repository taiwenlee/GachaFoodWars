using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTNode
{
    public Vector2 coords;
    public Vector4 value;
    public bool isActive;

    public LTNode()
    {
        coords = new();
        value = new();
        isActive = false;
    }

    public LTNode(Vector2 coords)
    {
        this.coords = coords;
        value = new();
        isActive = false;
    }
}

public class LevelConstructor : ScriptableObject
{
    public int depth;
    public int maxPathways;
    public List<List<LTNode>> rootMatrix;

    public void BuildLevel()
    {
        rootMatrix = InitializeLevelGraph(depth);
        int x = Random.Range(0, depth);
        int y = Random.Range(0, depth);
    }

    private List<List<LTNode>> InitializeLevelGraph(int depth)
    {
        List<List<LTNode>> matrix = new();
        for (int i = 0; i < depth; i++)
        {
            matrix.Add(new List<LTNode>());
            for (int j = 0; j < depth; j++)
            {
                matrix[i].Add(
                    new LTNode(
                        new Vector2(i, j)
                    )
                );
            }
        }

        return matrix;
    }

    private void GenerateRoomVector(int x, int y)
    {
        List<int> exitList = new();
        rootMatrix[x][y].value = GenerateRandomVector4(ref exitList, maxPathways);

    }

    private Vector4 GenerateRandomVector4(ref List<int> exitList, int maxPaths, int entryIndex = -1)
    {
        Vector4 root = Vector4.zero;

        if (entryIndex == -1)
        {
            root[Random.Range(0, 4)] = 1;
        }
        else
        {
            root[entryIndex] = 1;
        }

        int exits = Random.Range(0, maxPaths);
        List<int> exitIndices = new();
        do
        {
            int index = Random.Range(0, 4);
            if ((int)root[index] != 1)
            {
                root[index] = -1;
                exitIndices.Add(index);
                exits++;
            }
        }
        while (exits < maxPaths);


        return root;
    }
}
