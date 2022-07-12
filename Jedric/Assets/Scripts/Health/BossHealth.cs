using System.Collections;
using UnityEngine;

public class BossHealth : Health
{
    // need to generalize this to all boss classes subsequently
    //[SerializeField] private GameObject dashInstructions;
    private Animator anim;

    public delegate void BossDeath();
    public static BossDeath BossDeathEvent;

    public override void Awake()
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //if (dashInstructions != null)
        //{
        //    BossHealth.BossDeathEvent += dashTutorial;
        //}
    }

    //public void dashTutorial() {
    //    dashInstructions.SetActive(true);
    //}

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if (base.currentHealth > 0)
        {
            //boss hurt
            anim.SetTrigger("hurt");
        }
        else if (!base.dead)
        {
            anim.SetTrigger("die");
            gameObject.SetActive(false);
            BossDeathEvent.Invoke();
            base.dead = true;
        }
    }
}
