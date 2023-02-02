using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerExitGate;
    [SerializeField] string exitLevelName;

    Gate gate;

    private void Awake()
    {
        if (string.Compare(exitLevelName, "") == 0)
        {
            Debug.LogError(
                    "Unable to initiate next scene. Possible issues:\n" +
                    "\t- Next scene's string name not defined in Level Manager inspector window...\n" +
                    "\t- Next scene was not added to build settings (File -> Build Settings -> Scenes in Build)..."
            );
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gate = playerExitGate.GetComponent<Gate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gate.triggered)
        {
            Initiate.Fade(exitLevelName, Color.black, 3.0f);
        }
    }
}
