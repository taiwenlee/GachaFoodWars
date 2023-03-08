using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Gacha : MonoBehaviour
{
    public GameObject GachaUI;
    public TMP_Text WeaponObtainedUI;

    [SerializeField]
    //public List<GameObject> weapons;                // list of all weapons
    public List<Equipment> Items;

    [SerializeField]
    public int[] table = {500, 300, 160, 40};       // total weight of each rarity
    //public string[] tableName = {"Sword", "Axe", "Bow", "Gun"};     // weapon name of the according rarity above

    public int totalWeight;
    public int randomNumber;

    private void Start()
    {
        GachaUI.SetActive(false);
        // calculate total weight of loot table
        foreach(var item in table)
        {
            totalWeight += item;
        }
    }

    public void StartGacha()
    {
        print("Started Gacha");
        // generate random number
        randomNumber = Random.Range(0, totalWeight);
        // compare random number to loot table weight
        for(int i = 0; i < table.Length; i++)
        {
            // compare random number to the [i] weight in loot table, if smaller give [i] item
            if(randomNumber <= table[i])
            {
                //weapons[i].SetActive(true);
                Inventory.instance.Add(Items[i]);
                if (table[i] == 500)
                {
                    WeaponObtainedUI.text = "Common" + " " + Items[i].name;
                    WeaponObtainedUI.color = Color.white;
                    Debug.Log("ObtainCommon");
                }
                if (table[i] == 300)
                {
                    WeaponObtainedUI.text = "Rare" + " " + Items[i].name;
                    WeaponObtainedUI.color = Color.blue;
                    Debug.Log("ObtainRare");
                }
                if (table[i] == 160)
                {
                    WeaponObtainedUI.text = "Epic" + " " + Items[i].name;
                    WeaponObtainedUI.color = Color.magenta;
                    Debug.Log("ObtainEpic");
                }
                if (table[i] == 40)
                {
                    WeaponObtainedUI.text = "Legendary" + " " + Items[i].name;
                    WeaponObtainedUI.color = Color.red;
                }
                Debug.Log("Award: " + table[i] + Items[i].name);
                GachaUI.SetActive(true);
                return;
            }else
            {
                // if random number is bigger than previous weight, subtract it with weight and compare the product to next weight in table
                randomNumber -= table[i];
            }
        }
    }
}
