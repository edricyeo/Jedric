using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyDamage
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    private Transform boss3Transform;
    private Transform player;
    private RaycastHit2D hit;
    private Vector2 initScale;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boss3Transform = GetComponent<Transform>();
        initScale = boss3Transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("Teleport");
        }
    }

    private void Teleport() {
        if (boss3Transform.position.x > player.position.x) {
            boss3Transform.localScale = new Vector2(Mathf.Abs(initScale.x), initScale.y);
            boss3Transform.position = new Vector2(player.position.x + 0.8f, boss3Transform.position.y);
        } else {
            boss3Transform.localScale = new Vector2(-Mathf.Abs(initScale.x), initScale.y);
            boss3Transform.position = new Vector2(player.position.x - 0.8f, boss3Transform.position.y);
        }
        anim.SetTrigger("Appear");
    }

    private void MeleeAttack() {
        anim.SetTrigger("Attack");
    }

    private void DealDamage() {
        if (PlayerInSight()) {
            if (hit.collider.CompareTag("Player")) {
                hit.collider.GetComponent<Health>().TakeDamage(1.0f);
            }
        }
    }    

    private bool PlayerInSight()
    {
        hit =
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
}
