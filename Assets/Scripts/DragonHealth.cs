using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    private Healthbar_UI healthbar;
    [SerializeField] private GameObject gameOverMenu;

    private void Start()
    {
        currentHealth = maxHealth;

        healthbar = GameObject.FindObjectOfType<Healthbar_UI>();
        healthbar.Update_Healthbar(currentHealth / maxHealth);
    }

    public void AddDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if(healthbar)
        {
            healthbar.Update_Healthbar(currentHealth / maxHealth);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("u ded!");
        gameOverMenu.SetActive(true);
        GameObject.Destroy(this.gameObject);
    }
}