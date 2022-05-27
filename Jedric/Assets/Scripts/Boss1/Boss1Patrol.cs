using UnityEngine;

public class Boss1Patrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Boss")]
    [SerializeField] private Transform boss;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initialScale;
    private bool movingLeft;

    private void Awake()
    {
        initialScale = boss.localScale;
    }

    private void MoveInDirection(int dir)
    {
        // Make boss face direction
        boss.localScale = new Vector3(-Mathf.Abs(initialScale.x) * dir, initialScale.y, initialScale.z);
        // Make boss move in that direction
        boss.position = new Vector3(boss.position.x + Time.deltaTime * dir * speed, boss.position.y, boss.position.z);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (boss.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                ChangeDirection();
            }
        } else
        {
            if (boss.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        movingLeft = !movingLeft;
    }
}
