using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(11, 12, true);
    }

    public void ActivateProjectile()
    {
        hit = false;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(-movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        coll.enabled = false;

        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(1);

        gameObject.SetActive(false); //When this hits any object deactivate arrow
    }
}