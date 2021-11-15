using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy
{
    private void Update()
    {
        if (DetectPlayer(visionRange))
        {
            wasPlayerDetected = true;
        }
        else
        {
            if (wasPlayerDetected)
            {
                if (!isSearchingForPlayer)
                {
                    isSearchingForPlayer = true;
                    Invoke("StopChasing", 5);
                }
            }
        }

        if (wasPlayerDetected)
        {
            Attack();
        }

        anim.SetInteger("state", (int)state);
    }

    public override void Attack()
    {
        ChasePlayer();
        state = EnemyState.running;
    }

    private void StopChasing()
    {
        StopChase();
        state = EnemyState.idle;
    }
}
