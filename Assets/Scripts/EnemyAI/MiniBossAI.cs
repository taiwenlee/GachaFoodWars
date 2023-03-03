using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAI : Enemy
{
    [Header("MiniBoss AI Settings")]
    public float chargeForce = 100.0f; // force to charge at player
    public float knockbackForce = 10.0f; // force to knockback player
    // damage timeout insures that the player cant be hit multiple times in a single attack
    public float damageTimeout = 1f; // time between damage ticks
    private float damageCooldown = 0.0f;
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

        // check if player is in line of sight
        var rayDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out var hit, sightRange))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                // check if player is in attack range, attack if true
                if (rayDirection.magnitude <= attackRange && attackcooldown <= 0.0f && element != Element.Electric)
                {
                    agent.ResetPath();
                    rb.AddForce(rayDirection.normalized * chargeForce, ForceMode.Impulse);
                    attackcooldown = 1 / attackRate;
                }
                else if (agent.isActiveAndEnabled && attackcooldown <= 0.0f)
                {
                    // chase player if too far away
                    agent.SetDestination(player.transform.position - rayDirection.normalized * attackRange * .9f);
                }
            }
        }

        // decrement attack cooldown
        if (attackcooldown > 0.0f)
        {
            attackcooldown -= Time.deltaTime;
        }

        // decrement damage cooldown
        if (damageCooldown > 0.0f)
        {
            damageCooldown -= Time.deltaTime;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCooldown <= 0.0f)
            {
                Debug.Log("Player hit by boss");
                collision.gameObject.GetComponent<Player>().takeDamage(damage);
                damageCooldown = damageTimeout;
                // knockback the player in the direction of the collision
                var knockbackDirection = new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.z - transform.position.z);
                knockbackDirection.Normalize();
                collision.gameObject.GetComponent<Movement>().Knockback(knockbackDirection, knockbackForce);

                // knockback the boss in the opposite direction of the collision
                var bossKnockbackDirection = new Vector3(-knockbackDirection.x, 0, -knockbackDirection.y);
                bossKnockbackDirection.Normalize();
                rb.AddForce(bossKnockbackDirection * knockbackForce, ForceMode.Impulse);

            }
        }
    }
}
