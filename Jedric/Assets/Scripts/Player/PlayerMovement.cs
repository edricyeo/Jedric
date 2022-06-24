using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private AudioClip jumpSound;
    public static PlayerMovement instance;

    private void Awake()
    {
        // Grab reference for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            instance.transform.position = this.transform.position;
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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
        anim.SetBool("grounded", IsGrounded());

        // wall jump logic
        if (wallJumpCooldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else if (playerDash.isDashing == true)
            {
                body.gravityScale = 0;
                body.mass = 0;
            } else
            {
                body.gravityScale = 1;
                body.mass = 1;
            }
            

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Jump();
                if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
                {
                    SoundManager.instance.PlaySound(jumpSound);
                }
            }
                
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (OnWall() && !IsGrounded())
        {

            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * speed, 3);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * speed, 6);
            }

            wallJumpCooldown = 0;

        }

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
    }

    private bool OnWall()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-transform.localScale.x, 0), 0.1f, wallLayer);
    }

    public bool CanAttack()
    {
        return IsGrounded() && !OnWall();
    }
}
