using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    /*    public int armorModifier;
        public int damageModifier;*/
    public int damageStat; // base damage
    public float attackSpeed; // base attack speed

    public float damageMultiplier; // damage multiplier
    public float attackSpeedMultiplier; // attack speed multiplier
    public WeaponType gSlots;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }
}
public enum WeaponType { Sword, Spear, Range };