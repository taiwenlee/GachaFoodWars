using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    /*    public int armorModifier;
        public int damageModifier;*/
    public int damageStat;
    public WeaponType gSlots;
    public Grade gradeType;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }
}
public enum WeaponType { Sword, Spear, Range};
public enum Grade { Common, Uncommon, Rare, Epic, Legendary};