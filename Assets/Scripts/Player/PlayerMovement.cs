using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // SerializeFields
    [SerializeField] float jumpForce = 14f;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float bounceBackForce = 10f;
    [SerializeField] float hurtForce = 10f;
    [SerializeField] LayerMask jumpableGround;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] ParticleSystem[] psDusts;

    // Private
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float directionX = 0f;
    private bool doubleJumped;
    private bool wasOnGround;
    private static bool _isPlayerFalling;
    private enum MovementState { idle, running, jumping, falling, doubleJumping, hit }
    private MovementState state = MovementState.idle;

    // Public
    public static bool IsPlayerFalling
    {
        get => _isPlayerFalling;
        set => _isPlayerFalling = value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        if (state != MovementState.hit)
        {
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsGrounded())
        {
            doubleJumped = false;
        }

        if (state != MovementState.hit)
        {
            Movement();
        }

        if (!wasOnGround && IsGrounded())
        {
            psDusts[2].gameObject.SetActive(true);
            psDusts[2].Stop();
            psDusts[2].Play();
        }

        wasOnGround = IsGrounded();
        AnimationState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.ENEMY_TAG))
        {
            if (state == MovementState.falling)
            {
                _isPlayerFalling = true;
                collision.gameObject.GetComponent<Animator>().SetTrigger("eliminated");
                rb.velocity = new Vector2(rb.velocity.x, 14f);
                Destroy(collision.gameObject, collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - .12f);
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
            } else
            {
                _isPlayerFalling = false;
                HandleHit(collision, hurtForce, rb.velocity.y);
            }
        }

        if (collision.gameObject.CompareTag(Constants.TRAP_TAG))
        {
            HandleHit(collision, hurtForce, rb.velocity.y * bounceBackForce);
        }
    }

    private void HandleHit(Collision2D collision, float xForce, float yForce)
    {
        state = MovementState.hit;
        if (collision.gameObject.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(-xForce, yForce);
        } else
        {
            rb.velocity = new Vector2(xForce, yForce);
        }
    }

    private void Movement()
    {
        // Single jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            psDusts[0].Stop();
            psDusts[1].Play();
            SoundManager.Instance.PlaySound(jumpSFX);
        }

        // Double jump
        if (Input.GetButtonDown("Jump") && !doubleJumped && !IsGrounded())
        {
            psDusts[0].Stop();
            psDusts[1].Stop();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            doubleJumped = true;
            SoundManager.Instance.PlaySound(jumpSFX);
        }

        // Allows short and long jumps
        if (Input.GetButtonUp("Jump") && rb.velocity.y > .1f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    private void AnimationState()
    {
        // JUMP
        if (rb.velocity.y > .01f)
        {
            state = MovementState.jumping;
            if (doubleJumped)
            {
                state = MovementState.doubleJumping;
            }
        }
        else if (rb.velocity.y < -.01f) // FALLING
        {
            psDusts[0].Stop();
            psDusts[1].Stop();
            state = MovementState.falling;
            if (rb.velocity.y > .01f)
            {
                state = MovementState.doubleJumping;
            }
            else if (rb.velocity.y < -.01f)
            {
                state = MovementState.falling;
                if (IsGrounded())
                {
                    state = MovementState.idle;
                }
            }
        } else if (state == MovementState.hit)
        {
            if (Mathf.Abs(rb.velocity.x) < .01f)
            {
                state = MovementState.idle;
            }
        } else if (directionX > 0f) // RUNNING
        {
            state = MovementState.running;
            sprite.flipX = false;
            psDusts[0].Play();
        }
        else if (directionX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
            psDusts[0].Play();
        }
        else
        {
            state = MovementState.idle;
        }

        
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
