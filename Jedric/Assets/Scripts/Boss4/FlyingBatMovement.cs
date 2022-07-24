using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBatMovement : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform LeftPatrolPoint;
    [SerializeField] private Transform RightPatrolPoint;

    [Header ("Bat Transform")]
    [SerializeField] private Transform BatTransform;

    [Header ("Movement parameters")]
    [SerializeField] private float speed;
    private Vector2 initScale;
    private bool movingLeft;

    private Animator anim;

    private void Awake() {
        initScale = BatTransform.localScale;
        anim = GetComponentInChildren<Animator>();
    }

    private void DirectionChangeX() {
        movingLeft = !movingLeft;
    }

    private void Patrol() {
        if (movingLeft) {
            if (BatTransform.position.x >= LeftPatrolPoint.position.x) {
                MoveInDirection(-1, 0, speed, 0);
            } else {
                DirectionChangeX();
            }
        } else {
            if (BatTransform.position.x <= RightPatrolPoint.position.x) {
                MoveInDirection(1, 0, speed, 0);
            } else {
                DirectionChangeX();
            }
        } 
    }

    private void Update() {
        Patrol();
    }

    private void MoveInDirection(int _xDirection, int _yDirection, float xSpeed, float ySpeed) {
        BatTransform.localScale = new Vector2(Mathf.Abs(initScale.x) * -_xDirection, initScale.y);
        BatTransform.position = new Vector2(BatTransform.position.x + Time.deltaTime * _xDirection * xSpeed, BatTransform.position.y + Time.deltaTime * _yDirection * ySpeed);
    }
}
