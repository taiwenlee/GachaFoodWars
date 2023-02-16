using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerProto1 : MonoBehaviour
{
    [SerializeField] GameObject roomOrigin;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] LevelMap levelMap;
    [SerializeField] LevelConstructor lc;
    [SerializeField] RoomConstructor rc;

    private bool inTransition;
    private bool groundActivated;
    private Ground groundScript;
    private List<Gate> gateScripts;

    private void Awake()
    {
        inTransition = false;
        groundActivated = false;
        gateScripts = new();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector4 layout = levelMap.currentRoomLayout;
        rc.BuildRoom(out GameObject player, roomOrigin.transform, layout);
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate_Exit");
        foreach (GameObject go in gates)
        {
            gateScripts.Add(go.GetComponent<Gate>());
        }

        groundScript = GameObject.FindWithTag("Ground").GetComponent<Ground>();

        enemySpawner.GetComponent<EnemySpawner>().Player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!groundActivated && groundScript.groundTriggered)
        {
            foreach (Gate gate in gateScripts)
            {
                gate.boxCollider.enabled = true;
            }

            groundActivated = true;
        }

        for (int i = 0; i < gateScripts.Count; i++)
        {
            if (gateScripts[i].triggered)
            {
                bool condition = RoomTransition(gateScripts[i]);

                if (condition)
                {
                    Initiate.Fade("Game", Color.black, 3.0f);
                }
            }
        }
    }

    private bool RoomTransition(Gate gate)
    {
        if (inTransition)
        {
            return false;
        }

        RoomBlueprint rb;
        int exit;
        Vector2 current = levelMap.currentVertex;

        if (gate.terminal.x == 1)
        {
            current.y -= 1;
            exit = 0;
            rb = levelMap.currentRoom.North;
        }
        else if (gate.terminal.y == 1)
        {
            current.x += 1;
            exit = 1;
            rb = levelMap.currentRoom.East;
        }
        else if (gate.terminal.z == 1)
        {
            current.y += 1;
            exit = 2;
            rb = levelMap.currentRoom.South;
        }
        else
        {
            current.x -= 1;
            exit = 3;
            rb = levelMap.currentRoom.West;
        }

        if (rb == null)
        {
            return false;
        }

        levelMap.currentVertex = current;
        levelMap.currentRoom = rb;
        levelMap.currentRoomLayout = levelMap.currentRoom.GenerateRoomEntryPoint(exit);
        inTransition = true;
        return true;
    }
}
