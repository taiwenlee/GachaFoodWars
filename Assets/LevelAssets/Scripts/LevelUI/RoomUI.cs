using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
    [Tooltip("TMPro UI element that will display the score")]
    [SerializeField] TextMeshProUGUI levelDisplay;
    [SerializeField] PlayerLevelProgression plp;
    
    public Gate roomExitGate;

    private void Start()
    {
        roomExitGate = GameObject.FindWithTag("Gate_Exit").GetComponent<Gate>();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (!roomExitGate.triggered) 
        {
            levelDisplay.SetText($"Level {plp.GetLevelIndex() + 1} Room {plp.GetRoomIndex(plp.GetLevelIndex())}");
        }
    }

}
