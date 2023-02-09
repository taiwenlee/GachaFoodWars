using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Level1RoomPrefabs;
    [SerializeField] GameObject roomOrigin;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] EnemySpawner eSpawner;

    private int levelInd;
    private string exitSceneName;
    //private GameObject entryGate;
    private GameObject exitGate;
    private Gate gateScript;
    public PlayerLevelProgression plp;
    public LevelStructure levelStructure;

    void Awake()
    {
        levelInd = plp.GetLevelIndex();
        int roomInd = plp.GetRoomIndex(levelInd);

        Debug.Log("Level " + (levelInd + 1) + " - Room " + (roomInd + 1));

        Instantiate(levelStructure.matrixRooms[levelInd][roomInd], roomOrigin.transform);
        //entryGate = GameObject.FindWithTag("Gate_Enter");
        exitGate = GameObject.FindWithTag("Gate_Exit");
        gateScript = exitGate.GetComponent<Gate>();

        exitSceneName = roomInd + 1 < levelStructure.matrixRooms[levelInd].Count ?
            levelStructure.combatAreaName :
            levelStructure.restAreaName;
        plp.IncrementRoomIndex(levelInd);

        Vector3 pos = gateScript.GetEntryWorldPosition();
        GameObject player = Instantiate(playerPrefab, pos, Quaternion.identity);
        eSpawner.Player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (gateScript.triggered)
        {
            if (exitSceneName.CompareTo(levelStructure.restAreaName) == 0)
            {
                plp.CompleteCurrentLevel(levelInd);
            }

            Initiate.Fade(exitSceneName, Color.black, 3.0f);
        }
    }
}
