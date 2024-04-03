using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundChanger : MonoBehaviour
{
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();    
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = slider.value;
    }

    public void ChangeSoundVolume(string soundName)
    {
        AudioManager.Instance.SetVolume(soundName, slider.value);
    }
}
