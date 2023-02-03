using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCamera : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // follows the player at a distance with isometric view
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 10, player.transform.position.z - 10);
        transform.LookAt(player.transform);
    }
}
