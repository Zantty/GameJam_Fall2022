using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldPickup : MonoBehaviour
{
    public DragonController dragonController;
    public DayCycle dayCycle;

    public int goldScore = 0;
    public int maxScore;
    public SafeZone safeZone;
    public bool carrying;
    [SerializeField] GameObject player;
    [SerializeField] GameObject goldInRange;
    GameObject goldCarrying = null;
    [SerializeField] TMP_Text goldScoreText;
    [SerializeField] TMP_Text dayCountTotal;
    [SerializeField] private GameObject winScreen;
    bool inSafeZone = false;

    [SerializeField] private GameObject scoreParticle;

    public bool win = false;

    [SerializeField] private AudioSource goldAudio;
    [SerializeField] private AudioClip[] goldAudioClips;
    private AudioClip currentClip;

    private void Start()
    {
        safeZone = GetComponent<SafeZone>();
        player = this.gameObject;
        goldScoreText.text = goldScore.ToString();
        winScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dragonController.flying)
        {
            if (collision.gameObject.tag == "Gold")
            {
                goldInRange = collision.gameObject;
                collision.gameObject.GetComponent<Villager_Visual>().Toggle_Highlight(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!dragonController.flying)
        {
            if (collision.gameObject.tag == "Gold")
            {
                goldInRange = null;
                collision.gameObject.GetComponent<Villager_Visual>().Toggle_Highlight(false);
            }
        }
        if(collision.gameObject.tag == "SafeZone")
        {
            inSafeZone = false;
        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag == "Gold")
        {
            if (!dragonController.flying)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (!carrying)
                    {
                        gold = collision.gameObject;
                        //   carrying = true;
                        gold.transform.parent = player.transform;
                        Debug.Log("Picked up some gold!");
                    }
                    if (carrying)
                    {
                        //   carrying = false;
                        gold.transform.SetParent(null);
                        Debug.Log("Dropped some gold.");
                    }
                }
            }                                                
        }
        else
        {
            Debug.Log("Cannot pick up gold while flying.");
        }
        */

        if(collision.gameObject.tag == "SafeZone")
        {
            inSafeZone = true;
        }
    }


    public void ScoreGold()
    {
        if (safeZone.safe)
        {
            

            //   Destroy(gold.gameObject);
            goldScore++;
            Debug.Log("Scored some gold!");
         //   carrying = false;
        }
    }

    private void Update()
    {
        goldScoreText.text = goldScore.ToString() + "/10";
        if (goldScore >= maxScore)
        {
            Time.timeScale = 0f;
            win = true;
            winScreen.SetActive(true);
            dayCountTotal.text = "Krogar Survived \n" + dayCycle.currentDay.ToString() + "\n Days.";
        }

        if (Input.GetKeyDown(KeyCode.E) && !dragonController.flying)
        {
            if(carrying && inSafeZone)
            {
                currentClip = goldAudioClips[2];
                goldAudio.clip = currentClip;
                goldAudio.Play();

                GameObject instantiatedParticle = GameObject.Instantiate(scoreParticle);
                instantiatedParticle.transform.position = goldCarrying.transform.position;

                Destroy(goldCarrying);
                goldScore++;
                carrying = false;
                goldCarrying = null;

                GameObject.FindObjectOfType<Arrow>().displayArrow = false;
            }
            else
            {
                if (goldInRange != null && !carrying)
                {
                    currentClip = goldAudioClips[0];
                    goldAudio.clip = currentClip;
                    goldAudio.Play();

                    carrying = true;
                    goldCarrying = goldInRange;
                    goldInRange = null;
                    goldCarrying.transform.parent = player.transform;
                    Debug.Log("Picked up some gold!");

                    GameObject.FindObjectOfType<Arrow>().displayArrow = true;
                }
                else if (goldCarrying != null)
                {
                    currentClip = goldAudioClips[1];
                    goldAudio.clip = currentClip;
                    goldAudio.Play();

                    carrying = false;
                    goldCarrying.transform.parent = null;
                    goldCarrying = null;
                    Debug.Log("Dropped some gold.");

                    GameObject.FindObjectOfType<Arrow>().displayArrow = false;
                }
            }
        }
    }
}

