using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMove;

    [SerializeField] private float dashMultiplier;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private bool dashEnabled = false;
    [SerializeField] private bool invulnDash = false;
    private float dashDurationTimer;
    private float dashCdTimer;
    public bool isDashing = false;

    public ParticleSystem dust;
    [SerializeField] private AudioClip dashSound;

    void Update()
    {
        if (dashEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (dashCdTimer <= 0 && dashDurationTimer <= 0)
                {
                    if (invulnDash)
                    {
                        StartCoroutine(Invuln());
                    }
                    CreateDust();
                    SoundManager.instance.PlaySound(dashSound);
                    playerMove.speed *= dashMultiplier;
                    isDashing = true;
                    dashDurationTimer = dashDuration;
                    
                }
            }

            if (dashDurationTimer > 0)
            {
                // player is dashing 
                dashDurationTimer -= Time.deltaTime;

                if (dashDurationTimer <= 0)
                {
                    // player stops dashing, set move speed back to normal
                    float originalSpeed = playerMove.speed /= dashMultiplier;
                    playerMove.speed = originalSpeed;
                    dashCdTimer = dashCooldown;
                    isDashing = false;
                }
            }
            
            if (dashCdTimer > 0)
            {
                // dash is on cooldown before it can be used again
                dashCdTimer -= Time.deltaTime;
            }
        }
    }

    public void EnableDash()
    {
        if (dashEnabled == false)
        {
            dashEnabled = true;
        }
    }

    public void CreateDust()
    {
        dust.Play();
    }

    private IEnumerator Invuln()
    {
        //ignore collision between player, enemy, enemy projectile
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 12, true);
        yield return new WaitForSeconds(dashDuration + 0.3f);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 12, false);
    }

    public void EnableInvulnDash()
    {
        invulnDash = true;
    }
}
