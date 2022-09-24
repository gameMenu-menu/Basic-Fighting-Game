using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum Situation {Running, Idle, Attacking, Hit, Die, Laddering}
[System.Serializable]
public abstract class Human
{
    protected Movement CurrentMovement;
    string Trigger;
    protected Animator animator;

    public GameObject Self;

    public NavMeshAgent Agent;

    public LayerMask TargetLayer;

    public Situation CurrentSituation;

    protected RunMovement run = new RunMovement(); protected IdleMovement idle = new IdleMovement(); protected DieMovement die = new DieMovement(); protected HitMovement hit = new HitMovement();

    List<Movement> Attacks = new List<Movement>();

    protected int AttackIndex;

    protected Human target;
    
    int BaseDamage, Health, MaxHealth;

    int frame = 0;

    protected bool IsPlayer;

    public float BehaviourTime;
    

    public Human(GameObject self, LayerMask targetLayer)
    {
        Self = self;
        animator = Self.GetComponent<Animator>();
        Agent = Self.GetComponent<NavMeshAgent>();

        TargetLayer = targetLayer;
    }
    public virtual void OnStart()
    {
        Attacks.Add(new LeftPunchMovement());
        Attacks.Add(new KickMovement());
        Attacks.Add(new RightPunchMovement());
    }

    public virtual void OnUpdate()
    {
        if(CurrentSituation == Situation.Die || (!IsPlayer && target != null && target.CurrentSituation == Situation.Die) || BehaviourTime > Time.time)
        {
            return;
        }
        DefineMovement();
    }

    public virtual void OnLateUpdate()
    {
        StartAnimation();
    }

    protected void Animate(Movement movement)
    {
        if( movement == CurrentMovement && movement!=hit ) return;
        CurrentMovement = movement;
        movement.Triggered = false;

    }

    void StartAnimation()
    {
        if( CurrentMovement != null && !CurrentMovement.Triggered && !animator.IsInTransition(0))
        {
            animator.SetTrigger(CurrentMovement.ReturnTrigger());
            CurrentMovement.Triggered = true;
        }
    }

    protected virtual void DefineMovement()
    {
        
    }

    protected virtual void Move()
    {
        
    }

    protected void SmoothLookAt(Vector3 pos)
    {
        Vector3 dir = pos - Self.transform.position;
        Quaternion to = Quaternion.LookRotation(dir, Vector3.up);
        Self.transform.rotation = Quaternion.Lerp(Self.transform.rotation, to, 0.01f);
    }

    protected virtual bool LineOfSight()
    {
        return true;
    }

    protected void ChangeSituation(Situation situation)
    {
        CurrentSituation = situation;
    }

    public void Attack()
    {
        if(target != null)
        {
            target.GetDamage(Attacks[AttackIndex].ReturnDamage() + BaseDamage);
        }
    }

    protected void PerformAttack()
    {
        BehaviourTime = Time.time + 0.7f;
        IncreaseAttackIndex();
        SmoothLookAt(target.Self.transform.position);
        Animate(Attacks[AttackIndex]);
        CurrentSituation = Situation.Attacking;
    }

    void IncreaseAttackIndex()
    {
        AttackIndex++;

        if(AttackIndex>=Attacks.Count)
        {
            AttackIndex = 0;
        }
    }

    public void GetDamage(int damage)
    {
        if(CurrentSituation == Situation.Die) return;
        Health -= damage;

        if(Health <= 0)
        {
            SceneManager.Instance.StartCoroutine(DieRoutine());
        }
        else
        {
            if(IsPlayer && Random.Range(0, 2) == 0) return;
            
            Animate(hit);
            ChangeSituation(Situation.Hit);
        }
    }

    IEnumerator DieRoutine()
    {

        Animate(die);
        ChangeSituation(Situation.Die);

        yield return new WaitForSeconds(1.5f);

        if(IsPlayer)
        {
            SceneManager.Instance.controller.LevelFail();
        }
        else
        {
            SceneManager.Instance.controller.KillEnemy();
        }

        yield return new WaitForSeconds(4f);

        SceneManager.Instance.Pool.SendBack(Self);
    }

    public void Reset(int health, int damage)
    {
        CurrentMovement = null;
        CurrentSituation = Situation.Idle;
        Animate(idle);
        MaxHealth = health;
        Health = MaxHealth;
        BaseDamage = damage;
    }
}
