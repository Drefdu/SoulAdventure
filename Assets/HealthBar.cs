using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxHelth(int heaalth)
    {
        slider.maxValue = heaalth;
        slider.value = heaalth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

}
