using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    /*    public int armorModifier;
        public int damageModifier;*/
    public int damageStat;
    public WeaponType GSlots;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        //RemoveFromInventory();
    }
}
public enum WeaponType { Sword, Spear, Range};