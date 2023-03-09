using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Allows us to update the ui when knowing when inventory is changed
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public delegate void OnCurrencyChanged();
    public OnCurrencyChanged onCurrencyChangedCallBack;

    public int space = 30;
    public static Inventory instance;
    public int currency = 0;


    // singleton
    private void Awake()
    {
        //allows us to always access it
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
        }
        instance = this;
    }

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            //Debug.Log("Not enough room");
            return false;
        }
        items.Add(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();

        }
        return true;
    }

    public void Remove(Item item)
    {
        // Debug.Log("Removing");
        //Debug.Log("Removing " + item.itemObject);
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    public void AddOnAwake()
    {
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
        if (onCurrencyChangedCallBack != null)
        {
            onCurrencyChangedCallBack.Invoke();
        }
    }

    public void AddCurrency(int value)
    {
        currency += value;
        if (onCurrencyChangedCallBack != null)
        {
            onCurrencyChangedCallBack.Invoke();
        }
    }

    // returns true if currency was removed
    public bool RemoveCurrency(int value)
    {
        if (currency >= value)
        {
            currency -= value;
            if (onCurrencyChangedCallBack != null)
            {
                onCurrencyChangedCallBack.Invoke();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //call currency UI update when currency is changed
    public void OnValidate()
    {
        if (onCurrencyChangedCallBack != null)
        {
            onCurrencyChangedCallBack.Invoke();
        }
    }
}
