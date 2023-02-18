using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform gearsParent;
    public GameObject inventoryUI;
    public GameObject gearUI;

    Inventory inventory;
    EquipmentManager equipments;

    InventorySlot[] slots;
    GearSlot[] gSlots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateInventoryUI;

        equipments = EquipmentManager.instance;


        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            gearUI.SetActive(!gearUI.activeSelf);
        }
    }
    
    void UpdateInventoryUI()
    {
        // Checks our array for items to add in the UI
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                //Debug.Log("Adding");
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
               // Debug.Log("clearing");
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateGearUI() {
        // Checks our array for items to add in the UI
        for (int i = 0; i < gSlots.Length; i++)
        {
            if (equipments == null)
            {
                gSlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                gSlots[i].ClearSlot();
            }
        }
    }
}
