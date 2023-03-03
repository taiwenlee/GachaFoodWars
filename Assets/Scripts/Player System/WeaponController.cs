using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject weapon;
    public EquipmentManager em;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public float attackSpeed;
    public bool isAttacking = false;
    public bool isSelected = false;

    [Header("Weapon Sounds")]
    public AudioSource audioSource;
    public AudioClip swordSound;
    public AudioClip spearSound;

    //[Header("Animations")]
    private WeaponAnimation sprite;
    private bool swordSwing = false;
    private bool spearSwing = false;

    [Header("Stats")]
    private float attackDuration = 0.2f;
    public float damageMultiplier = 1;
    public float hitboxMultiplier = 1;
    public float attackSpeedMultiplier = 1;
    public Element element = Element.None;
    public int elementLevel = 0;
    public float knockbackForce = 0;
    public static WeaponController instance;

    public enum Element { None, Fire, Ice, Electric };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (em == null)
        {
            em = GameObject.FindWithTag("Inventory").GetComponent<EquipmentManager>();
            em.wc = this;
        }
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponentInChildren<WeaponAnimation>();
        WeaponSelector();
        setModifiers();
    }

    // Update is called once per frame
    void Update()
    {
        sprite = GetComponentInChildren<WeaponAnimation>();
        if (this.GetComponentInParent<Player>().playerDead != true)
        {
            if (Input.GetMouseButton(0))
            {
                if (CanAttack && em.currentEquipment[0])
                {
                    //Debug.Log(weapon);
                    if (swordSwing)
                    {
                        sprite.animation.SetTrigger("swordSlash");

                    }
                    if (spearSwing)
                    {
                        sprite.animation.SetTrigger("spearSlash");

                    }
                    audioSource.PlayOneShot(audioSource.clip);
                    SwordAttack();
                }
            }
        }
    }


    public void WeaponSelector()
    {
        if (isAttacking == false && em.currentEquipment.Length > 0 && em.currentEquipment[0] != null)
        {
            switch (((Equipment)em.currentEquipment[0]).gSlots)
            {
                case WeaponType.Sword:
                    DisableChild();
                    SetWeapon("Sword");
                    attackSpeed = ((Equipment)em.currentEquipment[0]).attackSpeed;
                    audioSource.clip = swordSound;
                    weapon.transform.localScale = new Vector3(1, 1, 1) * hitboxMultiplier;
                    swordSwing = true;
                    spearSwing = false;
                    //isSelected = true;
                    break;
                case WeaponType.Spear:
                    DisableChild();
                    SetWeapon("Spear");
                    attackSpeed = ((Equipment)em.currentEquipment[0]).attackSpeed;
                    audioSource.clip = spearSound;
                    weapon.transform.localScale = new Vector3(1, 1, 1) * hitboxMultiplier;
                    spearSwing = true;
                    swordSwing = false;
                    //isSelected = true;
                    break;
                case WeaponType.Range:
                    DisableChild();
                    SetWeapon("Range");
                    attackSpeed = ((Equipment)em.currentEquipment[0]).attackSpeed;
                    //weapon.transform.localScale = new Vector3(1, 1, 1) * hitboxMultiplier;
                    break;
                default:
                    DisableChild();
                    SetWeapon("None");
                    Debug.Log("This is not a weapon");
                    break;

            }
        }
    }
    public void SwordAttack()
    {
        isAttacking = true;
        // wait .1 second then set CanAttack to false
        CanAttack = false;
        /*        Animator anim = Weapon.GetComponent<Animator>();
                anim.SetTrigger("Attack");*/
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(1 / (attackSpeed * attackSpeedMultiplier));
        CanAttack = true;
    }

    private IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;

    }
    private void DisableChild()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var children = this.transform.GetChild(i).gameObject;
            if (children != null)
                children.SetActive(false);
        }

        isSelected = true;
    }
    private void SetWeapon(string weaponSel)
    {
        if (weaponSel == "None")
        {
            weapon = null;
            return;
        }
        this.transform.Find(weaponSel).gameObject.SetActive(true);
        weapon = this.transform.Find(weaponSel).gameObject;
    }

    private void onValidate()
    {
        if (weapon != null)
        {
            weapon.transform.localScale = new Vector3(1, 1, 1) * hitboxMultiplier;
        }
    }

    public void setModifiers()
    {

        // reset multipliers
        damageMultiplier = 1;
        hitboxMultiplier = 1;
        attackSpeedMultiplier = 1;
        element = Element.None;
        elementLevel = 0;
        for (int i = 1; i < em.currentEquipment.Length; i++)
        {
            if (em.currentEquipment[i] != null)
            {
                var modifier = (Modifier)em.currentEquipment[i];
                switch (modifier.mType)
                {
                    case Modifier.ModifierType.Damage:
                        damageMultiplier += .2f * ((int)modifier.grade + 1);
                        break;
                    case Modifier.ModifierType.Hitbox:
                        hitboxMultiplier += .2f * ((int)modifier.grade + 1);
                        break;
                    case Modifier.ModifierType.AttackSpeed:
                        attackSpeedMultiplier += .2f * ((int)modifier.grade + 1);
                        break;
                    case Modifier.ModifierType.Fire:
                        elementLevel += (int)modifier.grade;
                        element = Element.Fire;
                        break;
                    case Modifier.ModifierType.Ice:
                        elementLevel += (int)modifier.grade;
                        element = Element.Ice;
                        break;
                    case Modifier.ModifierType.Electric:
                        elementLevel += (int)modifier.grade;
                        element = Element.Electric;
                        break;
                    case Modifier.ModifierType.Knockback:
                        knockbackForce += (int)modifier.grade * 10;
                        break;
                    case Modifier.ModifierType.MultiHit:
                        break;
                }
            }
        }
        // update weapon hitbox
        if (weapon != null)
        {
            weapon.transform.localScale = new Vector3(1, 1, 1) * hitboxMultiplier;
        }
    }
}
