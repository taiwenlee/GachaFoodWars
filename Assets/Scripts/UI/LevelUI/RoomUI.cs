using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
    [Tooltip("TMPro UI element that will display the score")]
    [SerializeField] TextMeshProUGUI levelDisplay;
    [SerializeField] LevelMap levelMap;
    
    public List<Gate> roomExitGates;

    private void Awake()
    {
        roomExitGates = new List<Gate>();
    }

    private void Start()
    {
        levelDisplay.SetText($"Room {levelMap.currentVertex.x} - {levelMap.currentVertex.y}");

    }
}
