using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite Icon = null;
    public bool isDefaultItem = false;
    public string description;
    public Grade grade;

    public virtual void Use()
    {
    }

    public virtual Item Clone()
    {
        Item newItem = ScriptableObject.CreateInstance<Item>();
        //item properties
        newItem.name = this.name;
        newItem.Icon = this.Icon;
        newItem.isDefaultItem = this.isDefaultItem;
        newItem.grade = this.grade;
        return newItem;
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
    public enum Grade { Common, Rare, Epic, Legendary };
}
