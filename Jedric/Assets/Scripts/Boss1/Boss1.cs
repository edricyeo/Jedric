using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyDamage
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header ("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private PlayerLevel player;

    private void Awake() {
        //BossHealth.BossDeathEvent += LevelUpPlayer;
        BossHealth.BossDeathEvent += IncreaseProgressLevel;
    }

    //private void LevelUpPlayer()
    //{
    //    if (player.level == 1)
    //    {
    //        player.LevelUp();
    //    }
    //}

    private void IncreaseProgressLevel()
    {
        if (GameManager.instance.progressLevel == 0)
        {
            GameManager.instance.progressLevel++;
        }
    }
}
