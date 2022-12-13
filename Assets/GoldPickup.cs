using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public int goldScore = 0;
    SafeZone safeZone;
    bool carrying;
    GameObject player;

    private void Start()
    {
        safeZone = GetComponent<SafeZone>();
        player = this.gameObject;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gold")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!carrying)
                {
                    carrying = true;
                    collision.transform.parent = player.transform;
                    Debug.Log("Picked up some gold!");
                }
                if (safeZone.safe)
                {
                    Destroy(collision.gameObject);
                    goldScore++;
                    Debug.Log("Scored some gold!");
                }
                
            }
        }
    }
}
