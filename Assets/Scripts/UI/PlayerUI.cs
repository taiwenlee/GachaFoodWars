using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;

    public GameObject restartMenu;
    public GameObject blackscreen;
    public BlackCircleScript blackscreenScript;
    public PlayerStats playerStats;
    //private bool playerDead = false;
    //private bool showDeathUI = false;
    private float restartMenuTimer = 0.0f;
    private float blackScreenTimer = 0.0f;
    public int health = 4;
    GameObject heart;
    
    //[SerializeField] private Image[] numHearts;

    public void DrawHearts(int hearts, int maxHearts) {
        foreach(Transform child in transform) { //destroy so no hearts on top of each other
            Destroy(child.gameObject);
        }

        for(int i = 0; i < maxHearts; i++) {
            if(i  <= hearts) {
                heart = Instantiate(heartPrefab, transform.position, Quaternion.identity); //spawn hearts
                heart.transform.SetParent(transform);
            }else {
                Destroy(heart); //destroy if damaged
            }
        }
    }

    private void Start(){
        UpdateHealth();
    }

    private void Update()
    {
        restartMenuTimer += Time.deltaTime;
        blackScreenTimer += Time.deltaTime;
        health = playerStats.playerHealthData.GetPlayerHealth();
        if (health <= 0) {
            blackscreen.SetActive(true);
            blackscreenScript.OpenBlackScreen();
            if(blackScreenTimer >= 3.0f) { //dont look at this mess
                blackscreenScript.CloseBlackScreen();
            }
            if(restartMenuTimer >= 3.0f) { //delays opening restart menu by 3 second
                restartMenu.SetActive(true);
                restartMenuTimer = 0.0f; //resets timer
            }  
        }else{
            restartMenu.SetActive(false);
            blackscreen.SetActive(false);
            restartMenuTimer = 0.0f; //resets timer
            blackScreenTimer = 0.0f;
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
        // for(int i = 0; i < numHearts.Length; i++) {
        //     if(i < health) {
        //         numHearts[i].color = Color.red;
        //     } else {
        //         numHearts[i].color = Color.black;
        //     }
        // }
    }

    public void SetHealth(int currentHealth)
    {
        health = currentHealth;
        UpdateHealth();
    } 

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
 
     // Code to execute after the delay
    }
}
