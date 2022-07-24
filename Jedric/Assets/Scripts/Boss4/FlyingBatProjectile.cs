using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBatProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;
    private Transform player;
    private Vector3 playerPosition;
    private Vector3 movementVector;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(9, 12, true);
        Physics2D.IgnoreLayerCollision(12, 12, true);
        Physics2D.IgnoreLayerCollision(13, 12, true);
    }

    public void ActivateProjectile()
    {
        hit = false;
        gameObject.SetActive(true);
        coll.enabled = true;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movementVector = (player.position - transform.position).normalized * speed;
    }

    private void Update()
    {

        if (hit) 
            return;
            
        transform.position += movementVector * Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(1);

        hit = true;
        coll.enabled = false;
        gameObject.SetActive(false);
    }
}
