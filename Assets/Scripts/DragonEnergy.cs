using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonEnergy : MonoBehaviour
{
    public DragonController dragon;
    public Slider energySlider;

    private DragonHealth health;

    public float maxEnergy;
    public float minEnergy;
    public float currentEnergy;

    public const float decreaseAmount = 1.0f;
    public const float decreaseAmountFlying = 2.0f;

    public float replenishAmount;
    public float healthDecreaseAmount = 1;

    bool incapacitated = false;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy / 2;
        energySlider.value = currentEnergy;

        health = GetComponent<DragonHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!incapacitated)
        {
            if (!dragon.flying)
            {
                currentEnergy -= decreaseAmount * Time.deltaTime;
            }

            else if (dragon.flying)
            {
                currentEnergy -= decreaseAmountFlying * Time.deltaTime;
            }
        }

        energySlider.value = currentEnergy;

        if (currentEnergy <= minEnergy)
        {
            incapacitated = true;
        }
        if(currentEnergy >= maxEnergy)
        {
            dragon.movementSpeed = dragon.fatMovementSpeed;
            dragon.flying = false;
            incapacitated = true;
        }

        if(currentEnergy <= 0)
        {
            health.AddDamage(healthDecreaseAmount * Time.deltaTime);
        }
    }

    public void Eat()
    {
        currentEnergy += replenishAmount;
    }

}
