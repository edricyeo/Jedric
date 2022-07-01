using UnityEngine;
using UnityEngine.UI;

public class Healthbar2 : MonoBehaviour
{
    // grab reference to boss / player health script
    [SerializeField] private Health health;
    public Slider slider;

    private void Start()
    {
        SetStartHealth(health.startingHealth);
        health.HealthChangeEvent += ChangeHealth;
    }


    public void ChangeHealth()
    {
        slider.value = health.currentHealth;
    }

    public void SetStartHealth(float maxHp)
    {
        slider.maxValue = maxHp;
        slider.value = maxHp;
    }
    
}
