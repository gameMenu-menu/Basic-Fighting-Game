using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Human
{
    float AttackDistance, SightDistance;
    public Enemy(GameObject self, LayerMask targetLayer)
    :base(self, targetLayer)
    {
        IsPlayer = false;
    }

    public override void OnStart()
    {
        base.OnStart();

        target = SceneManager.Instance.cameraController.Target.GetComponent<HumanController>().character;
        

        AttackDistance = 1f;
        SightDistance = 40f;
    }

    protected override void DefineMovement()
    {
        base.DefineMovement();

        if(target == null)
        {
            target = SceneManager.Instance.cameraController.Target.GetComponent<HumanController>().character;
        }

        float distance = (target.Self.transform.position - Self.transform.position).sqrMagnitude;



        if(distance > SightDistance)
        {
            ChangeSituation(Situation.Idle);
            Animate(idle);
            Agent.SetDestination(Self.transform.position);
        }
        else if(distance > AttackDistance)
        {
            Move();
        }
        else
        {

            Agent.SetDestination(Self.transform.position);

            if(CurrentSituation == Situation.Idle)
            {
                PerformAttack();
            }
            else if( ((CurrentSituation == Situation.Attacking || CurrentSituation == Situation.Hit) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && !animator.IsInTransition(0)) || CurrentSituation == Situation.Running )
            {
                ChangeSituation(Situation.Idle);
                Animate(idle);
            }

            SmoothLookAt(target.Self.transform.position);
        }
    }

    protected override void Move()
    {
        base.Move();

        Agent.SetDestination(target.Self.transform.position);
        
        ChangeSituation(Situation.Running);
        Animate(run);
    }

}
