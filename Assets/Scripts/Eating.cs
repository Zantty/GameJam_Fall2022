using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public DragonEnergy dragonEnergy;
    public VillagerManager villagerManager;
    public DragonController dragonController;
    public GoldPickup goldPickup;

    [Space(10)]

    [Header("Dragon Attack Properties")]
    public float dragonDamage = 5;
    public float attackRate = 1;    // seconds
    float nextAttack = 0;

    public float regenerationAmount = 10;

    [Space(10)]

    public AudioSource eatingAudio;
    public AudioClip[] eatingSounds;

    private AudioSource animalAudio;

    [SerializeField] private GameObject bloodParticle;

    private void Start()
    {
        //dragonController.GetComponent<DragonController>();
        villagerManager = GameObject.FindObjectOfType<VillagerManager>();
    }
    private void Update()
    {
        if(nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dragonController.flying)
        {
            if (collision.gameObject.tag == "Animal" || collision.gameObject.tag == "Villager")
            {
                collision.gameObject.GetComponent<Villager_Visual>().Toggle_Highlight(true);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!dragonController.flying)
        {
            if (collision.gameObject.tag == "Animal" || collision.gameObject.tag == "Villager")
            {
                collision.gameObject.GetComponent<Villager_Visual>().Toggle_Highlight(false);
            }
        }
            
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!dragonController.flying && !goldPickup.carrying)
        {
            if (Input.GetKey(KeyCode.E) && nextAttack <= 0)
            {
                if (collision.gameObject.tag == "Animal")
                {
                    Debug.Log("Ate an animal!");

                    GameObject instantiatedParticle = GameObject.Instantiate(bloodParticle);
                    instantiatedParticle.transform.position = collision.transform.position;

                    Destroy(collision.gameObject);

                    dragonEnergy.Eat();
                    GetComponent<DragonHealth>().AddDamage(-regenerationAmount);
                    nextAttack = attackRate;
                    PlayRandomSound();
                    if(collision.GetComponentInParent<AnimalSpawner>())
                    {
                        collision.GetComponentInParent<AnimalSpawner>().EatenAnimal();
                    }
                }
                else if (collision.gameObject.tag == "Villager")
                {
                    Debug.Log("Attacked a villager!");

                    Villager_Health villager = collision.gameObject.GetComponent<Villager_Health>();
                    villager.AddDamage(dragonDamage);
                    if (villager.Get_CurrentHealth() <= 0)
                    {
                        Debug.Log("Ate a villager!");

                        GameObject instantiatedParticle = GameObject.Instantiate(bloodParticle);
                        instantiatedParticle.transform.position = collision.transform.position;

                        dragonEnergy.Eat();
                        villager.Die();
                        PlayRandomSound();
                        villagerManager.DecreaseVillagers();
                    }
                    nextAttack = attackRate;
                }
            }
        }
        else
        {
            Debug.Log("Cannot eat while flying.");
        }            
    }

    void PlayRandomSound()
    {
        eatingAudio.clip = eatingSounds[Random.Range(0, eatingSounds.Length)];
        eatingAudio.Play();
    }
}
