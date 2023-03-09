using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeAI : Enemy
{
    [Header("Range AI References")]
    public GameObject projectile;

    [Header("Range AI Settings")]
    public float projectileSpeed = 10.0f;
    public float targetRange = 1.0f; // the range between the attack distance that the enemy wont try to move

    [Header("Range Audio Settings")]
    public AudioSource rangeAttackSFX;

    // Start is called before the first frame update
    protected override void Start()
    {
        // run parent script start function
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // check if player is in line of sight, move to 9 units away from player if true
        var rayDirection = player.transform.position - transform.position;
        bool inRange = rayDirection.magnitude > attackRange - targetRange && rayDirection.magnitude < attackRange + targetRange;
        if (Physics.Raycast(transform.position, rayDirection, out var hit, sightRange, ~LayerMask.GetMask("Ignore Raycast")))
        {
            if (hit.collider.gameObject == player)
            {
                if (!inRange && agent.isActiveAndEnabled)
                {
                    agent.SetDestination(player.transform.position - rayDirection.normalized * attackRange);
                }
                else if (attackcooldown <= 0.0f && element != WeaponController.Element.Electric)
                {
                    // spawns a projectile that moves towards the player
                    rangeAttackSFX.Play();
                    var projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectileInstance.GetComponent<Projectile>().direction = rayDirection;
                    projectileInstance.GetComponent<Projectile>().damage = damage;
                    projectileInstance.GetComponent<Projectile>().speed = projectileSpeed;
                    projectileInstance.GetComponent<Projectile>().ignoreTags = new string[] { "Enemy", "Projectile" };
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
        Gizmos.DrawWireSphere(transform.position, attackRange - targetRange);
        Gizmos.DrawWireSphere(transform.position, attackRange + targetRange);

        // draw wireframe for sight range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
