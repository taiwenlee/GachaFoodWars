using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerUI healthControl;

    [Header("Stats")]
    public int health = 4;
    public float damageTimeout = 1f; // prevent too many hits at once. set in seconds
    private bool delayDamage = true;
    public void takeDamage(int damage)
    {
        if (delayDamage)
        {
            health -= damage;
            healthControl.GetComponent<PlayerUI>().SetHealth(health);
            StartCoroutine(damageTimer());
        }
    }
    private IEnumerator damageTimer()
    { //wait x seconds until player can take damage again
        delayDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        delayDamage = true;
    }

    void WeaponEquip(Equipment item)
    {

    }
}
