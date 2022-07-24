using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    [Header("Turning Points")]
    [SerializeField] private Transform bottomTurningPoint;
    [SerializeField] private Transform upperTurningPoint;

    private Animator anim;
    private BoxCollider2D coll;
    private Quaternion originalRotation;

    private bool hit;
    private Transform player;
    private bool movingRight;
    private bool movingUp;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        originalRotation = transform.rotation;
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(9, 12, true);
        Physics2D.IgnoreLayerCollision(12, 12, true);

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

        if (movingUp) {
            transform.Translate(movementSpeed, 0, 0);
            if (transform.position.y > upperTurningPoint.position.y) {
                movingRight = true;
                movingUp = false;
                transform.Rotate(0, 0, -90);
            }
        } else if (movingRight) {
            transform.Translate(movementSpeed, 0, 0);
        } else {
            transform.Translate(movementSpeed, 0, 0);
            if (transform.position.x < bottomTurningPoint.position.x) {
                transform.Rotate(0, 0, -90);
                movingUp = true;
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
        movingRight = false;
        transform.rotation = originalRotation;
    }
}
