using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && wc.isAttacking == true)
        {
            Debug.Log(other.gameObject.name);
            if(other.gameObject.name == "MeleeEnemy(Clone)")
            {
                Debug.Log("Doing dmg to melee");
                other.gameObject.GetComponent<MeleeAI>().TakeDamage(50);
            }
            if (other.gameObject.name == "RangeEnemy(Clone)")
            {
                Debug.Log("Doing dmg to range");

                other.gameObject.GetComponent<RangeAI>().TakeDamage(50);
            }
        }
    }
}
