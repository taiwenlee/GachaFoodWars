using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room Constructor")]

/// Builds a room out of prefab instances
/// Vector4 coordinates that are passed into BuildRoom are normalized integers
/// These coordinates indicate how the perimeter of the room will be shaped
/// to accommodate gates, boundaries, etc.

public class RoomConstructor : ScriptableObject
{
    public GameObject player;
    public GameObject ground;

    [Space(10)]
    public GameObject treeStackNorth;
    public GameObject treeStackEast;
    public GameObject treeStackSouth;
    public GameObject treeStackWest;

    [Space(10)]
    public GameObject bushStackNorth;
    public GameObject bushStackEast;
    public GameObject bushStackSouth;
    public GameObject bushStackWest;

    [Space(10)]
    public GameObject northEntryGate;
    public GameObject eastEntryGate;
    public GameObject southEntryGate;
    public GameObject westEntryGate;

    [Space(10)]
    public EntryGate[] entryObjects;

    [Space(10)]
    public GameObject northExitGate;
    public GameObject eastExitGate;
    public GameObject southExitGate;
    public GameObject westExitGate;

    [Space(10)]
    public GameObject[] boundaries;

    public void BuildRoom(
        out GameObject thisPlayer,
        Transform origin, // origin point to build around
        Vector4 pathCoords // indicates which side of the room is either an entry or exit (N, E, S, W format like a clock) 
        )
    {
        int[] coords =
        {
            (int)pathCoords.x,
            (int)pathCoords.y,
            (int)pathCoords.z,
            (int)pathCoords.w
        };

        bool north = pathCoords.x == 0;
        bool east = pathCoords.y == 0;
        bool south = pathCoords.z == 0;
        bool west = pathCoords.w == 0;
        bool[] cardinals = { north, east, south, west };

        Instantiate(ground, origin);
        InstantiateTreeStack(origin, out GameObject[] ts);
        InstantiateBushStack(origin, out GameObject[] bs);
        OmitTreeAndBushSections(
            ts, bs, 
            pathCoords.x == 0, 
            pathCoords.y == 0, 
            pathCoords.z == 0, 
            pathCoords.w == 0
        );
        BundleEntryExitGates(out Tuple<GameObject, GameObject>[] gates);
        InstantiateGates(out EntryGate entryObject, gates, coords, origin);
        InstantiateBoundaries(cardinals);
        thisPlayer = Instantiate(player, entryObject.position, Quaternion.identity);
        
    }
   
    private void InstantiateTreeStack(Transform origin, out GameObject[] stack)
    {
        stack = new GameObject[]
        {
            Instantiate(treeStackNorth, origin), // N
            Instantiate(treeStackEast, origin),  // E
            Instantiate(treeStackSouth, origin), // S
            Instantiate(treeStackWest, origin),  // W
        };
    }

    private void InstantiateBushStack(Transform origin, out GameObject[] stack)
    {
        stack = new GameObject[]
        {
            Instantiate(bushStackNorth, origin), // N
            Instantiate(bushStackEast, origin),  // E
            Instantiate(bushStackSouth, origin), // S
            Instantiate(bushStackWest, origin)   // W
        };
    }

    private void InstantiateGates(
        out EntryGate entryObject,
        Tuple<GameObject, GameObject>[] gates,
        int[] coords,
        Transform origin
        )
    {
        List<GameObject> gateList = new();
        EntryGate entryGate = null;
        for (int i = 0; i < gates.Length; i++)
        {
            switch (coords[i])
            {
                case -1:
                    gateList.Add(Instantiate(gates[i].Item2, origin));
                    break;
                case 1:
                    Instantiate(gates[i].Item2, origin);
                    entryGate = entryObjects[i];
                    break;
                default:
                    break;
            }
        }

        for (int j = 0; j < gateList.Count; j++)
        {
            gateList[j].GetComponent<Gate>().entryPoint = entryGate != null ? entryGate : null;
        }

        entryObject = entryGate;
    }

    private void InstantiateBoundaries(bool[] cards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == true)
            {
                Instantiate(boundaries[i]);
            }
        }
    }

    private void OmitTreeAndBushSections(
        GameObject[] ts, 
        GameObject[] bs, 
        bool north,
        bool east,
        bool south,
        bool west
        )
    {
        // North
        ts[0].transform.GetChild(4).gameObject.SetActive(north);
        bs[0].transform.GetChild(4).gameObject.SetActive(north);

        // East
        for(int i = 2; i < 4; i++)
        {
            ts[1].transform.GetChild(i).gameObject.SetActive(east);
        }
        for(int j = 4; j < 7; j++)
        {
            bs[1].transform.GetChild(j).gameObject.SetActive(east);
        }

        // South
        ts[2].transform.GetChild(4).gameObject.SetActive(south);
        bs[2].transform.GetChild(4).gameObject.SetActive(south);

        // West
        for (int i = 2; i < 4; i++)
        {
            ts[3].transform.GetChild(i).gameObject.SetActive(west);
        }
        for (int j = 4; j < 7; j++)
        {
            bs[3].transform.GetChild(j).gameObject.SetActive(west);
        }
    }

    private void BundleEntryExitGates(out Tuple<GameObject, GameObject>[] bundle)
    {
        Tuple<GameObject, GameObject>[] b = 
        {
            Tuple.Create(northEntryGate, northExitGate),
            Tuple.Create(eastEntryGate, eastExitGate),
            Tuple.Create(southEntryGate, southExitGate),
            Tuple.Create(westEntryGate, westExitGate)
        };
        bundle = b;
    }
}
