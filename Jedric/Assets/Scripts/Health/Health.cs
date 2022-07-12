using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health Parameters")]
    public float startingHealth;
    public float currentHealth; //{ get; protected set; }
    protected bool dead;

    // Events and Delegates
    public delegate void HealthChange();
    public HealthChange HealthChangeEvent;

    // Sound
    [SerializeField] protected AudioClip hurtSound;

    public virtual void Awake()
    {
        currentHealth = startingHealth;
        dead = false;
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, startingHealth);
        HealthChangeEvent.Invoke();
        SoundManager.instance.PlaySound(hurtSound);
    }

}
