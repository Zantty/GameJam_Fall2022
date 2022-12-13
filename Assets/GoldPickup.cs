using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldPickup : MonoBehaviour
{
    public int goldScore = 0;
    public int maxScore;
    public SafeZone safeZone;
    [SerializeField] bool carrying;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gold;
    [SerializeField] TMP_Text goldScoreText;

    bool win = false;

    private void Start()
    {
        safeZone = GetComponent<SafeZone>();
        player = this.gameObject;
        goldScoreText.text = goldScore.ToString();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gold")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!carrying)
                {
                    gold = collision.gameObject;
                    carrying = true;
                    gold.transform.parent = player.transform;
                    Debug.Log("Picked up some gold!");
                    
                }                
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
        }
    }
}
