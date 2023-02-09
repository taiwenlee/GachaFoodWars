using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestAreaManager : MonoBehaviour
{
    [SerializeField] Gate exitGate;
    [SerializeField] int numberOfLevels;
    [SerializeField] int numberOfRooms;

    public LevelStructure levelStructure;
    public PlayerLevelProgression plp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (exitGate.triggered)
        {
            BeginGenerateLevels();
        }
    }

    private void BeginGenerateLevels()
    {
        Initiate.Fade(levelStructure.combatAreaName, Color.black, 3.0f);
    }
}
