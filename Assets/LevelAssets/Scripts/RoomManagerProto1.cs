using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerProto1 : MonoBehaviour
{
    [SerializeField] Transform roomOrigin;
    [SerializeField] LevelMap levelMap;
    [SerializeField] LevelConstructor lc;
    [SerializeField] RoomConstructor rc;

    private bool inTransition;
    private List<Gate> gateScripts;

    private void Awake()
    {
        inTransition = false;
        gateScripts = new();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector4 layout = levelMap.currentRoomLayout;
        rc.BuildRoom(out GameObject player, roomOrigin, layout);
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate_Exit");
        foreach (GameObject go in gates)
        {
            gateScripts.Add(go.GetComponent<Gate>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gateScripts.Count; i++)
        {
            if (gateScripts[i].triggered)
            {
                RoomTransition(gateScripts[i]);
                Initiate.Fade("Game", Color.black, 3.0f);
            }
        }
    }

    private void RoomTransition(Gate gate)
    {
        if (inTransition)
        {
            return;
        }

        RoomBlueprint rb;
        int exit;

        if (gate.terminal.x == 1)
        {
            levelMap.currentVertex.y -= 1;
            exit = 0;
            rb = levelMap.currentRoom.North;
        }
        else if (gate.terminal.y == 1)
        {
            levelMap.currentVertex.x += 1;
            exit = 1;
            rb = levelMap.currentRoom.East;
        }
        else if (gate.terminal.z == 1)
        {
            levelMap.currentVertex.y += 1;
            exit = 2;
            rb = levelMap.currentRoom.South;
        }
        else
        {
            levelMap.currentVertex.x -= 1;
            exit = 3;
            rb = levelMap.currentRoom.West;
        }

        levelMap.currentRoom = rb;
        levelMap.currentRoomLayout = levelMap.currentRoom.GenerateRoomEntryPoint(exit);
        inTransition = true;
    }
}
