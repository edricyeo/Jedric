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

    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    private Health playerHealth;
    private BossMovement bossMovement;

    private void Awake() {
        anim = GetComponent<Animator>();
        bossMovement = GetComponentInParent<BossMovement>();
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        //Note that boss fight doesn't need this feature

        /*
        if (PlayerInSight()) {
            if (cooldownTimer >= attackCooldown) {
                cooldownTimer = 0;
                anim.SetTrigger("attack"); 
            }
        }

        if (enemyPatrol != null) {
            enemyPatrol.enabled = !PlayerInSight(); 
        }
        

        if (cooldownTimer >= attackCooldown) {
            anim.SetTrigger("attack");
            //set coroutine to wait 
            chargeAttack();
        }
        */


    }

    private bool PlayerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * -transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,  boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        
        if (hit.collider != null) {
            playerHealth = hit.transform.GetComponent<Health>();
            Debug.Log(gameObject.name);
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * -transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,  boxCollider.bounds.size.z));    
    }

    private void DamagePlayer() {
        if (PlayerInSight()) {
            playerHealth.TakeDamage(1);
        }
    }
}
