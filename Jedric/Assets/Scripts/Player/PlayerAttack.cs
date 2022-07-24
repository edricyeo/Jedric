using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private AudioClip bulletSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown)
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(bulletSound);
        anim.SetTrigger("attack");
        // pooling bullets for better performance
        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<PlayerProjectile>().SetDirection(-Mathf.Sign(transform.localScale.x));
    }

    private int FindBullet()
    {
        for(int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}
