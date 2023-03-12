using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Gacha : MonoBehaviour
{
    public GameObject GachaUI;
    public GameObject Broke;
    public TMP_Text WeaponObtainedUI;
    public AudioSource GachaSFX;
    public Inventory inventory;
    public KeyCode interactKey;
    public GameObject GachaInstructionUI;

    public Animator animator;
    public bool animationPlaying = false;

    public float cooldownTime = 1.0f;
    public int gachaCost = 2;

    public bool isInRange = false;
    public bool isGachaing = false;

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
        if (Broke == null)
        {
            Broke = GameObject.FindGameObjectWithTag("Broke");
        }
        if (GachaInstructionUI == null)
        {
            GachaInstructionUI = GameObject.FindGameObjectWithTag("GachaInstructionUI");
        }
        GachaInstructionUI.GetComponent<Canvas>().enabled = false;
        WeaponObtainedUI = GachaUI.GetComponentInChildren<TMP_Text>();
        Broke.SetActive(false);
        // disable UI
        GachaUI.GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInRange)   // check if player is in range of Gacha machine
        {
            if (canGacha())
            {
                Broke.SetActive(false);
                GachaSFX.Play();
                isGachaing = true;
                StartCoroutine(WaitandGachaCoroutine());
                isGachaing = false;
            }
            if (inventory.currency < gachaCost)
            {
                Broke.SetActive(true);
                //BrokeCoroutine();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            isInRange = true;
            GachaInstructionUI.GetComponent<Canvas>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exist range");
            isInRange = false;
            GachaInstructionUI.GetComponent<Canvas>().enabled = false;
        }
    }

    private bool canGacha()
    {
        if (animationPlaying == false && isGachaing == false && GachaUI.GetComponent<Canvas>().enabled == false)
        {
            if (inventory.RemoveCurrency(gachaCost))
            {
                return true;
            }
            else
            {
                return false;
            }
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
        yield return new WaitForSeconds(1.0f);
        animationPlaying = false;
        startGacha();
    }

    // IEnumerator BrokeCoroutine()
    // {
    //     Debug.Log("Broke");
    //     Broke.SetActive(true);
    //     yield return new WaitForSeconds(1.0f);
    //     Broke.SetActive(false);
    // }

    private void startGacha()
    {
        // get random item from loot table
        Item item = GetRandomItem();
        // add item to inventory
        inventory.Add(item);
        // display item obtained
        WeaponObtainedUI.text = "You obtained: " + item.grade + " " + item.name + ".";
        // temp to track item grades
        Debug.Log(item.grade + " " + item.name + " obtained.");
        // enable UI
        GachaUI.GetComponent<Canvas>().enabled = true;
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
