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
    public AudioSource GachaSFX;
    public Inventory inventory;
    public KeyCode interactKey;

    public Animator animator;
    public bool animationPlaying = false;

    public float cooldownTime = 1.0f;
    public int gachaCost = 2;

    public bool isInRange = false;

    [SerializeField]
    // list of items in the gacha
    public List<Item> Items;

    [SerializeField]
    // probability by rarity (Common, Rare, Epic, Legendary)
    public float[] table = { .5f, .3f, .15f, .05f };

    private void Start()
    {
        // find objects
        if (inventory == null)
        {
            inventory = Inventory.instance;
        }
        if (GachaUI == null)
        {
            GachaUI = GameObject.FindGameObjectWithTag("GachaUI");
        }
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (GachaSFX == null)
        {
            GachaSFX = GetComponent<AudioSource>();
        }
        WeaponObtainedUI = GachaUI.GetComponentInChildren<TMP_Text>();
        // disable UI
        GachaUI.SetActive(false);
        // calculate total weight of loot table
        // foreach(var item in table)
        // {
        //     totalWeight += item;
        // }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInRange)   // check if player is in range of Gacha machine
        {
            if (canGacha())
            {
                GachaSFX.Play();
                StartCoroutine(WaitandGachaCoroutine());
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private bool canGacha()
    {
        if (inventory.RemoveCurrency(gachaCost) && animationPlaying == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator WaitandGachaCoroutine()    // wait for animation to finish before starting gacha event
    {
        animator.SetTrigger("StartGacha");
        animationPlaying = true;
        Debug.Log("animation played");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Waited for 1 second!");
        animationPlaying = false;
        startGacha();
    }

    private void startGacha()
    {
        // get random item from loot table
        Item item = GetRandomItem();
        // add item to inventory
        inventory.Add(item);
        // display item obtained
        WeaponObtainedUI.text = "You obtained: " + item.grade + " " + item.name + ".";
        GachaUI.SetActive(true);
    }

    private Item GetRandomItem()
    {
        // get random item from list of items
        Item temp = Items[Random.Range(0, Items.Count)];
        Item item = temp.Clone();

        // get rarity of item
        float rarity = 0;
        float randomRarity = Random.value;
        for (int i = 0; i < table.Length; i++)
        {
            rarity += table[i];
            if (randomRarity <= rarity)
            {
                item.grade = (Item.Grade)i;
                break;
            }
        }
        return item;
    }

}
