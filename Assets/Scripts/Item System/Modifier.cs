using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Inventory/Modifier")]
public class Modifier : Item
{
    public ModifierType mType;
    public Grade grade;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }
    public enum ModifierType { Damage, Hitbox, AttackSpeed, Fire, Ice, Electric, Knockback, MultiHit }
}
