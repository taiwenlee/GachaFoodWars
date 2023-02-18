using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject drop;
    public GameObject player;
    [Header("Stats")]
    public float health = 100.0f;
    public float speed = 8.0f;
    public int damage = 1;


    public int dropValue = 1;

    protected NavMeshAgent agent;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // update agent speed
        agent.speed = speed;
        if (health <= 0)
        {
            // drop loot
            if (dropValue > 0)
            {
                var dropInstance = Instantiate(drop, transform.position, Quaternion.identity);
                dropInstance.GetComponent<Drop>().value = dropValue;
                dropInstance.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), 2, Random.Range(-1f, 1f));
            }
            // destroy enemy
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
