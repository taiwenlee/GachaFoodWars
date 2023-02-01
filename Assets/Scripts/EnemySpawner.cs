using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;

    public float spawnRate = 1.0f;  // number of spawns per second

    private float spawnTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 1.0f / spawnRate)
        {
            spawnTimer = 0.0f;
            spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        // spawn an enemy at a random location a certain distance away from the player

        // get a random point on the navmesh
        var position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

        // if position too close to player, move it away
        if (Vector3.Distance(position, Player.transform.position) < 5.0f)
        {
            position = Player.transform.position + (position - Player.transform.position).normalized * 5.0f;
        }

        NavMesh.SamplePosition(position, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas);

        // spawn the enemy
        if (Random.Range(0, 2) == 0)
        {
            var enemy = Instantiate(meleeEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<MeleeAI>().player = Player;
        }
        else
        {
            var enemy = Instantiate(rangedEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<RangeAI>().player = Player;
        }
    }
}
