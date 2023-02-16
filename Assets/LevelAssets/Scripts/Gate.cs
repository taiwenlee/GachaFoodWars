using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public EntryGate entryPoint;
    public Vector4 terminal; // indicates which side this gate is on
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
    }

    public Vector3 GetEntryWorldPosition()
    {
        return entryPoint.position;
    }
}
