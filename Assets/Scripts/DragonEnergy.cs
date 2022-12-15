using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public TMP_Text warningText;

    [SerializeField] private SpriteAnimation spriteAnim;
    public float fatThreshhold = 80;
    public float skinnyThreshhold = 20;

    // Start is called before the first frame update
    void Start()
    {
        warningText.enabled = false;
        currentEnergy = maxEnergy / 2;
        energySlider.value = currentEnergy;

        health = GetComponent<DragonHealth>();
        spriteAnim = GetComponent<SpriteAnimation>();
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

        if(currentEnergy <= 25f)
        {
            warningText.text = "You are getting hungry!";
            warningText.enabled = true;
        }
        if (currentEnergy >= 75)
        {
            warningText.text = "You are getting too full!";
            warningText.enabled = true;
        }
        if (currentEnergy <= minEnergy)
        {
            incapacitated = true;
            warningText.text = "You are starving.";
            warningText.enabled = true;
        }
        if(currentEnergy >= minEnergy)
        {
            incapacitated = false;
        }
        if(currentEnergy >= 25f && currentEnergy <= 75f)
        {
            warningText.enabled = false;
        }
        if (currentEnergy >= maxEnergy)
        {
            dragon.movementSpeed = dragon.fatMovementSpeed;
            dragon.flying = false;
            incapacitated = true;
            warningText.text = "You are too full...";
            warningText.enabled = true;
        }

        if (currentEnergy <= 0)
        {
            health.AddDamage(healthDecreaseAmount * Time.deltaTime);
        }

        if(currentEnergy < skinnyThreshhold)
        {
            spriteAnim.UpdateDragonSprites(DragonStatus.SKINNY);
        }
        else if(currentEnergy > fatThreshhold)
        {
            spriteAnim.UpdateDragonSprites(DragonStatus.FAT);
        }
        else
        {
            spriteAnim.UpdateDragonSprites(DragonStatus.NORMAL);
        }
    }

    public void Eat()
    {
        currentEnergy += replenishAmount;
    }

  /*IEnumerator IncapacitatedIndicator()
    {
        if (currentEnergy <= minEnergy)
        {
            warningText.text = "You are starving.";
            warningText.enabled = true;
            yield return new WaitForSeconds(3.0f);
            warningText.enabled = false;
        }
        if(currentEnergy >= maxEnergy)
        {
            warningText.text = "You are too full...";
            warningText.enabled = true;
            yield return new WaitForSeconds(3.0f);
            warningText.enabled = false;
        }
    }*/
}
