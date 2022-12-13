using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_Spawner : MonoBehaviour
{
    public float spawnRate = 5;     // seconds
    float nextSpawn = 0;
    [Tooltip("Should always be less than spawnRate")]    public float spawnRandomness = 1;
    public float spawnDistance = 1;

    [SerializeField]    private GameObject spawnPrefab;

    public void SpawnVillagers()
    {
        GameObject spawnedPrefab = GameObject.Instantiate(spawnPrefab);
        spawnedPrefab.transform.position = transform.position + ((Vector3)Random.insideUnitCircle * spawnDistance);
    }

    void Start()
    {
        nextSpawn = spawnRate;
    }

    void Update()
    {
        if(nextSpawn <= 0)
        {
            SpawnVillagers();
            nextSpawn = spawnRate + Random.Range(-spawnRandomness, spawnRandomness);
        }
        else
        {
            nextSpawn -= Time.deltaTime;
        }
    }
}
