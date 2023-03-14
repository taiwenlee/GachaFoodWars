using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    public float lifeTime = 5.0f;
    public string[] ignoreTags = { };
    public Vector3 direction;
    private Collider col;
    public WeaponController.Element element = WeaponController.Element.None;
    public int elementLevel = 0;
    public float elementDuration = 0f;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ignore collisions if tag is in ignore list
        foreach (string tag in ignoreTags)
        {
            if (other.gameObject.tag == tag)
            {
                return;
            }
        }

        // if not part of ignore list
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().takeDamage(damage);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage, element, elementLevel, elementDuration);

        }
        Destroy(gameObject);
    }
}