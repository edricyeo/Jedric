using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        // Grab reference for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        // flip player when moving left-right: +ve when facing left, -ve when facing right
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // wall jump logic
        if (wallJumpCooldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 1;

            if (Input.GetKey(KeyCode.UpArrow))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {

            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 6, 3);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            wallJumpCooldown = 0;

        }
        
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
    }

    private bool onWall()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-transform.localScale.x, 0), 0.1f, wallLayer);
    }

    public bool canAttack()
    {
        return isGrounded() && !onWall();
    }
}