using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_Visual : MonoBehaviour
{
    private Color initialColor;

    [Header("highlighting Properties")]
    public Color highlightColor = Color.white;
    public GameObject highlightElement;


    private void Start()
    {
        initialColor = GetComponent<SpriteRenderer>().color;

        Toggle_Highlight(false);
    }


    public void Toggle_Highlight(bool highlight)
    {
        GetComponent<SpriteRenderer>().color = highlight ? highlightColor : initialColor;
        if(highlightElement)
        {
            highlightElement.SetActive(highlight);
        }
    }
}
