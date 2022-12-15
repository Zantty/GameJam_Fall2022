using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldPickup : MonoBehaviour
{
    public DragonController dragonController;

    public int goldScore = 0;
    public int maxScore;
    public SafeZone safeZone;
    public bool carrying;
    bool triggered;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gold;
    [SerializeField] TMP_Text goldScoreText;
    [SerializeField] private GameObject winScreen;

    bool win = false;

    private void Start()
    {
        safeZone = GetComponent<SafeZone>();
        player = this.gameObject;
        goldScoreText.text = goldScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dragonController.flying)
        {
            if (collision.gameObject.tag == "Gold")
            {
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
                collision.gameObject.GetComponent<Villager_Visual>().Toggle_Highlight(false);
            }
        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gold")
        {
            if (!dragonController.flying)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!carrying)
                    {
                        gold = collision.gameObject;
                        carrying = true;
                        gold.transform.parent = player.transform;
                        Debug.Log("Picked up some gold!");
                    }
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    if (carrying)
                    {
                        carrying = false;
                        gold.transform.SetParent(null);
                        Debug.Log("Dropped some gold.");
                    }
                }                           
                                                                  
        }
        else
        {
            Debug.Log("Cannot pick up gold while flying.");
        }
    }

        if(collision.gameObject.tag == "SafeZone")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (carrying)
                {
                    Destroy(gold);
                    goldScore++;
                    carrying = false;
                }
            }
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
        goldScoreText.text = goldScore.ToString();
        if (goldScore >= maxScore)
        {
            win = true;
            winScreen.SetActive(true);
        }

        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!carrying)
                {
                    carrying = true;
                }
                if (carrying)
                {
                    carrying = false;
                }
            }
        }
    }

    void PickUpAndDrop()
    {
        if (carrying)
        {
            carrying = false;
        }
        if (!carrying)
        {
            carrying = true;
        }
    }
}

