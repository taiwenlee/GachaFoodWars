using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public int value = 1;
    public float range = 1;
    public bool pickable = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pickable)
        {
            // NOTE: the logic could potentially be put into the player script rather than have it be in the drop object
            // get player position and zero out y
            var PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            PlayerPos.y = 0;
            // check if player is in range
            if (Vector3.Distance(transform.position, PlayerPos) < range)
            {
                // add value to player
                // *add code here*
                // destroy drop
                Destroy(gameObject);
            }
        }
    }
}
