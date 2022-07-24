using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyDamage
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint1;
    [SerializeField] private Transform firepoint2;
    [SerializeField] private Transform[] firepointArray;
    [SerializeField] private GameObject[] bolts;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private enum Attack {ThreeConsec, AlternatingAttack, RainingAttack}
    private int nextAttack;
    private int excludedFirepoint;
    private GameObject currentBolt;

    //References
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        BossHealth.BossDeathEvent += IncreaseProgressLevel;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            if (nextAttack == (int) Attack.ThreeConsec) {
                anim.SetTrigger("attack02");
            } else if (nextAttack == (int) Attack.AlternatingAttack) {
                anim.SetTrigger("attack");
            } else {
                anim.SetTrigger("rainingAttack");
            }
            nextAttack = Random.Range(0,3);
        }
    }

    private void LowerAttack()
    {   
        currentBolt = bolts[FindBolt()];
        cooldownTimer = 0;
        currentBolt.transform.position = firepoint1.position;
        currentBolt.GetComponent<Boss2Projectile>().ActivateProjectile();
        currentBolt.GetComponent<Boss2Projectile>().LaunchProjectile();
    }

    private void UpperAttack()
    {
        currentBolt = bolts[FindBolt()];
        cooldownTimer = 0;
        currentBolt.transform.position = firepoint2.position;
        currentBolt.GetComponent<Boss2Projectile>().ActivateProjectile();
        currentBolt.GetComponent<Boss2Projectile>().LaunchProjectile();
    }
/*
idea for raining fireballs:
1. store array of firepoints
2. choose random index to not place a fireball
for each firepoint in array
    if index, ignore
    else bolt.transform.position = firepoint position

3. start coroutine to allow player to escape
4. activate all projectiles
*/
    private void PlaceFireballs() {
        excludedFirepoint = Random.Range(0, firepointArray.Length);
        
        for (int i = 0; i < firepointArray.Length; i++) {
            currentBolt = bolts[FindBolt()];
            if (excludedFirepoint != i) {
                currentBolt.transform.Rotate(0, 0, 90.0f);
                currentBolt.transform.position = firepointArray[i].position;
                currentBolt.GetComponent<Boss2Projectile>().ToggleRainAttack();
                currentBolt.GetComponent<Boss2Projectile>().ActivateProjectile();
            }
        }
        StartCoroutine(AttackBuffer());
    }

    private IEnumerator AttackBuffer() {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < bolts.Length; i++)
            if (bolts[i].activeInHierarchy)
                bolts[i].GetComponent<Boss2Projectile>().LaunchProjectile();
    }

/*
code for raining projectiles
- have 6 different firepoints 
- randomly choose 6
- for each bolt, set position to firepoint position
- translate downwards
*/
    private int FindActiveBolt() {
    for (int i = 0; i < bolts.Length; i++)
        {
            if (bolts[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private int FindBolt()
    {
        for (int i = 0; i < bolts.Length; i++)
        {
            if (!bolts[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void IncreaseProgressLevel()
    {
        if (GameManager.instance.progressLevel == 1)
        {
            GameManager.instance.progressLevel++;
        }
    }
}
