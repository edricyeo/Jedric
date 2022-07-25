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
    private Vector3 playerPosition;
    private Vector3 movementVector;
    private bool homing = false;

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

    public void ToggleHomingProjectile() {
        homing = !homing;
    }

    public void LaunchProjectile() {
        launched = true;

        if (homing) {
            currLifetime = 0;
        } else {
            movementVector = (player.position - transform.position).normalized * speed;
        }
    }

    private void Update()
    {
        if (!player.gameObject.activeSelf)
            return;

        currLifetime += Time.deltaTime;
        if (hit) return;

        if (launched && !homing) {
            transform.position += movementVector * Time.deltaTime;
        } else if (launched && homing) {
            float movementSpeed = (speed/3) * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed);
            if (currLifetime > resetTime) {
                hit = true;
                coll.enabled = false;
                gameObject.SetActive(false);
                launched = false;
                homing = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(1);

        hit = true;
        coll.enabled = false;
        gameObject.SetActive(false);
        launched = false;
        homing = false;
    }
}