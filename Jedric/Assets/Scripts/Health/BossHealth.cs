using System.Collections;
using UnityEngine;

public class BossHealth : Health
{
    [Header("Boss Parameters")]
    [SerializeField] private GameObject powerUpInstructions;
    private Animator anim;

    public delegate void BossDeath();
    public static BossDeath BossDeathEvent;

    public override void Awake()
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if (!base.dead && base.currentHealth == 0)
        {
            anim.SetTrigger("die");
            gameObject.SetActive(false);
            BossDeathEvent.Invoke();
            base.dead = true;
            if (powerUpInstructions != null)
            {
                powerUpInstructions.SetActive(true);
            }
        }
    }
}
