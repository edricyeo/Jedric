using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    private PlayerMovement playerMove;
    private Animator anim;

    [Header("Player iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer spriteRend;

    public override void Awake()
    {
        base.Awake();
        spriteRend = GetComponent<SpriteRenderer>();
        playerMove = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if (base.currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            if (playerMove != null)
            {
                StartCoroutine(Invuln());
            }

        }
        else if (!base.dead)
        {
            anim.SetTrigger("die");
            playerMove.enabled = false;
            gameObject.SetActive(false);
            base.dead = true;
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
