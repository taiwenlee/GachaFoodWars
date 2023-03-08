using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currencyText;
    public Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        // set currency text to the text component
        currencyText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        // set inventory to the instance of the inventory
        if (Inventory.instance != null)
        {
            inventory = Inventory.instance;
            inventory.onCurrencyChangedCallBack += UpdateCurrencyUI;
        }

        // set currency text to the currency value
        currencyText.text = inventory.currency.ToString();
    }

    void UpdateCurrencyUI()
    {
        currencyText.text = inventory.currency.ToString();
    }
}
