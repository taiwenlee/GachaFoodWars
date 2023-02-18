using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform player;
    public virtual void Interact ()
    {
        Debug.Log("Interacting with " + gameObject);
    }
    private void Update()
    {
/*        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= radius)
        {
            //Debug.Log("Interact");
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Interact();
            float distance = Vector3.Distance(other.GetComponent<Player>().transform.position, transform.position);
        }
    }

    public void InRadius(Transform newPlayer)
    {
        player = newPlayer;
    }
        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
