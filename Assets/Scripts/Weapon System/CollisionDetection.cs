using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;

    private void OnTriggerStay(Collider other)
    {
        // check if object is an enemy and if the player is attacking
        if (other.gameObject.CompareTag("Enemy") && wc.isAttacking == true)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage((int)(wc.damage * wc.damageMultiplier), wc.element, wc.elementLevel, wc.elementDuration * wc.elementDurationMultiplier);
        }
    }
}
