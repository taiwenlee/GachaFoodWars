using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAllObject : MonoBehaviour
{
    public Gate gate;
    public GameObject Player;
    public GameObject Manager;
    private bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Manager = GameObject.FindWithTag("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        if (gate.triggered && isTriggered == false)
        {
            isTriggered = true;

            Destroy(Player);
            Destroy(Manager);
            Debug.Log("Destroying all objects");
        }

    }
}
