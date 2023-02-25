using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public PlayerUI healthControl;
    public PlayerStats playerStats;
    private Animations sprite;
    //private GameObject restartMenu;
    //public GameObject player;

    [Header("Stats")]
    public int health = 4;
    public float damageTimeout = 1f; // prevent too many hits at once. set in seconds
    private bool delayDamage = true;
    private bool playerDead = false;

    //[Header("Damage Visualizer")]
    //public int numFlicker = 9; //num times player flickers
    //public float flickerDuration = .1f;

    public void Start()
    {
        //healthControl = GameObject.FindWithTag("HealthController").GetComponent<PlayerUI>();    
        sprite = GetComponentInChildren<Animations>();
        //restartMenu = GameObject.FindWithTag("RestartMenu");
    }

    public void takeDamage(int damage)
    {
        if (delayDamage)
        {
            //health -= damage;
            playerStats.playerHealthData.ModifyPlayerHealth(-damage);
            health = playerStats.playerHealthData.GetPlayerHealth();

            if (healthControl != null)
            {
                healthControl.GetComponent<PlayerUI>().SetHealth(health);
            }

            StartCoroutine(damageTimer());
        }

        if(health <= 0) {
           if(!playerDead) {
                sprite.animation.SetTrigger("PlayerDead");
                //player.GetComponent<CharacterController>().enabled = false;
                //player.GetComponent<WeaponController>().enabled = false;
               // restartMenu.SetActive(true);
                playerDead = true;
                //Destroy(gameObject, 1f);
                //Time.timeScale = 0f;
           }
        }
    }
    private IEnumerator damageTimer()
    { //wait x seconds until player can take damage again
        delayDamage = false;
        //player transparent when taking damage
        sprite.spriteRenderer.color = new Color(1f,1f,1f,.5f);
        yield return new WaitForSeconds(damageTimeout);
        sprite.spriteRenderer.color = Color.white;
        delayDamage = true;
    }

    void WeaponEquip(Equipment item)
    {

    }
}
