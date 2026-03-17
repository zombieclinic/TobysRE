
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] public Gradient gradient;
    [SerializeField] public Image fill;


    public void SetMaxPower(float Power)
    {
        slider.maxValue = Power;
        slider.value = Power;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetBatteryPower(float Power)
    {
        slider.value = Power;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
