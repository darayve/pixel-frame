using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : Enemy
{
    private void Update()
    {
        if (DetectPlayer(visionRange))
        {
            Attack();
        }
        else
        {
            state = EnemyState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

    public override void Attack()
    {
        state = EnemyState.attack;
    }
}
