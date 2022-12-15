using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_Spawner : MonoBehaviour
{
    public VillagerManager villagerManager;
    public float spawnRate = 5;     // seconds
    float nextSpawn = 0;
    public float maxVillagers;
    [Tooltip("Should always be less than spawnRate")]    public float spawnRandomness = 1;
    public float spawnDistance = 1;

    [SerializeField]    private GameObject[] spawnPrefabs;

    public void SpawnVillagers()
    {
        GameObject spawnedPrefab = GameObject.Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)]);
        spawnedPrefab.transform.position = transform.position + ((Vector3)Random.insideUnitCircle * spawnDistance);
    }

    void Start()
    {
        nextSpawn = spawnRate;
        villagerManager = GameObject.FindObjectOfType<VillagerManager>();
    }

    void Update()
    {
        if(nextSpawn <= 0)
        {
            if(villagerManager.currentVillagers <= villagerManager.maxVillagers)
            {
                SpawnVillagers();
                nextSpawn = spawnRate + Random.Range(-spawnRandomness, spawnRandomness);
                villagerManager.IncreaseVillagers();
            }
            else
            {
                Debug.Log("Reached the max number of spawned villagers.");
            }
        }
        else
        {
            nextSpawn -= Time.deltaTime;
        }
    }
}
