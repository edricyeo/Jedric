using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float range;
    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private RaycastHit2D hit;
    private PlayerHealth playerHealth;
    private Animator anim;
    private Transform player;
    private Vector2 initScale;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void DealDamage() {
        if (PlayerInSight() && PlayerHealth.isInvuln == false) {
            if (hit.collider.CompareTag("Player")) {
                hit.collider.GetComponent<Health>().TakeDamage(1.0f);
            }
        }
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }

    private bool PlayerInSight()
    {
        hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.up * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z),
            0, Vector2.up, 0, playerLayer);
        
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.up * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z));
    }
}
