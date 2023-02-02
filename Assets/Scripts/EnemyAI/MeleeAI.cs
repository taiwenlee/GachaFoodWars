using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAI : MonoBehaviour
{
    [Header("References")]
    public GameObject player;

    [Header("Stats")]
    public float health = 100.0f;
    public float speed = 3.0f;
    public int damage = 1;
    public float attackRate = 1.0f;
    public float attackRange = 1.0f;
    public float sightRange = 10.0f;

    // private variables
    private NavMeshAgent agent;
    private float attackcooldown = 0.0f;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        print("test");
    }

    // Update is called once per frame
    void Update()
    {
        // update agent speed
        agent.speed = speed;

        // check if player is in line of sight, chase if true
        var rayDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out var hit, sightRange))
        {
            if (hit.collider.gameObject == player)
            {
                agent.SetDestination(player.transform.position);
            }
        }

        // check if player is in attack range, attack if true
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            // attack player
            if (attackcooldown <= 0.0f)
            {
                // subject to change based on how we implement player health
                Debug.Log("Player took " + damage + " damage!");
                attackcooldown = 1 / attackRate;
            }
        }

        // decrement attack cooldown
        if (attackcooldown > 0.0f)
        {
            attackcooldown -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        // draw wireframe for attack range
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // draw wireframe for sight range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
