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

    public Item[] currentEquipment;
    public WeaponController wc;
    Inventory inventory;
    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChangedCallBack;
    private void Start()
    {
        inventory = Inventory.instance;
        currentEquipment = new Item[4];
    }

    private void Update()
    {
    }
    public void Equip(Item newItem)
    {
        if (newItem is Equipment && currentEquipment[0] == null)
        {
            currentEquipment[0] = newItem;
            newItem.RemoveFromInventory();
            if (onEquipmentChangedCallBack != null)
            {
                onEquipmentChangedCallBack.Invoke();
            }
        }
        else if (newItem is Modifier)
        {
            for (int i = 1; i < currentEquipment.Length; i++)
            {
                if (currentEquipment[i] == null)
                {
                    currentEquipment[i] = newItem;
                    newItem.RemoveFromInventory();
                    if (onEquipmentChangedCallBack != null)
                    {
                        //Debug.Log("Invoking in equip");
                        onEquipmentChangedCallBack.Invoke();

                    }
                    break;
                }
            }
        }
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Item oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            wc.WeaponSelector();
            wc.setModifiers();
        }

        if (onEquipmentChangedCallBack != null)
        {
            onEquipmentChangedCallBack.Invoke();
        }
    }
}
