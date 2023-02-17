using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    void Awake()
    {
        instance = this;
    }

    Equipment[] currentEquipment;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipment;
    private void Start()
    {
        currentEquipment = new Equipment[2];
    }

    public void Equip (Equipment newItem)
    {
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("Checking for available slot");
            if(currentEquipment[i] == null)
            {
                Debug.Log("Theres a slot");
                currentEquipment[i] = newItem;
                newItem.RemoveFromInventory();
                break;
            }
            else
            {
                Debug.Log("Gearslot is full");
            }
        }
    }
    public void Unequip (int slotIndex)
    {
        //if(current)
    }
}
