using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAI : Enemy
{
    public AudioSource meleeAttackSFX;
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
        // check if player is in line of sight, chase if true
        var rayDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out var hit, sightRange, ~LayerMask.GetMask("Ignore Raycast")))
        {
            if (hit.collider.gameObject == player && agent.isActiveAndEnabled
            & Vector3.Distance(transform.position, player.transform.position) > attackRange)
            {
                agent.SetDestination(player.transform.position);
                //flip sprite if player is on the left
                spriteRenderer.flipX = player.transform.position.x < transform.position.x;
            }
        }

        // check if player is in attack range, attack if true
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && element != WeaponController.Element.Electric)
        {
            // reset agent destination
            agent.SetDestination(transform.position);
            // attack player
            if (attackcooldown <= 0.0f)
            {
                meleeAttackSFX.Play();
                spriteAnimator.SetTrigger("Attack");
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
