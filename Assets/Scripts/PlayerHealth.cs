using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;
    
    [SerializeField] private Image[] numHearts;

    private void Start(){
        UpdateHealth();
    }

    public void UpdateHealth() { // alive or dead hearts
        if(playerHealth <= 0) {
            //restart game code here
            print("Player Dead");
        }
        for(int i = 0; i < numHearts.Length; i++) {
            if(i < playerHealth) {
                numHearts[i].color = Color.red;
            } else {
                numHearts[i].color = Color.black;
            }
        }
    }
}
