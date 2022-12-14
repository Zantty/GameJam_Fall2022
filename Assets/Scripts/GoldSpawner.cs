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
        Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        Instantiate(goldPrefab, pos, Quaternion.identity);
    }
}
