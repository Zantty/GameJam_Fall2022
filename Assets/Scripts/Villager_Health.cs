using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    [SerializeField] private Healthbar_UI healthbar;
    [SerializeField] private CanvasGroup healthbarCanvas;
    bool healthbarVisibility = false;
    public float healthbarVisibilityTime = 2;
    float healthbarTimeLeft = 0;
    public float healthbarPopupSpeed = 1;


    void Start()
    {
        currentHealth = maxHealth;
        healthbarCanvas.alpha = 0;
    }

    void Update()
    {
        if(healthbarVisibility)
        {
            healthbarCanvas.alpha = Mathf.Lerp(healthbarCanvas.alpha, 1, Time.deltaTime * healthbarPopupSpeed);
        }
        else
        {
            healthbarCanvas.alpha = Mathf.Lerp(healthbarCanvas.alpha, 0, Time.deltaTime * healthbarPopupSpeed);
        }

        if(healthbarTimeLeft > 0)
        {
            healthbarTimeLeft -= Time.deltaTime;
        }
        else
        {
            healthbarVisibility = false;
        }
    }

    public float Get_CurrentHealth()
    {
        return currentHealth;
    }

    public void AddDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if(healthbar)
        {
            healthbar.Update_Healthbar(currentHealth / maxHealth);
        }

        healthbarVisibility = true;
        healthbarTimeLeft = healthbarVisibilityTime;
    }

    public void Die()
    {
        GameObject.Destroy(this.gameObject);
    }
}
