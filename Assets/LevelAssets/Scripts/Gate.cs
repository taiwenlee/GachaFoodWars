using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [ReadOnly]
    public bool triggered;

    private void Start()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
        else
        {
            Debug.LogError("Check if object tag is 'Player'");
        }
    }
}
