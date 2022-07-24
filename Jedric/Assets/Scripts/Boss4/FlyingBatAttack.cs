using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBatAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] bolts;
    private GameObject currentBolt;

    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    private Transform player;
    private Vector2 initScale;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        initScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            anim.SetTrigger("BoltAttack");
            cooldownTimer = 0;
        }
    }

    private void FireBolt()
    {
        currentBolt = bolts[FindBolt()];
        currentBolt.transform.position = firepoint.position;
        currentBolt.GetComponent<FlyingBatProjectile>().ActivateProjectile();
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
}
