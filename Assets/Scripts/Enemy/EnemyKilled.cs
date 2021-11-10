using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilled : MonoBehaviour
{
    // Private
    private Collider2D coll;

    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnDestroy()
    {
        coll.enabled = true;
    }
}