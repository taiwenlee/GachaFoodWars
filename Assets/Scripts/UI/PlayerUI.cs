using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public int health = 4;
    
    [SerializeField] private Image[] numHearts;

    private void Start(){
        UpdateHealth();
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth() { // alive or dead hearts
        if(health <= 0) {
            //restart game code here
            print("Player Dead");
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
