using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyDamage
{
    //[Header ("Attack Parameters")]
    //[SerializeField] private float attackCooldown;
    //[SerializeField] private float range;

    //[Header ("Collider Parameters")]
    //[SerializeField] private BoxCollider2D boxCollider;
    //[SerializeField] private float colliderDistance;

    [Header ("Player Parameters")]
    [SerializeField] private PlayerDash playerDash;

    private void Awake() {
        BossHealth.BossDeathEvent += playerDash.EnableDash;
        BossHealth.BossDeathEvent += IncreaseProgressLevel;
    }

    private void IncreaseProgressLevel()
    {
        if (GameManager.instance.progressLevel == 0)
        {
            GameManager.instance.progressLevel++;
        }
    }
}
