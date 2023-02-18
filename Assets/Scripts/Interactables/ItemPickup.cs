using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }
    void PickUp()
    {
        /*        if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Pickup pressed");
                    Debug.Log("e pressed");
                    Debug.Log("Picking up " + gameObject);
                    //Destroy(gameObject);
                }*/

        Debug.Log("Picking up " + item.name);
        bool wasPickUp = Inventory.instance.Add(item);

        if(wasPickUp)
        {
           Destroy(gameObject);
        }
    }

}
