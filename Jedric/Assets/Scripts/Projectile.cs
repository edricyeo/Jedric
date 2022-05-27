using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit)
            return;
        float bulletSpeed = speed * Time.deltaTime * direction;
        transform.Translate(bulletSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 3)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("collide");

        // damaging bosses
        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    public void SetDirection(float dir)
    {
        lifetime = 0;
        direction = dir;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
    }

    private void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
