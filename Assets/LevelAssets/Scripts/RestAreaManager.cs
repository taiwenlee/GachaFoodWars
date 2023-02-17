using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestAreaManager : MonoBehaviour
{
    [SerializeField] Gate exitGate;
    [SerializeField] int numberOfLevels;
    [SerializeField] int numberOfRooms;
    [SerializeField] Gacha gacha;
    [SerializeField] GameObject gachaUI;
    [SerializeField] TextMeshProUGUI weaponObtainedUI;

    public LevelStructureProto levelStructure;
    public PlayerLevelProgression plp;

    private void Start()
    {
        gacha.GachaUI = gachaUI;
        gacha.WeaponObtainedUI = weaponObtainedUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (exitGate.triggered)
        {
            Initiate.Fade(levelStructure.combatAreaName, Color.black, 3.0f);
        }
    }
}
