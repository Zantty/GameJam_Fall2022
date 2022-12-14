using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject goldPrefab;

    [Header("X Value Spawn Range")]
    public float xMin;
    public float xMax;

    [Header("Y Value Spawn Range")]
    public float yMin;
    public float yMax;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            SpawnGold();
        }
    }

    void SpawnGold()
    {
        GameObject gold = Instantiate(goldPrefab) as GameObject;
        gold.transform.position = new Vector2(Random.Range(xMax, xMax), Random.Range(yMin, yMax));
    }
}
