using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // SerializeFields
    [SerializeField] float jumpForce = 14f;
    [SerializeField] float moveSpeed = 7f;
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


    private enum MovementState { idle, running, jumping, falling, doubleJumping };
        
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

        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsGrounded())
        {
            doubleJumped = false;
        }

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

        if (!wasOnGround && IsGrounded())
        {
            psDusts[2].gameObject.SetActive(true);
            psDusts[2].Stop();
            psDusts[2].Play();
        }

        wasOnGround = IsGrounded();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        
        // Running animation
        if (directionX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
            psDusts[0].Play();
        }
        else if (directionX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
            psDusts[0].Play();
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .01f) // Imprecise value, can be changed to .001f for example
        {
            state = MovementState.jumping;
            if (doubleJumped) state = MovementState.doubleJumping;
        } else if (rb.velocity.y < -.01f)
        {
            psDusts[0].Stop();
            psDusts[1].Stop();
            state = MovementState.falling;
            if (rb.velocity.y > .01f) state = MovementState.doubleJumping;
            else if (rb.velocity.y < -.01f) state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
