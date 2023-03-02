using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public WeaponController wc;
    public GameObject projectile;
    private bool hasFired = false;
    // Update is called once per frame
    void Update()
    {
        if (wc.isAttacking == true && hasFired == false)
        {
            hasFired = true;
            // Mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir;
            if (Physics.Raycast(ray, out RaycastHit hit, 100, ~LayerMask.GetMask("Ignore Raycast")))
            {
                // Creates the projectile
                dir = hit.point - transform.position;
                var projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
                projectileInstance.GetComponent<Projectile>().direction.x = dir.x;
                projectileInstance.GetComponent<Projectile>().direction.z = dir.z;
                projectileInstance.GetComponent<Projectile>().damage = ((Equipment)wc.em.currentEquipment[0]).damageStat;
                Debug.Log(projectileInstance.GetComponent<Projectile>().damage);
                projectileInstance.GetComponent<Projectile>().speed = 15;
                projectileInstance.GetComponent<Projectile>().ignoreTags = new string[] { "Player", "Projectile" };
            }
        }
        if(wc.isAttacking == false)
        {
            hasFired = false;
        }
    }
}
