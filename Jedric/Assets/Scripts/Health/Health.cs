using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health Parameters")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    [SerializeField] private Healthbar healthbar;

    private Animator anim;
    private PlayerMovement playerMove;
    private Boss1 boss1;

    // to make sure die animation doesnt play twice
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerMove = GetComponent<PlayerMovement>();
        boss1 = GetComponent<Boss1>();

    }

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, startingHealth);
        healthbar.ChangeHealth();
        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            if (playerMove != null)
            {
                StartCoroutine(Invuln());
            }
        }
        else
        {
            //player dead
            if (!dead)
            {
                anim.SetTrigger("die");
                if (playerMove != null)
                {
                    //disable player movement when dead
                    playerMove.enabled = false;
                    Destroy(gameObject);
                }
                if (boss1 != null)
                {
                    Destroy(gameObject);
                }
                dead = true;
            }
        }
    }


    private IEnumerator Invuln()
    {
        //ignore collision between layer 8 (player) and 9 (enemy)
        Physics2D.IgnoreLayerCollision(8, 9, true);
        //invunerability duration
        for (int i = 0; i < numOfFlashes; i++)
        {
            // make the sprite flash red and make it slightly transparent
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
