using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerUI healthControl;
    public GameObject sprite;
    public GameObject player;

    [Header("Stats")]
    public int health = 4;
    public float damageTimeout = 1f; // prevent too many hits at once. set in seconds
    private bool delayDamage = true;
    private bool playerDead = false;

    public void Start()
    {
        //healthControl = GameObject.FindWithTag("HealthController").GetComponent<PlayerUI>();    
    }

    public void takeDamage(int damage)
    {
        if (delayDamage)
        {
            health -= damage;
            if (healthControl != null)
            {
                healthControl.GetComponent<PlayerUI>().SetHealth(health);
            }
            StartCoroutine(damageTimer());
        }
        if(health <= 0) {
           if(!playerDead) {
                sprite.GetComponent<Animations>().animation.SetTrigger("PlayerDead");
                //player.GetComponent<CharacterController>().enabled = false;
                //player.GetComponent<WeaponController>().enabled = false;
                playerDead = true;
                //Destroy(gameObject, 1f);
                //Time.timeScale = 0f;
           }
        }
    }
    private IEnumerator damageTimer()
    { //wait x seconds until player can take damage again
        delayDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        delayDamage = true;
    }

    void WeaponEquip(Equipment item)
    {

    }
}
