using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Weapon;
    public EquipmentManager em;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(em);
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }
        }
        if (em.equipmentSelected != null)
        {
            WeaponSelector();
        }
    }

    public void WeaponSelector()
    {
        switch(em.equipmentSelected.GSlots)
        {
            case WeaponType.Sword:
                Debug.Log("sword");
                break;
            case WeaponType.Spear:
                Debug.Log("spear");
                break;
            case WeaponType.Range:
                Debug.Log("range");
                break;
        }
    }
    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
        Animator anim = Weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    private IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;

    }
}
