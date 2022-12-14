using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public DragonEnergy dragonEnergy;

    [Space(10)]

    [Header("Dragon Attack Properties")]
    public float dragonDamage = 5;
    public float attackRate = 1;    // seconds
    float nextAttack = 0;

    public AudioSource eatingAudio;
    public AudioClip[] eatingSounds;

    private AudioSource animalAudio;

    private void Update()
    {
        if(nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Animal" || collision.gameObject.tag == "Villager")
        {
            collision.gameObject.GetComponent<Villager_Visual>().Toggle_Highlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Animal" || collision.gameObject.tag == "Villager")
        {
            collision.gameObject.GetComponent<Villager_Visual>().Toggle_Highlight(false);
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && nextAttack <= 0)
        {
            if (collision.gameObject.tag == "Animal")
            {
                Debug.Log("Ate an animal!");

                Destroy(collision.gameObject);

                dragonEnergy.Eat();
                nextAttack = attackRate;
                PlayRandomSound();
                
            }
            else if (collision.gameObject.tag == "Villager")
            {
                Debug.Log("Attacked a villager!");

                Villager_Health villager = collision.gameObject.GetComponent<Villager_Health>();
                villager.AddDamage(dragonDamage);
                if(villager.Get_CurrentHealth() <= 0)
                {
                    Debug.Log("Ate a villager!");

                    dragonEnergy.Eat();
                    villager.Die();
                    PlayRandomSound();
                }
                nextAttack = attackRate;
            }
        }
        
    }

    void PlayRandomSound()
    {
        eatingAudio.clip = eatingSounds[Random.Range(0, eatingSounds.Length)];
        eatingAudio.Play();
    }
}
