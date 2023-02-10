using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManangerProto : MonoBehaviour
{
    [SerializeField] Transform roomOrigin;
    [SerializeField] EnemySpawner eSpawner;

    private int levelInd;
    private int roomInd;
    private GameObject exitGate;
    private Gate gateScript;
    private string exitSceneName;

    public GameObject player;
    public RoomConstructor rc;
    public LevelStructureProto lsp;
    public PlayerLevelProgression plp;

    // Start is called before the first frame update
    void Awake()
    {
        levelInd = plp.GetLevelIndex();
        roomInd = plp.GetRoomIndex(levelInd);
        bool valid = lsp.GetRoomVector(out Vector4 roomVector, levelInd, roomInd);
        if (!valid)
        {
            Debug.LogError(
                "** Invalid Room Vector ** " +
                "Integer values must be from -1 to 1. " +
                "Must have only one (1) coordinate and " +
                "no more than 3 (-1) coordinates or less than 1 (-1) coordinate"
            );
            return;
        }

        rc.BuildRoom(out player, roomOrigin, roomVector);
        exitGate = GameObject.FindWithTag("Gate_Exit");
        gateScript = exitGate.GetComponent<Gate>();
    }

    private void Start()
    {
        exitSceneName = roomInd + 1 < lsp.GetMatrix()[levelInd].Count ?
            lsp.combatAreaName :
            lsp.restAreaName;
        plp.IncrementRoomIndex(levelInd);

        eSpawner.Player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (gateScript.triggered)
        {
            if (exitSceneName.CompareTo(lsp.restAreaName) == 0)
            {
                plp.CompleteCurrentLevel(levelInd);
            }

            Initiate.Fade(exitSceneName, Color.black, 3.0f);
        }
    }
}
