using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float shootTimer;
    private float searchTimer;

    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        if (sentry.CanSeePlayer())
        {
            shootTimer += Time.deltaTime;
            sentry.transform.LookAt(sentry.player.transform);
            if (shootTimer > sentry.fireRate)
            {
                Shoot();
                shootTimer = 0;
            }
        }
        else
        {
            searchTimer += Time.deltaTime;
            if (searchTimer > 1)
            {
                searchTimer = 0;
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {

    }

    public void Shoot()
    {
        sentry.Shoot();
    }
}
