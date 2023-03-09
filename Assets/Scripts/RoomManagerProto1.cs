using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerProto1 : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject roomOrigin;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] LevelMap levelMap;
    [SerializeField] RoomConstructor rc;

    private bool inTransition;
    private bool groundActivated;
    private EnemySpawner es;
    private Ground groundScript;
    private List<Gate> gateScripts;


    private void Awake()
    {
        inTransition = false;
        groundActivated = false;
        gateScripts = new();

        /*player = GameObject.FindWithTag("Player");
        if (player == null )
        {
            Debug.LogError("FIND WITH TAG UNSUCCESSFUL: 'player'");
        }*/
        inventory = GameObject.FindWithTag("Inventory");

        levelMap.currentRoom.HasVisited = true;
        Vector2 roomCoords = levelMap.currentVertex;
        Vector4 layout = levelMap.currentRoomLayout;
        rc.BuildRoom(out GameObject player, roomOrigin.transform, layout);
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate_Exit");
        foreach (GameObject go in gates)
        {
            gateScripts.Add(go.GetComponent<Gate>());
        }
        groundScript = GameObject.FindWithTag("Ground").GetComponent<Ground>();

        inventoryUI = GameObject.FindWithTag("InventoryUI");
        inventoryUI.GetComponent<InventoryUI>().player = player.GetComponent<Player>();

        player.GetComponent<Player>().heartSystem = GameObject.FindWithTag("HealthController").GetComponent<PlayerUI>();

        es = enemySpawner.GetComponent<EnemySpawner>();
        es.Player = player;
        es.BeginLevel(levelMap.spawnerMatrix
            .cols[(int)roomCoords.x]
            .rows[(int)roomCoords.y]
        );
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
