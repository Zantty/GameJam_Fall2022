using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Animal")
        {
            if(Input.GetKey(KeyCode.E))
            {
                 Debug.Log("Ate an animal!");
                 Destroy(collision.gameObject);
            }           
        }
    }
}
