using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GachaMachineController : MonoBehaviour
{
    public bool isInRange = false;

    private GachaAnimator GAnimator;
    private Gacha gachaUI;
    public bool canGacha = true;
    public float cooldownTime = 1.0f;

    void Start()
    {
        GAnimator = GameObject.Find("GachaSprite").GetComponent<GachaAnimator>();
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
