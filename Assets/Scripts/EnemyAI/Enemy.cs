using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject drop;

    public GameObject player;
    public AudioClip[] deathSounds;
    [Header("Stats")]
    public float health = 100.0f;
    public float speed = 8.0f;
    public int damage = 1;
    public int dropValue = 1;
    public float invunerableTime = .5f;
    private float invunerableTimer = 0.0f;
    public float maxHealth = 100.0f;

    [Header("Health Bar")]
    public Slider slider;
    public GameObject enemyType;

    protected NavMeshAgent agent;
    protected Rigidbody rb;
    protected Animator spriteAnimator;
    protected SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        slider.value = healthBarUpdate();
        health = maxHealth;
        spriteAnimator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // update agent speed
        agent.speed = speed;

        //updates slider value
        slider.value = healthBarUpdate();
        //updates healthbar ui
        if(health < maxHealth) 
        {
            enemyType.SetActive(true);
        }

        // update invunerable timer
        if (invunerableTimer > 0.0f)
        {
            invunerableTimer -= Time.deltaTime;
            if (invunerableTimer <= 0.0f)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        // walk animation
        if (agent.velocity.magnitude > .5 && spriteAnimator != null)
        {
            // start walk animation
            spriteAnimator.SetFloat("WalkSpeed", speed);
        }
        else if (spriteAnimator != null)
        {
            // stop walk animation
            spriteAnimator.SetFloat("WalkSpeed", 0);
        }

        // death check
        if (health <= 0)
        {
            // drop loot
            if (dropValue > 0)
            {
                var dropInstance = Instantiate(drop, transform.position, Quaternion.identity);
                dropInstance.GetComponent<Drop>().value = dropValue;
                dropInstance.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), 2, Random.Range(-1f, 1f));
            }
            // play random death sound
            if (deathSounds.Length > 0)
            {
                var deathSound = deathSounds[Random.Range(0, deathSounds.Length)];
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }
            //play death animation
            if (spriteAnimator != null)
            {
                spriteAnimator.SetBool("Dead", true);
            }
            // destroy enemy
            agent.enabled = false;
            Destroy(gameObject, 1.0f);
            this.enabled = false;
        }
    }
    
    //updates healthbar based on health/maxhealth
    float healthBarUpdate() {
        return health/maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (invunerableTimer <= 0.0f)
        {
            invunerableTimer = invunerableTime;
            health -= damage;
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        }
    }
}
