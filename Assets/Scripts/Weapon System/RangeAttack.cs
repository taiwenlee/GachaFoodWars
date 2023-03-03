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
            var projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            projectileInstance.GetComponent<Projectile>().element = wc.element;
            projectileInstance.GetComponent<Projectile>().elementLevel = wc.elementLevel;
            projectileInstance.GetComponent<Projectile>().direction = transform.rotation * Vector3.forward;
            projectileInstance.GetComponent<Projectile>().damage = ((Equipment)wc.em.currentEquipment[0]).damageStat * (int)wc.damageMultiplier;
            projectileInstance.transform.localScale *= wc.hitboxMultiplier;
            Debug.Log(projectileInstance.GetComponent<Projectile>().damage);
            projectileInstance.GetComponent<Projectile>().speed = 15;
            projectileInstance.GetComponent<Projectile>().ignoreTags = new string[] { "Player", "Projectile" };
        }
        if(wc.isAttacking == false)
        {
            hasFired = false;
        }
    }
}
