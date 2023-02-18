using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAI : Enemy
{
    [Header("Stats")]
    public float attackRate = 1.0f;
    public float attackRange = 1.0f;
    public float sightRange = 10.0f;
    public float damageTimeout = 1f;

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
                player.GetComponent<Player>().takeDamage(damage);
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
