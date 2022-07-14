using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] public float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityVal;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("Player Dash")]
    [SerializeField] private PlayerDash playerDash;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("Double Jump")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        // Grab reference for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // flip player when moving left-right: +ve when facing left, -ve when facing right
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (OnWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else 
        {
            body.gravityScale = gravityVal;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (IsGrounded())
            {
                jumpCounter = extraJumps;
            } 
        }

        wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {

        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else if (!OnWall() && jumpCounter <= 0)
            return;
        else if (OnWall() && wallJumpCooldown > 0.2f)
            WallJump();
        else
        {
            // double jump logic
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            jumpCounter--;
        }
        SoundManager.instance.PlaySound(jumpSound);
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
    }

    private bool OnWall()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-transform.localScale.x, 0), 0.1f, wallLayer);
    }
}
