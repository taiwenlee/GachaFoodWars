using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && wc.isAttacking == true)
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<Enemy>().TakeDamage(50);
        }
    }
}
