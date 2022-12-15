using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;


[System.Serializable]
public class DaySettings
{
    public Color lightColor;
    public AudioClip ambientMusic;
}

public class DayCycle : MonoBehaviour
{
    public float dayDuration;
    float nextDay;

    public int currentDay = 1;
    public List<DaySettings> daySettings;

    private Light2D globalLight;
    public float lightChangeSpeed = 1;

    [SerializeField] private TextMeshProUGUI dayCount_TMP;


    public int Get_CurrentDay()
    {
        return currentDay;
    }

    public void Update_Day()
    {
        int daySections = daySettings.Count;
        float dayMeter = (nextDay / dayDuration) * daySections;
        int daySettingsIndex = (daySettings.Count - 1) - (int)dayMeter;


        if (daySettingsIndex < daySettings.Count && daySettingsIndex >= 0)
        {
            globalLight.color = Color.Lerp(globalLight.color, daySettings[daySettingsIndex].lightColor, Time.deltaTime * lightChangeSpeed);
        }
    }

    void Update_UI()
    {
        if(dayCount_TMP)
        {
            dayCount_TMP.text = "Day: " + currentDay.ToString();
        }
    }

    public GoldPickup goldPickup;
    private void Start()
    {
        globalLight = GetComponent<Light2D>();

        nextDay = dayDuration;
        Update_UI();
    }

    private void Update()
    {
        if (goldPickup.win)
        {
            if (nextDay <= 0)
            {
                currentDay++;
                nextDay = dayDuration;
                Update_UI();
            }
            else
            {
                nextDay -= Time.deltaTime;
            }
            Update_Day();
        }      
    }
}
