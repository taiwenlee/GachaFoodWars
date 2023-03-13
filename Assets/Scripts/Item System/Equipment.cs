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
        Debug.Log("Item hover is grade: " + newEquipment.grade);
        newEquipment.description = newEquipment.name + "\nGrade: " + newEquipment.grade + "\nDamage: " + newEquipment.damageStat + (int)newEquipment.grade * 2 + "\nAttack Speed: " + newEquipment.attackSpeed + (int)newEquipment.grade * 0.1f;
        return newEquipment;
    }
}
public enum WeaponType { Sword, Spear, Range };