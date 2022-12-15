using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

[RequireComponent(typeof(Slider))]
public class AudioSliders : MonoBehaviour
{
    Slider audioSlider
    {
        get { return GetComponent<Slider>(); }
    }
    public AudioMixer mixer;

    [SerializeField]
    public string volumeName;

    [SerializeField]
    private TMP_Text volumeLevelText;

    private void Start()
    {
        //UpdateValueOnChange(audioSlider.value);
        audioSlider.value = AudioListener.volume;

        audioSlider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(audioSlider.value);
        }
        );
    }
    public void UpdateValueOnChange(float value)
    {
        if (mixer != null)
        {
            mixer.SetFloat(volumeName, Mathf.Log(value) * 20f);
        }

        if (volumeLevelText != null)
        {
            volumeLevelText.text = Mathf.Round(value * 100.0f).ToString() + "%";
        }

    }
}