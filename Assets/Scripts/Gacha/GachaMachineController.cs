using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GachaMachineController : MonoBehaviour
{
    public bool isInRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    private GachaAnimator GAnimator;
    private Gacha gachaUI;
    public bool canGacha = true;
    public float cooldownTime = 1.0f;

    void Start()
    {
        // GameObject animObject = GameObject.Find("GachaSprite");
        // GAnimator = animObject.GetComponentInParent<GachaAnimator>();
        GAnimator = GameObject.Find("GachaSprite").GetComponent<GachaAnimator>();

        // GameObject gacha = GameObject.Find("Gacha");
        // gachaUI = gacha.GetComponent<Gacha>();
        gachaUI = GameObject.Find("Gacha").GetComponent<Gacha>();
    }

    public void Update()
    {
        if ((gachaUI.GachaUI.activeSelf) || (GAnimator.animationPlaying == true))
        {
            canGacha = false;
        }
        else
        {
            canGacha = true;
        }

        if (isInRange)   // check if player is in range of Gacha machine
        {
            if (canGacha == true && Input.GetKeyDown(interactKey))
            {
                Debug.Log("interact key was pressed");
                interactAction.Invoke();    // start event
                //StartCoroutine(CooldownCoroutine());
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player is in range");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is out of range");
        }
    }

    // IEnumerator CooldownCoroutine()
    // {
    //     yield return new WaitForSeconds(cooldownTime);
    // }
}
