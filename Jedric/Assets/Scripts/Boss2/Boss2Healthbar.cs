using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Healthbar : MonoBehaviour
{
    [SerializeField] private Boss2Health health;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = health.currentHealth / 10;
        health.HealthChangeEvent += ChangeHealth;
    }


    public void ChangeHealth()
    {
        currenthealthBar.fillAmount = health.currentHealth / 10;
    }
}
