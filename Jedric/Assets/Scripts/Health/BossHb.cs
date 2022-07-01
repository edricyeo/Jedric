using UnityEngine;
using UnityEngine.UI;

public class BossHb : MonoBehaviour
{
    // grab reference to boss / player health script
    [SerializeField] private Health health;
    [SerializeField] private Image fill;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;

    private void Start()
    {
        SetStartHealth(health.startingHealth);
        health.HealthChangeEvent += ChangeHealth;
    }

    public void ChangeHealth()
    {
        slider.value = health.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetStartHealth(float maxHp)
    {
        slider.maxValue = maxHp;
        slider.value = maxHp;
        fill.color = gradient.Evaluate(1f);
    }
}
