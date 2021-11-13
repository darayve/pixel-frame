using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Transform castStart;
    public float visionRange = 4;

    public enum EnemyState { idle, running, attack }
    protected EnemyState state;

    public abstract void Attack();

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
