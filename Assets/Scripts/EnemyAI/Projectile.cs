using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    public float lifeTime = 5.0f;
    public Vector3 direction;
    private Rigidbody rb;
    private Collider col;
    public GameObject owner;   // the object that fired this projectile
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
        if (collision.gameObject == owner || collision.gameObject.name == gameObject.name)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            // subject to change based on how we implement player health
            collision.gameObject.GetComponent<Player>().takeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
