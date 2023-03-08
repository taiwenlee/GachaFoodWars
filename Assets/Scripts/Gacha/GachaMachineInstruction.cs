using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GachaMachineInstruction : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    public GameObject InstructionUI;

    void Start()
    {

    }

    public void Update()
    {
        if (isInRange)   // check if player is in range of Gacha machine
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();    // start event
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            InstructionUI.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            InstructionUI.SetActive(false);
        }
    }
}
