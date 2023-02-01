using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeAI : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject projectile;

    [Header("Stats")]
    public float health = 100.0f;
    public float speed = 2.0f;
    public int damage = 1;
    public float attackRate = 1.0f;
    public float sightRange = 20.0f;

    public float targetDistance = 9.0f; // distance from player to stop at
    public float targetRange = 1.0f; // the range in the target distance where AI wont move

    // private variables
    private NavMeshAgent agent;
    private float attackcooldown = 0.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // update agent speed
        agent.speed = speed;

        // check if player is in line of sight, move to 9 units away from player if true
        var rayDirection = player.transform.position - transform.position;
        bool inRange = rayDirection.magnitude > targetDistance - targetRange && rayDirection.magnitude < targetDistance + targetRange;
        if (Physics.Raycast(transform.position, rayDirection, out var hit, sightRange))
        {
            if (hit.collider.gameObject == player)
            {
                if (!inRange)
                {
                    agent.SetDestination(player.transform.position - rayDirection.normalized * targetDistance);
                }
                else if (attackcooldown <= 0.0f)
                {
                    // spawns a projectile that moves towards the player
                    var projectileInstance = Instantiate(projectile, transform.position + rayDirection.normalized * 0.5f, Quaternion.identity);
                    projectileInstance.GetComponent<Projectile>().owner = gameObject;
                    projectileInstance.GetComponent<Projectile>().direction = rayDirection;

                    attackcooldown = attackRate;
                }

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
        // draw wireframe for target range
        Gizmos.DrawWireSphere(transform.position, targetDistance - targetRange);
        Gizmos.DrawWireSphere(transform.position, targetDistance + targetRange);

        // draw wireframe for sight range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
