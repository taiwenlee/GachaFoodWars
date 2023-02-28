using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite Icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
public enum Grade { Common, Rare, Epic, Legendary };