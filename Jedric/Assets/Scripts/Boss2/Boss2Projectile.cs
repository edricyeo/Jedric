using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator anim;
    private BoxCollider2D coll;
    private bool launched = false;
    private bool rainAttack = false;

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

    public void ToggleRainAttack() {
        rainAttack = true;
    }

    public void LaunchProjectile() {
        launched = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;

        if (launched) {
            if (rainAttack) {
                //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                transform.Translate(-movementSpeed*2, 0, 0);
            } else {
                transform.Translate(-movementSpeed, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        coll.enabled = false;

        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(1);

        gameObject.SetActive(false);
        gameObject.transform.rotation = Quaternion.identity;
        launched = false;
        rainAttack = false;
    }
}