using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Progression")]

/// Contains static data regarding the player's level progression
/// After the player clears all rooms in a level's room pool,
/// the corresponding boolean for that level is set to true.
/// During next combat, the game will load the next level's rooms.
/// 
/// Right now, the structure is linear

public class PlayerProgression : ScriptableObject
{
    [Header("Base Metrics (do not modify)")]
    [SerializeField] bool gameStartInit = false;
    [SerializeField] bool trainingCompleteInit = false;
    [SerializeField] bool level1CompleteInit = false;
    /*[SerializeField] bool level2CompleteInit = false;
    [SerializeField] bool level3CompleteInit = false;
    [SerializeField] bool level4CompleteInit = false;
    [SerializeField] bool level5CompleteInit = false;*/

    public bool GameStarted { get; set; }

    public bool TrainingComplete { get; set; }

    public bool Level1Complete { get; set; }


    private void OnEnable()
    {
        Debug.Log("Player Progression Enabled");
        GameStarted = gameStartInit;
        TrainingComplete = trainingCompleteInit;
        Level1Complete = level1CompleteInit;

    }

    private void OnDisable()
    {
        Debug.Log("Player Progression Disabled");
        GameStarted = false;
        TrainingComplete = false;
        Level1Complete = false;
    }

}
