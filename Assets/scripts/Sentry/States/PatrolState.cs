using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        sentry.transform.LookAt(sentry.defaultDirection.transform);
        if (sentry.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {

    }
}
