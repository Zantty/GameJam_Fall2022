using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonEnergy : MonoBehaviour
{
    public DragonController dragon;
    public Slider energySlider;

    public float maxEnergy;
    public float minEnergy;
    [SerializeField] private float currentEnergy;

    public const float decreaseAmount = 1.0f;

    public float replenishAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy / 2;
        energySlider.value = currentEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnergy -= decreaseAmount * Time.deltaTime;
        energySlider.value = currentEnergy;

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
