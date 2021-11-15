using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform castPosition;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float baseCastDistance;
    [SerializeField] private string facingDirection;

    private Rigidbody2D rb;
    private Vector3 baseScale;

    const string LEFT = "left";
    const string RIGHT = "right";

    private void Start()
    {
        baseScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float velocityX = moveSpeed;

        if (facingDirection == LEFT)
        {
            velocityX = -moveSpeed;
        }

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
        
        if (IsTouchingWall() || IsCloseToEdge())
        {
            ChangeWalkingDirection();
        }
    }

    private void ChangeWalkingDirection()
    {
        if (facingDirection == LEFT)
        {
            ChangeDirection(RIGHT);
        }
        else
        {
            ChangeDirection(LEFT);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.ENEMY_TAG))
        {
            ChangeWalkingDirection();
        }
    }

    private void ChangeDirection(string newDir)
    {
        Vector3 newScale = baseScale;
        if (facingDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        } else
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        facingDirection = newDir;
    }

    private bool IsTouchingWall()
    {
        bool value;
        float castDistance = baseCastDistance;

        if (facingDirection == LEFT)
        {
            castDistance = -baseCastDistance;
        }

        Vector3 targetPosition = castPosition.position;
        targetPosition.x += castDistance;

        Debug.DrawLine(castPosition.position, targetPosition, Color.blue);

        if (Physics2D.Linecast(castPosition.position, targetPosition, 1 << LayerMask.NameToLayer(Constants.GROUND_LAYER))) {
            value = true;
        } else
        {
            value = false;
        }

        return value;
    }

    private bool IsCloseToEdge()
    {
        bool value;
        float castDistance = baseCastDistance;

        Vector3 targetPosition = castPosition.position;
        targetPosition.y -= castDistance;

        Debug.DrawLine(castPosition.position, targetPosition, Color.yellow);
        
        if (Physics2D.Linecast(castPosition.position, targetPosition, 1 << LayerMask.NameToLayer(Constants.GROUND_LAYER)))
        {
            value = false;
        }
        else
        {
            value = true;
        }

        return value;
    }
}
