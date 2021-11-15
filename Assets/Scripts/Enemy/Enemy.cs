using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform castStart;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected EnemyState state;
    protected bool wasPlayerDetected = false;
    protected bool isSearchingForPlayer = false;

    public bool isFacingLeft;
    public float visionRange = 6;
    public float moveSpeed = 2;
    public enum EnemyState { idle, running, attack }

    public abstract void Attack();

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    public bool DetectPlayer(float distance)
    {
        bool val = false;

        Vector2 endPosition = castStart.position + Vector3.right * distance;
        RaycastHit2D hit = Physics2D.Linecast(castStart.position, endPosition, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag(Constants.PLAYER_TAG))
            {
                val = true;
            } else
            {
                val = false;
            }
        }

        return val;
    }
}
