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

    // Update is called once per frame
    void Update()
    {
        if (exitGate.triggered)
        {
            Initiate.Fade(levelStructure.combatAreaName, Color.black, 3.0f);
        }
    }
}
