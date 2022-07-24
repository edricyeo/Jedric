using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : EnemyDamage
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header("Boomerang Attack")]
    [SerializeField] private Transform BoomerangFirepoint;
    [SerializeField] private GameObject[] bolts;
    private GameObject currentBolt;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Column Attack")]
    [SerializeField] private GameObject[] AttackColumns;

    //References
    private Animator anim;
    private Transform boss3Transform;
    private Transform player;
    private PlayerHealth playerHealth;
    private RaycastHit2D hit;
    private Vector2 initScale;
    private enum Attack {ColumnAttack, BoomerangAttack}
    private int nextAttack;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boss3Transform = GetComponent<Transform>();
        initScale = boss3Transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (!player.gameObject.activeSelf)
            return;

        if (cooldownTimer >= attackCooldown)
        {
            nextAttack = Random.Range(0,2);
            if (nextAttack == (int) Attack.ColumnAttack) { 
                CastColumnAttack();
            } else if (nextAttack == (int) Attack.BoomerangAttack) {
                CastBoomerangAttack();
            }
            cooldownTimer = 0;
        }
    }

    private void CastColumnAttack() {
        anim.SetTrigger("Cast");
    }

    private void ColumnAttack() {
        int excludedColumn = Random.Range(0,3);
        for (int column = 0; column < 3; column++) {
            if (column == excludedColumn)
                continue;
            AttackColumns[column].SetActive(true); //from here, animation will play, deactivate automatically
        }
    }

    private void CastBoomerangAttack() {
        anim.SetTrigger("CastBoomerang");
    }

    private void BoomerangAttack() {
        currentBolt = bolts[FindBolt()];
        currentBolt.transform.position = BoomerangFirepoint.position;
        currentBolt.GetComponent<BoomerangProjectile>().ActivateProjectile();
    }

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
