using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject weapon;
    public EquipmentManager em;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public int damage;
    public float attackSpeed;
    public float elementDuration = 3f;
    public float elementDurationMultiplier = 1f;
    public float baseHitboxMultiplier = 1;
    public bool isAttacking = false;
    public bool isSelected = false;

    [Header("Weapon Sounds")]
    public AudioSource audioSource;
    public AudioClip swordSound;
    public AudioClip spearSound;
    public AudioClip rangeSound;

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
                        if (Input.mousePosition.x < Screen.width / 2f)
                        { // if mouse on left side of player
                            sprite.spriteRenderer.flipX = false; //flip sprite towards x axis
                        }
                        else
                        {
                            sprite.spriteRenderer.flipX = true;
                        }
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
            var equipment = (Equipment)em.currentEquipment[0];
            switch (equipment.gSlots)
            {
                case WeaponType.Sword:
                    DisableChild();
                    SetWeapon("Sword");
                    audioSource.clip = swordSound;

                    swordSwing = true;
                    spearSwing = false;

                    // set damage, attack speed, and hitbox multiplier
                    damage = equipment.damageStat + (int)equipment.grade * 2;
                    attackSpeed = equipment.attackSpeed + (int)equipment.grade * 0.1f;
                    elementDurationMultiplier = 1 + (int)equipment.grade * 0.2f;
                    baseHitboxMultiplier = 1 + (int)equipment.grade * 0.1f;
                    weapon.transform.localScale = new Vector3(1, 1, 1) * baseHitboxMultiplier * hitboxMultiplier;

                    break;
                case WeaponType.Spear:
                    DisableChild();
                    SetWeapon("Spear");
                    audioSource.clip = spearSound;
                    spearSwing = true;
                    swordSwing = false;

                    // set damage, attack speed, and hitbox multiplier
                    damage = equipment.damageStat + (int)equipment.grade * 2;
                    attackSpeed = equipment.attackSpeed + (int)equipment.grade * 0.1f;
                    elementDurationMultiplier = 1 + (int)equipment.grade * 0.2f;
                    baseHitboxMultiplier = 1 + (int)equipment.grade * 0.1f;
                    weapon.transform.localScale = new Vector3(1, 1, 1) * baseHitboxMultiplier * hitboxMultiplier;

                    break;
                case WeaponType.Range:
                    DisableChild();
                    SetWeapon("Range");
                    audioSource.clip = rangeSound;
                    //weapon.transform.localScale = new Vector3(1, 1, 1) * hitboxMultiplier;
                    spearSwing = false;
                    swordSwing = false;
                    // set damage, attack speed, and hitbox multiplier
                    damage = equipment.damageStat + (int)equipment.grade * 2;
                    attackSpeed = equipment.attackSpeed + (int)equipment.grade * 0.1f;
                    elementDurationMultiplier = 1 + (int)equipment.grade * 0.2f;
                    baseHitboxMultiplier = 1 + (int)equipment.grade * 0.1f;
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
                        break;
                    case Modifier.ModifierType.Hitbox:
                        hitboxMultiplier += .2f * ((int)modifier.grade + 1);

                        break;
                    case Modifier.ModifierType.AttackSpeed:
                        attackSpeedMultiplier += .2f * ((int)modifier.grade + 1);
                        break;
                    case Modifier.ModifierType.Fire:
                        elementLevel += (int)modifier.grade;
                        elementDuration = 3.0f;
                        element = Element.Fire;
                        break;
                    case Modifier.ModifierType.Ice:
                        elementLevel += (int)modifier.grade;
                        elementDuration = 3.0f;
                        element = Element.Ice;
                        break;
                    case Modifier.ModifierType.Electric:
                        elementLevel += (int)modifier.grade;
                        elementDuration = .5f * ((int)modifier.grade + 1);
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
            weapon.transform.localScale = new Vector3(1, 1, 1) * baseHitboxMultiplier * hitboxMultiplier;
        }
    }
}
