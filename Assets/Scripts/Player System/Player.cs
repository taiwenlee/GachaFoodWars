using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [Header("References")]
    //public PlayerUI healthControl;
    public PlayerStats playerStats;
    private Animations sprite;
    public PlayerUI heartSystem;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    //public GameObject player;

    [Header("Stats")]
    public int health = 4;
    public int maxHeart = 4;
    public float damageTimeout = 1f; // prevent too many hits at once. set in seconds
    private bool delayDamage = true;
    public bool playerDead = false;
    private float shakeTimer;


    [Header("Audio")]
    public AudioSource takingDamageSFX;
    public AudioSource dyingSFX;
    public AudioSource deathBGM;

    //[Header("Damage Visualizer")]
    //public int numFlicker = 9; //num times player flickers
    //public float flickerDuration = .1f;

    public void Start()
    {
        cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        //healthControl = GameObject.FindWithTag("HealthController").GetComponent<PlayerUI>();
        health = playerStats.playerHealthData.GetPlayerHealth();
        maxHeart = playerStats.playerHealthData.GetDefaultHealth();
        heartSystem = GameObject.FindWithTag("HealthController").GetComponent<PlayerUI>();
        heartSystem.DrawHearts(health, maxHeart);
        heartSystem.GetComponent<PlayerUI>().SetHealth(health);
        sprite = GetComponentInChildren<Animations>();
    }

    public void takeDamage(int damage)
    {
        if (health != 0 & takingDamageSFX != null)
        {
            takingDamageSFX.Play();
            ShakeCamera(5f,.1f);
        }
        if (delayDamage)
        {

            //health -= damage;
            playerStats.playerHealthData.ModifyPlayerHealth(-damage);
            health = playerStats.playerHealthData.GetPlayerHealth();
            heartSystem.DrawHearts(health, maxHeart);

            /*if (healthControl != null)
            {
                healthControl.GetComponent<PlayerUI>().SetHealth(health);
            }*/

            StartCoroutine(damageTimer());
        }

        if (health <= 0)
        {
            if (!playerDead)
            {
                dyingSFX.Play();
                Debug.Log("Player died");
                sprite.animation.SetTrigger("PlayerDead");
                deathBGM.Play();
                //player.GetComponent<CharacterController>().enabled = false;
                //player.GetComponent<WeaponController>().enabled = false;
                playerDead = true;
                //Destroy(gameObject, 1f);
                //Time.timeScale = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time) {
        CinemachineBasicMultiChannelPerlin c = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        c.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update(){ 
        if(shakeTimer >= 0) {// duration of camera shake
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f) {
                CinemachineBasicMultiChannelPerlin c = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                c.m_AmplitudeGain = 0f;
            }
        }
    }

    private IEnumerator damageTimer()
    { //wait x seconds until player can take damage again
        delayDamage = false;
        //player transparent when taking damage
        sprite.spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(damageTimeout);
        sprite.spriteRenderer.color = Color.white;
        delayDamage = true;
    }
}
