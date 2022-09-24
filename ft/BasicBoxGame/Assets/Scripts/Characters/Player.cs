using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    public Player(GameObject self, LayerMask targetLayer)
    :base(self, targetLayer)
    {
        IsPlayer = true;
    }

    protected override void DefineMovement()
    {
        base.DefineMovement();

        if(SceneManager.Instance.joistick.ResultExists && (CurrentSituation == Situation.Idle || CurrentSituation == Situation.Running) )
        {
            if(LineOfSight())
            {
                PerformAttack();
            }
            else
            {
                Move();
            }
        }
        else
        {
            if( ((CurrentSituation == Situation.Attacking || CurrentSituation == Situation.Hit) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && !animator.IsInTransition(0)) || CurrentSituation == Situation.Running )
            {
                ChangeSituation(Situation.Idle);
                Animate(idle);
            }
        }

        Vector3 lookVec = - SceneManager.Instance.joistick.ResultDirection;

        lookVec.z = lookVec.y;
        lookVec.y = 0;

        if(lookVec.sqrMagnitude < 0.1f) return;

        lookVec = Self.transform.position + lookVec;

        if(lookVec != Vector3.zero) SmoothLookAt(lookVec);
    }

    protected override bool LineOfSight()
    {
        Collider[] colls = Physics.OverlapBox(Self.transform.position + Self.transform.forward/2f, Vector3.one/2f, Quaternion.identity, TargetLayer);
        {
            for(int i=0; i<colls.Length; i++)
            {
                Vector3 dir = colls[i].transform.position - Self.transform.position;
                //float dot = Vector3.Dot(Self.transform.forward, dir);
                Vector3 resultDir = -SceneManager.Instance.joistick.ResultDirection;
                resultDir.z = resultDir.y;
                resultDir.y = 0;
                dir.y = 0;
                float dot = Vector3.Dot(Self.transform.forward, resultDir);
                if(dot>0.5f)
                {
                    if(colls[i].GetComponent<HumanController>().character.CurrentSituation != Situation.Die)
                    {
                        target = colls[i].GetComponent<HumanController>().character;
                        return true;
                    }
                    
                }
            }
        }
        return false;
    }

    protected override void Move()
    {
        base.Move();

        Animate(run);

        Vector3 moveVec = -SceneManager.Instance.joistick.ResultDirection / 150f;

        moveVec.z = moveVec.y;

        moveVec.y = 0;

        
        
        Self.transform.position += moveVec;
        
        ChangeSituation(Situation.Running);
    }
}
