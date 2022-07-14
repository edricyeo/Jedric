using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float lifetime;
    private bool hit;
    private float direction;

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
        if(lifetime > 1)
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
        Debug.Log(collision.tag);
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<BossHealth>().TakeDamage(1);
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
