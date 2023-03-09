using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ModifierGachaAnimator : MonoBehaviour
{
    public AudioSource GachaSFX;
    private ModifierGachaMachineController controller;
    private ModifierGacha gachaUI;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    public Animator animator;
    public bool animationPlaying = false;

    void Start()
    {
        
        GameObject CircleCollider = GameObject.Find("ModifierInteractableCircle");
        controller = CircleCollider.GetComponent<ModifierGachaMachineController>();

        GameObject gacha = GameObject.Find("ModifierGacha");
        gachaUI = gacha.GetComponent<ModifierGacha>();

        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (controller.canGacha == true && controller.isInRange)   // check if player is in range of Gacha machine
        {
            if (Input.GetKeyDown(interactKey))
            {
                GachaSFX.Play();
                StartCoroutine(WaitandGachaCoroutine());
            }
        }
    }
    
    IEnumerator WaitandGachaCoroutine()    // wait for animation to finish before starting gacha event
    {
        animator.SetTrigger("StartGacha");
        animationPlaying = true;
        Debug.Log("animation played");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Waited for 1 second!");
        animationPlaying = false;
        interactAction.Invoke();    // start gacha event
    }
}
