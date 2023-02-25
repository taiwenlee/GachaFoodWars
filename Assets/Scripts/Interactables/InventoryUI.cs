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
        equipments.onEquipmentChangedCallBack += UpdateGearUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        gSlots = gearsParent.GetComponentsInChildren<GearSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetButtonDown("Inventory")) || (Input.GetKeyDown(KeyCode.Tab)))
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
                //Debug.Log("clearing");
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateGearUI() {
        // Checks our array for items to add in the UI
        //Debug.Log("We are in UpdateGearUI");
        for (int i = 0; i < equipments.currentEquipment.Length; i++)
        {
            Debug.Log("Loop: " + i);
            Debug.Log("(equipments.currentEquipment[" + i + "]: " + (equipments.currentEquipment[i]));
            if (equipments.currentEquipment[i] == null && gSlots[i] != null)
            {
                Debug.Log("Hello");
                gSlots[i].ClearSlot();
            }
            else
            {
                Debug.Log("Adding to GearUI");
                gSlots[i].AddItem(equipments.currentEquipment[i]);
                //break;
            }
        }
    }
}
