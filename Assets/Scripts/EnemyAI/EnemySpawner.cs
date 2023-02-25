using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public GameObject miniBossEnemy;
    public SpawnerData[] datas;

    // Start is called before the first frame update
    void Start()
    {
        // placeholder to test starting the level, should be moved to another script
        // BeginLevel("3");
    }

    // Update is called once per frame
    void Update()
    {
    }

    // spawns enemies based on the data received
    public int BeginLevel(SpawnerData data)
    {
        Debug.Log($"Spawning enemies for Room {data.name}");
        spawnEnemys(data);
        return 0;
    }

    // initiates the spawner given a level name
    // returns 0 if unsuccessful, 1 if successful
    public int BeginLevel(string level)
    {
        // find the spawner data for the level
        foreach (SpawnerData data in datas)
        {
            if (data.LevelName == level)
            {
                Debug.Log("Spawning enemies for level " + level);
                spawnEnemys(data);
                return 1;
            }
        }

        return 0;
    }

    // spawns enemies based on the spawner data
    void spawnEnemys(SpawnerData data)
    {
        for (int i = 0; i < data.mobNames.Length; i++)
        {
            // check if there is a spawn point for this enemy
            var pos = new Vector3(0, 0, 0);
            if (data.spawnPoints.Length > i)
            {
                pos = data.spawnPoints[i];
            }
            else
            {
                // set random position if no spawn point is given
                pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            }

            spawnEnemy(data.mobNames[i], pos);
        }
    }

    // spawns enemy with type based on name in a random location
    void spawnEnemy(string name, Vector3 position)
    {
        // bound the position to the navmesh
        NavMesh.SamplePosition(position, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas);

        // spawn the enemy
        if (name.ToLower() == "melee")
        {
            var enemy = Instantiate(meleeEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<MeleeAI>().player = Player;
            enemy.transform.parent = transform;
            //Todo: make stat modifier a function rather than hardcoding
        }
        else if (name.ToLower() == "tank")
        {
            var enemy = Instantiate(meleeEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<MeleeAI>().player = Player;
            enemy.transform.parent = transform;
            // modify enemy stats
            //Todo: make stat modifier a function rather than hardcoding
            enemy.GetComponent<MeleeAI>().health = 200;
            enemy.GetComponent<MeleeAI>().damage = 2;
            enemy.GetComponent<MeleeAI>().speed = 1.5f;
        }
        else if (name.ToLower() == "range")
        {
            var enemy = Instantiate(rangedEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<RangeAI>().player = Player;
            enemy.transform.parent = transform;
            //Todo: make stat modifier a function rather than hardcoding
        }
        else if (name.ToLower() == "miniboss")
        {
            var enemy = Instantiate(miniBossEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<MiniBossAI>().player = Player;
            enemy.transform.parent = transform;
        }
        else
        {
            Debug.Log("Enemy type " + name + " not found");
        }
    }
    // returns the number of enemies still alive
    public int getEnemyCount()
    {
        return transform.childCount;
    }

    // removes all enemies from the scene
    public void removeEnemys()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
