using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform topEdge;
    [SerializeField] private Transform bottomEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private int numCharges;
    private float cooldownTimer = -1;
    private int chargeCounter;
    private float distToRCorner;
    private float distToLCorner;
    private bool atNearestCorner = false;
    private bool finishedBouncing = false;
    private int bounceCount;

    [Header ("Movement parameters")]
    [SerializeField] private float chasingSpeed;
    [SerializeField] private float chargingSpeed;
    [SerializeField] private float bounceYSpeed;
    [SerializeField] private float bounceXSpeed;
    [SerializeField] private float attackCooldown;
    private Vector2 initScale;
    private bool movingLeft;
    private bool movingUp;

    public enum Attack {OneCharge, MultipleCharges, BouncingAttack}
    private int nextAttack;
    private Transform player;
    private Animator anim;

    private void Awake() {
        initScale = enemy.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
    }

    private void DirectionChangeX() {
        movingLeft = !movingLeft;
    }

    private void DirectionChangeY() {
        movingUp = !movingUp;
    }

    private void ChasePlayer() {
        if (movingLeft) {
            if (enemy.position.x >= player.position.x) {
                MoveInDirection(-1, 0, chasingSpeed, 0);
            } else {
                DirectionChangeX();
            }
        } else {
            if (enemy.position.x <= player.position.x) {
                MoveInDirection(1, 0, chasingSpeed, 0);
            } else {
                DirectionChangeX();
            }
        }
    }

    private void Charge() {
        if (movingLeft) {
            if (enemy.position.x >= leftEdge.position.x) {
                MoveInDirection(-1, 0, chargingSpeed, 0);
            } else {
                cooldownTimer = 0;
                nextAttack = Random.Range(0, 3);
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x) {
                MoveInDirection(1, 0, chargingSpeed, 0);
            } else {
                cooldownTimer = 0;
                nextAttack = Random.Range(0, 3);
            }
        } 
    }

    private void nCharges() {
        if (chargeCounter == numCharges) {
            cooldownTimer = 0;
            chargeCounter = 0;
            nextAttack = Random.Range(0, 3);
        }

        if (movingLeft) {
            if (enemy.position.x >= leftEdge.position.x) {
                MoveInDirection(-1, 0, chargingSpeed, 0);
            } else {
                chargeCounter += 1;
                cooldownTimer = attackCooldown - 0.2f;
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x) {
                MoveInDirection(1, 0, chargingSpeed, 0);
            } else {
                chargeCounter += 1;
                cooldownTimer = attackCooldown - 0.2f;
            }
        } 
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown) {
            if (nextAttack == (int) Attack.OneCharge) {
                Charge();
            } else if (nextAttack == (int) Attack.MultipleCharges) {
                nCharges();
            } else {
                BounceAttack();
            }
        } else {
            ChasePlayer();
        }
    }

    private void GoToNearestCorner() {
        distToLCorner = Mathf.Abs(leftEdge.position.x - enemy.position.x);
        distToRCorner =  Mathf.Abs(rightEdge.position.x - enemy.position.x);
        if (distToLCorner <= distToRCorner) {
            if (enemy.position.x >= leftEdge.position.x) {
                MoveInDirection(-1, 0, chargingSpeed, 0);
                movingLeft = false;
            } else {
                atNearestCorner = true;
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x) {
                MoveInDirection(1, 0, chargingSpeed, 0);
                movingLeft = true;
            } else {
                atNearestCorner = true;
            }
        }
    }

    private void Bounce() {
        if (movingLeft) {
            if (enemy.position.x <= leftEdge.position.x) {
                //MoveInDirection(1, 0, bounceXSpeed, bounceYSpeed);
                DirectionChangeX();
            }

            if (movingUp) {
                if (enemy.position.y <= topEdge.position.y) {
                    MoveInDirection(-1, 1, bounceXSpeed, bounceYSpeed);
                } else {
                    DirectionChangeY();
                    bounceCount += 1;
                }
            } else {
                if (enemy.position.y >= bottomEdge.position.y) {
                    MoveInDirection(-1, -1, bounceXSpeed, bounceYSpeed);
                } else {
                    DirectionChangeY();
                    bounceCount += 1;
                }
            }
        } else {
            if (enemy.position.x >= rightEdge.position.x) {
                //MoveInDirection(-1, 0, bounceXSpeed, bounceYSpeed);
                DirectionChangeX();
            }
            if (movingUp) {
                if (enemy.position.y <= topEdge.position.y) {
                    MoveInDirection(1, 1, bounceXSpeed, bounceYSpeed);
                } else {
                    DirectionChangeY();
                    bounceCount += 1;
                }
            } else {
                if (enemy.position.y >= bottomEdge.position.y) {
                    MoveInDirection(1, -1, bounceXSpeed, bounceYSpeed);
                } else {
                    DirectionChangeY();
                    bounceCount += 1;
                }
            }
        }

        if (bounceCount >= 7 && enemy.position.y <= bottomEdge.position.y)
        {
            finishedBouncing = true;
        }
    }

    private void BounceAttack() {
        if (!atNearestCorner) {
            GoToNearestCorner();
        } else if (!finishedBouncing) {
            Bounce();
        } else {
            atNearestCorner = false;
            finishedBouncing = false;
            bounceCount = 0;
            cooldownTimer = 0;
            nextAttack = Random.Range(0, 3);
        }
    }

    private void MoveInDirection(int _xDirection, int _yDirection, float xSpeed, float ySpeed) {
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * -_xDirection, initScale.y);
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * _xDirection * xSpeed, enemy.position.y + Time.deltaTime * _yDirection * ySpeed);
    }
}
