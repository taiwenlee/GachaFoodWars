using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GachaAnimator : MonoBehaviour
{
    private bool isInRange;
    public KeyCode interactKey;
    //public UnityEvent interactAction;

    //private float Cooldown = 1f;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isInRange)   // check if player is in range of Gacha machine
        {
            if (Input.GetKeyDown(interactKey))
            {
                animator.SetTrigger("Gacha");
                //wait for animation to finish  //
                // for (int i = 0; i < Cooldown; i++)
                // {
                //     Time.timeScale = 0f;
                // }
                // Time.timeScale = 1f;
                //interactAction.Invoke();    // start event
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
