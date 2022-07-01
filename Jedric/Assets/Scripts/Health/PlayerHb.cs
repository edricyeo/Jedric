using UnityEngine;
using UnityEngine.UI;

public class PlayerHb : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        currenthealthBar.fillAmount = health.currentHealth / 10;
        totalhealthBar.fillAmount = health.currentHealth / 10;
        health.HealthChangeEvent += ChangeHealth;
    }


    public void ChangeHealth()
    {
        currenthealthBar.fillAmount = health.currentHealth / 10;
    }
}
