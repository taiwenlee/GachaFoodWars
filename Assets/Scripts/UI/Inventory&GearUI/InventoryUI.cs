using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform gearsParent;
    public GameObject inventoryUI;
    public GameObject gearUI;
    public Player player;
    Inventory inventory;
    EquipmentManager equipments;

    InventorySlot[] slots;
    GearSlot[] gSlots;

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI.SetActive(false);
        gearUI.SetActive(false);
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

        // set inventory to the instance of the inventory
        if (Inventory.instance != null)
        {
            inventory = Inventory.instance;
            inventory.onItemChangedCallBack += UpdateInventoryUI;
        }

        equipments = EquipmentManager.instance;
        equipments.onEquipmentChangedCallBack += UpdateGearUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        gSlots = gearsParent.GetComponentsInChildren<GearSlot>();

        // update the UI
        UpdateInventoryUI();
        UpdateGearUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerDead == false)
        {
            if ((Input.GetButtonDown("Inventory")) || (Input.GetKeyDown(KeyCode.Tab)))
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                gearUI.SetActive(!gearUI.activeSelf);
            }
        }
    }

    void UpdateInventoryUI()
    {
        // Checks our array for items to add in the UI
        for (int i = 0; i < slots.Length; i++)
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

    void UpdateGearUI()
    {
        // Checks our array for items to add in the UI
        for (int i = 0; i < equipments.currentEquipment.Length; i++)
        {
            if (equipments.currentEquipment[i] != null)
            {
                //Debug.Log("Adding");
                gSlots[i].AddItem(equipments.currentEquipment[i]);
            }
            else
            {
                //Debug.Log("clearing");
                gSlots[i].ClearSlot();
            }
        }
    }
}
