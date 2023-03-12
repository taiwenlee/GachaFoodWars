using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaInstruction : MonoBehaviour
{
    private Gacha gacha;
    public GameObject GachaInstructionUI;

    void Start()
    {
        if (GachaInstructionUI == null)
        {
            GachaInstructionUI = GameObject.FindGameObjectWithTag("GachaInstructionUI");
        }
        GachaInstructionUI.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gacha.isInRange == true)
        {
            Debug.Log("Gacha instruction");
            GachaInstructionUI.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            GachaInstructionUI.GetComponent<Canvas>().enabled = false;
        }
    }
}
