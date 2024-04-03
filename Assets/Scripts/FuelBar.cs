using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    [SerializeField] RocketEngine engine;
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = engine.Fuel;
        slider.value = slider.maxValue;
    }

    public void ChangeValue(float delta)
    {
        slider.value -= delta;
    }
}
