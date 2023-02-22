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

    public Equipment[] currentEquipment;
    public Equipment equipmentSelected;
    public WeaponController wc;
    Inventory inventory;
    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChangedCallBack;
    private void Start()
    {
        inventory = Inventory.instance;
        currentEquipment = new Equipment[2];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipmentSelected = currentEquipment[0];
            wc.isSelected = false;
            //Debug.Log("1 selected: " + equipmentSelected);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            equipmentSelected = currentEquipment[1];
            wc.isSelected = false;
            //Debug.Log("2 selected: " + equipmentSelected);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) && equipmentSelected != null)
            Debug.Log("Current Equipement: " + equipmentSelected.name + ", " + equipmentSelected.gSlots);
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
        if (onEquipmentChangedCallBack != null)
        {
            //Debug.Log("Invoking in equip");
            onEquipmentChangedCallBack.Invoke();

        }
    }
    public void Unequip (int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
        }

        if (onEquipmentChangedCallBack != null)
        {
            Debug.Log("Invoking in unequip");
            onEquipmentChangedCallBack.Invoke();

        }
    }
}
