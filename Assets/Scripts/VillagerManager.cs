using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public int maxVillagers = 150;
    public int currentVillagers = 0;

    private void Start()
    {
        
    }
    public void IncreaseVillagers()
    {
        currentVillagers++;
    }

    public void DecreaseVillagers()
    {
        currentVillagers--;
    }
}
