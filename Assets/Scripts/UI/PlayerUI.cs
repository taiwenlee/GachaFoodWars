using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject restartMenu;
    public PlayerStats playerStats;
    //private bool playerDead = false;
    //private bool showDeathUI = false;
    public int health = 4;
    
    [SerializeField] private Image[] numHearts;

    private void Start(){
        UpdateHealth();
    }

    private void Update()
    {
        health = playerStats.playerHealthData.GetPlayerHealth();

        if (health <= 0) {
            restartMenu.SetActive(true);
        }else{
            restartMenu.SetActive(false);
        }
        UpdateHealth();
    }

    private void UpdateHealth() { // alive or dead hearts
        if(health <= 0) {
            //restart game code here
            GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>().enabled = false;
            //restartMenu.SetActive(true);
            //Time.timeScale = 0f;
            //print("Player Dead");
          
                
                //Destroy(gameObject, 1f);
                //Time.timeScale = 0f;
           
        }
        for(int i = 0; i < numHearts.Length; i++) {
            if(i < health) {
                numHearts[i].color = Color.red;
            } else {
                numHearts[i].color = Color.black;
            }
        }
    }

    

    public void SetHealth(int currentHealth)
    {
        health = currentHealth;
        UpdateHealth();
    } 
}
