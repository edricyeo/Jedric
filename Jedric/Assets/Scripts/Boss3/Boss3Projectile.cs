using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private Animator anim;
    private BoxCollider2D coll;
    private bool launched = false;
    private float currLifetime;

    private bool hit;
    private Transform player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(9, 12, true);
    }

    public void ActivateProjectile()
    {
        hit = false;
        gameObject.SetActive(true);
        coll.enabled = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void LaunchProjectile() {
        launched = true;
        currLifetime = 0;
    }

    private void Update()
    {
        currLifetime += Time.deltaTime;
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;

        if (launched) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed);
            if (currLifetime > resetTime) {
                hit = true;
                coll.enabled = false;
                gameObject.SetActive(false);
                launched = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(1);

        gameObject.SetActive(false);
        launched = false;
    }
}