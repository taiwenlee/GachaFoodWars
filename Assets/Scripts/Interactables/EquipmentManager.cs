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
        currentEquipment = new Equipment[1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || equipmentSelected == null)
        {
            equipmentSelected = currentEquipment[0];
            wc.isSelected = false;
            //Debug.Log("1 selected: " + equipmentSelected);
        }
    }
    public void Equip (Equipment newItem)
    {
        if (currentEquipment[0] == null)
        {
            currentEquipment[0] = newItem;
            newItem.RemoveFromInventory();
        }
        else
        {
            Debug.Log("Gearslot is full");
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
            equipmentSelected = null;
        }

        if (onEquipmentChangedCallBack != null)
        {
            onEquipmentChangedCallBack.Invoke();
        }
    }
}
