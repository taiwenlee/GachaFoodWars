using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision between " + gameObject.name + " and " + other.gameObject.name + " with tag " + other.gameObject.tag);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("sas");
        }
        if(wc.isAttacking)
        {
            Debug.Log("loda");
        }
        Debug.Log("check");
        if(other.gameObject.CompareTag("Enemy") && wc.isAttacking == true)
        {
            Debug.Log("hello");
        }
    }
}
