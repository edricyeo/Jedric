using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = health.currentHealth / 10;
        currenthealthBar.fillAmount = health.currentHealth / 10;
    }


    public void ChangeHealth()
    {
        currenthealthBar.fillAmount = health.currentHealth / 10;
    }
}
