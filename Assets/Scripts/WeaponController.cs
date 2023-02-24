using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject weapon;
    public EquipmentManager em;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;
    public bool isSelected = false;

    [Header("Weapon Sounds")]
    public AudioSource audioSource;
    public AudioClip swordSound;
    public AudioClip spearSound;

     void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack && em.equipmentSelected)
            {
                //Debug.Log(weapon);
                audioSource.PlayOneShot(audioSource.clip);
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
        if (isAttacking == false)
        {
            switch (em.equipmentSelected.gSlots)
            {
                case WeaponType.Sword:
                    //Debug.Log("Sword");
                    SetWeapon("Sword");
                    DisableChild(this.transform.Find("Sword").gameObject);
                    audioSource.clip = swordSound;
                    //isSelected = true;
                    break;
                case WeaponType.Spear:
                    //Debug.Log("Spear");
                    SetWeapon("Spear");
                    DisableChild(this.transform.Find("Spear").gameObject);
                    audioSource.clip = spearSound;
                    //isSelected = true;
                    break;
                case WeaponType.Range:
                    //Debug.Log("range");
                    break;
            }
        }
    }
    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
/*        Animator anim = Weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");*/
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
    private void DisableChild(GameObject child)
    {
        if(child.activeInHierarchy)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                var children = this.transform.GetChild(i).gameObject;
                if (children != null && children != child)
                    children.SetActive(false);
            }
        }
        isSelected = true;
    }
    private void SetWeapon(string weaponSel)
    {
        this.transform.Find(weaponSel).gameObject.SetActive(true);
        weapon = this.transform.Find(weaponSel).gameObject;
    }
}
