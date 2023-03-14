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
    public WeaponType gSlots;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }

    public override Item Clone()
    {
        Equipment newEquipment = ScriptableObject.CreateInstance<Equipment>();
        //item properties
        newEquipment.name = this.name;
        newEquipment.Icon = this.Icon;
        newEquipment.isDefaultItem = this.isDefaultItem;
        newEquipment.grade = this.grade;
        //equipment properties
        newEquipment.damageStat = this.damageStat;
        newEquipment.attackSpeed = this.attackSpeed;
        newEquipment.gSlots = this.gSlots;
        return newEquipment;
    }
}
public enum WeaponType { Sword, Spear, Range };