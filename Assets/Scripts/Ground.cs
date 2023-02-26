using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public BoxCollider bcoll;
    public bool groundTriggered;

    // Start is called before the first frame update
    void Start()
    {
        bcoll = GetComponent<BoxCollider>();
        groundTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            groundTriggered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
