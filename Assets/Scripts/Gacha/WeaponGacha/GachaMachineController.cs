using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GachaMachineController : MonoBehaviour
{
    private GachaAnimator GAnimator;
    private Gacha gachaUI;
    public bool canGacha = true;
    public float cooldownTime = 1.0f;
    public Inventory inventory;

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

    // IEnumerator CooldownCoroutine()
    // {
    //     yield return new WaitForSeconds(cooldownTime);
    // }
}
