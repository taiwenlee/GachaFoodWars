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
            other.gameObject.GetComponent <MeleeAI>().TakeDamage(50);
           // other.gameObject.GetComponent<RangeAI>().TakeDamage(50);
            Debug.Log(other.gameObject.GetComponent<MeleeAI>().health);
        }
    }
}
