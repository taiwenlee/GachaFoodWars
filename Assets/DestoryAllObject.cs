using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAllObject : MonoBehaviour
{
    public Gate gate;
    public GameObject Player;
    public GameObject Manager;
    public GameObject HealthSystem;
    private bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gate.triggered && isTriggered == false)
        {
            isTriggered = true;
            Destroy(Player);
            Destroy(Manager);
            Destroy(HealthSystem);
            Debug.Log("Destroying all objects");
        }

    }
}
