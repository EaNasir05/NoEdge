using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float shootTimer;
    private float searchTimer;

    public override void Enter()
    {
        SoundFXManager.instance.PlaySoundFXClip(sentry.GetTargetAcquiredAudioClip(), sentry.transform, 0.5f);
    }

    public override void Perform()
    {
        if (sentry.CanSeePlayer())
        {
            shootTimer += Time.deltaTime;
            sentry.transform.LookAt(sentry.GetPlayer().transform);
            if (shootTimer > sentry.GetFireRate())
            {
                Shoot();
                shootTimer = 0;
            }
        }
        else
        {
            searchTimer += Time.deltaTime;
            if (searchTimer > 3)
            {
                searchTimer = 0;
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {
        SoundFXManager.instance.PlaySoundFXClip(sentry.GetResetAudioClip(), sentry.transform, 1f);
    }

    public void Shoot()
    {
        sentry.Shoot();
    }
}
