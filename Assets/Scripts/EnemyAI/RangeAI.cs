using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeAI : Enemy
{
    [Header("References")]
    public GameObject projectile;

    [Header("Stats")]
    public float attackRate = 1.0f;
    public float sightRange = 20.0f;

    public float targetDistance = 9.0f; // distance from player to stop at
    public float targetRange = 1.0f; // the range in the target distance where AI wont move

    // private variables
    private float attackcooldown = 0.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // check if player is in line of sight, move to 9 units away from player if true
        var rayDirection = player.transform.position - transform.position;
        bool inRange = rayDirection.magnitude > targetDistance - targetRange && rayDirection.magnitude < targetDistance + targetRange;
        if (Physics.Raycast(transform.position, rayDirection, out var hit, sightRange, ~LayerMask.GetMask("Ignore Raycast")))
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
                    projectileInstance.GetComponent<Projectile>().direction = rayDirection;
                    projectileInstance.GetComponent<Projectile>().damage = damage;
                    projectile.transform.parent = transform;
                    attackcooldown = 1 / attackRate;
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
