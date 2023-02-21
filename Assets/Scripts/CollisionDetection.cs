using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    private bool isDamaged = false;
    /*    private void OnTriggerEnter(Collider other)
        {
            Debug.Log("hello");
            if (other.gameObject.CompareTag("Enemy") && wc.isAttacking == true)
            {
                Debug.Log(other.gameObject.name);
                //other.gameObject.GetComponent<Enemy>().TakeDamage(50);
            }
        }*/
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("bye");
        if (other.gameObject.CompareTag("Enemy") && wc.isAttacking == true && isDamaged == false)
        {
            //Debug.Log(other.gameObject.name + "Dealing: " + wc.em.equipmentSelected.damageStat);
            isDamaged = true;
            other.gameObject.GetComponent<Enemy>().TakeDamage(wc.em.equipmentSelected.damageStat);
        }
        if (wc.isAttacking == false)
            isDamaged = false;
    }
}
