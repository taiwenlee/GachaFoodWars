using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Inventory/Modifier")]
public class Modifier : Item
{
    public ModifierType mType;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }

    public override Item Clone()
    {
        Modifier newModifier = ScriptableObject.CreateInstance<Modifier>();
        //item properties
        newModifier.name = this.name;
        newModifier.Icon = this.Icon;
        newModifier.isDefaultItem = this.isDefaultItem;
        newModifier.grade = this.grade;
        //modifier properties
        newModifier.mType = this.mType;
        return newModifier;
    }
    public enum ModifierType { Damage, Hitbox, AttackSpeed, Fire, Ice, Electric, Knockback, MultiHit }
}
