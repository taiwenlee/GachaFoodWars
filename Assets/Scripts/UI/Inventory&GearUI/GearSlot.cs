using UnityEngine;
using UnityEngine.UI;


public class GearSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public int gearIndex;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
        WeaponController.instance.setModifiers();
        WeaponController.instance.WeaponSelector();
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        EquipmentManager.instance.Unequip(gearIndex);
    }
}
