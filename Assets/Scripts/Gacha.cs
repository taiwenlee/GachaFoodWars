using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> weapons;
    // total weight of each rarity
    [SerializeField]
    public int[] table = {500, 300, 160, 40};

    public int totalWeight;
    public int randomNumber;

    private void Start()
    {
        // calculate total weight of loot table
        foreach(var item in table)
        {
            totalWeight += item;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
            // generate random number
            randomNumber = Random.Range(0, totalWeight);
            // compare random number to loot table weight
            for(int i = 0; i < table.Length; i++)
            {
                // compare random number to the [i] weight in loot table, if smaller give [i] item
                if(randomNumber <= table[i])
                {
                    weapons[i].SetActive(true);
                    Debug.Log("Award: " + table[i]);
                    return;
                }else
                {
                    // if random number is bigger than previous weight, subtract it with weight and compare the product to next weight in table
                    randomNumber -= table[i];
                }
            }
        }
    }

}
