using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float moveSpd;
    [SerializeField] private float agroRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float atkRange;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Vector3 initialScale;
    private Health playerHealth;

    void Awake()
    {
        anim = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight()) { 
            //Attack only when player in sight
            if (cooldownTimer >= attackCooldown)
            {
                //attack
                cooldownTimer = 0;
                anim.SetTrigger("attack02");
            }
        }

        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center - transform.right * atkRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * atkRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center - transform.right * atkRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * atkRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void MoveInDirection(int dir)
    {
        // Make boss face direction
        transform.localScale = new Vector3(-Mathf.Abs(initialScale.x) * dir, initialScale.y, initialScale.z);
        // Make boss move in that direction
        transform.position = new Vector3(transform.position.x + Time.deltaTime * dir * moveSpd, transform.position.y, transform.position.z);
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            //Damage player
            playerHealth.TakeDamage(damage);
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            // enemy on left of player, move right
            MoveInDirection(1);
        }
        else if (transform.position.x > player.position.x)
        {
            // enemy on right of player, move left
            MoveInDirection(-1);
        }
    }

    private void StopChasingPlayer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
