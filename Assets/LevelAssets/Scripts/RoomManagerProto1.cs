using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerProto1 : MonoBehaviour
{
    [SerializeField] GameObject roomOrigin;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] LevelMap levelMap;
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

        levelMap.currentRoom.HasVisited = true;
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
                bool condition = levelMap.RoomTransition(gateScripts[i], ref inTransition);

                if (condition)
                {
                    Initiate.Fade("Game", Color.black, 3.0f);
                }
            }
        }
    }
}
