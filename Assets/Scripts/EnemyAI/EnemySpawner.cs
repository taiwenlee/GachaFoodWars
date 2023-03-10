using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject meleeEnemy;
    public GameObject tankEnemy;
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
            enemy.GetComponent<MeleeAI>().maxHealth = 90;
            enemy.GetComponent<MeleeAI>().damage = 2;
            enemy.GetComponent<MeleeAI>().baseSpeed = 7f;
            enemy.GetComponent<MeleeAI>().attackRate = 1f;
        }
        else if (name.ToLower() == "tank")
        {
            var enemy = Instantiate(tankEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<MeleeAI>().player = Player;
            enemy.transform.parent = transform;
            // modify enemy stats
            //Todo: make stat modifier a function rather than hardcoding
            enemy.GetComponent<MeleeAI>().maxHealth = 150;
            enemy.GetComponent<MeleeAI>().damage = 3;
            enemy.GetComponent<MeleeAI>().baseSpeed = 5f;
            enemy.GetComponent<MeleeAI>().attackRate = 1f;
        }
        else if (name.ToLower() == "range")
        {
            var enemy = Instantiate(rangedEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<RangeAI>().player = Player;
            enemy.transform.parent = transform;
            //Todo: make stat modifier a function rather than hardcoding
            enemy.GetComponent<RangeAI>().maxHealth = 100;
            enemy.GetComponent<RangeAI>().damage = 1;
            enemy.GetComponent<RangeAI>().baseSpeed = 5f;
        }
        else if (name.ToLower() == "miniboss")
        {
            var enemy = Instantiate(miniBossEnemy, hit.position, Quaternion.identity);
            enemy.GetComponent<MiniBossAI>().player = Player;
            enemy.transform.parent = transform;
            //Todo: make stat modifier a function rather than hardcoding
            enemy.GetComponent<MiniBossAI>().maxHealth = 400;
            enemy.GetComponent<MiniBossAI>().damage = 1;
            enemy.GetComponent<MiniBossAI>().baseSpeed = 3f;
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
