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
    private Rigidbody rb;
    private Collider col;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction.normalized * speed;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ignore collisions if tag is in ignore list
        foreach (string tag in ignoreTags)
        {
            if (collision.gameObject.tag == tag)
            {
                return;
            }
        }

        // if not part of ignore list
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().takeDamage(damage);
        }
        else if (collision.gameObject.tag == "Enemy")
        {    
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
