using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float wallClimbSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (UIManager.Instance.IsPauseOrVictoryScreenActive())
            return;

        horizontalInput = Input.GetAxis("Horizontal");

        //flip character while moving
        if (horizontalInput > 0.01f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalInput < -0.01f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //Animations
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall jump
        if (wallJumpCooldown > 0.2f)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            if (onWall() && !isGrounded())
            {
                if (horizontalInput != 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallClimbSpeed);
                }
                rb.gravityScale = 0;
            }
            else
                rb.gravityScale = 5.5f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!onWall() || horizontalInput != 0)
                    Jump();
                if (isGrounded())
                    SoundManager.instance.PlaySound(jumpSound);
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            SoundManager.instance.PlaySound(jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            int direction = transform.localScale.x > 0 ? -1 : 1;

            if (horizontalInput == 0)
            {
                rb.velocity = new Vector2(direction * 10, jumpPower);
                transform.localScale = new Vector3(-direction, transform.localScale.y, transform.localScale.z);
            }
            else
                rb.velocity = new Vector2(direction * 3, 6);

            wallJumpCooldown = 0;

        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);
        return raycastHitLeft.collider != null || raycastHitRight.collider != null;
    }
}
