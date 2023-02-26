using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        spriteAnimator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Debug.Log(spriteAnimator);
        Debug.Log(spriteRenderer);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // update agent speed
        agent.speed = speed;

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
                Debug.Log("Playing death sound");
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

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
