using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayer : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100.0f;
    public float speed = 3.0f;
    [SerializeField] private PlayerHealth healthControl;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // movement 
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, 0, z);
    }

    public void takeDamage(int damage)
    {
        healthControl.playerHealth -= damage; // subtracts hearts
        healthControl.UpdateHealth(); //updates it visually
        Debug.Log("Player took " + damage + " damage!");
    }
}
