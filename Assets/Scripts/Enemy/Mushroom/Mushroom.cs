using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public override void Attack()
    {
        state = EnemyState.running;
    }
}
