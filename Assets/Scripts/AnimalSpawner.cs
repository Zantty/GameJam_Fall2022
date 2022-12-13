using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] animals;
    GameObject currentAnimal;

    public float waitForNextSpawn;
    public float countdown;

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            SpawnAnimal();
            countdown = waitForNextSpawn;
        }
    }

    void SpawnAnimal()
    {
        GameObject animal = animals[Random.Range(0, animals.Length)];
        Instantiate(animal, spawnPoint);
    }
}
