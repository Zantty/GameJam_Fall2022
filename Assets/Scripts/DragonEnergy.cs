using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnergy : MonoBehaviour
{
    public DragonController dragon;

    public float maxEnergy;
    public float minEnergy;
    [SerializeField] private float currentEnergy;

    public const float decreaseAmount = 1.0f;

    public float replenishAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy / 2;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnergy -= decreaseAmount * Time.deltaTime;

        if(currentEnergy <= minEnergy)
        {
            dragon.dead = true;
        }
        if(currentEnergy >= maxEnergy)
        {
            dragon.dead = true;
        }
    }

    public void Eat()
    {
        currentEnergy += replenishAmount;
    }
}
