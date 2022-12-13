using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_UI : MonoBehaviour
{
    [SerializeField] private Image healthbar_IMG;

    public void Update_Healthbar(float fillAmount)
    {
        healthbar_IMG.fillAmount = fillAmount;
    }
}
