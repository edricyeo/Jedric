using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMove;

    [SerializeField] private float dashMultiplier;
    [SerializeField] private bool dashEnabled = false;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashDurationTimer;
    private float dashCdTimer;
    public bool isDashing = false;

    public ParticleSystem dust;
    [SerializeField] private AudioClip dashSound;

    private void Start()
    {
        Health.BossDeathEvent += ToggleDash;
    }

    void Update()
    {
        if (dashEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (dashCdTimer <= 0 && dashDurationTimer <= 0)
                {
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

    public void ToggleDash()
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
}
