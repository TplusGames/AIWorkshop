using TPlus.AI;
using UnityEngine;

public class State_AI_Combat_Attack : State_AI_Combat
{
    protected float _timer;
    
    public State_AI_Combat_Attack(AISM_Combat stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateEnter()
    {
        GetAgent().SetDestination(GetPosition());
        LookAtTarget();
        _timer = _aiStateMachine.Info.Config.AttackTime;
        _target.TakeDamage(1, GetInfo().AI);

        if (_target.IsDead())
        {
            _combatStateMachine.OnTargetLost();
        }
    }

    public override void StateUpdate()
    {
        _timer -= Time.deltaTime * 100f;

        if (_timer <= 0)
        {
            _combatStateMachine.ChaseState.SetTarget(_target);
            _combatStateMachine.ChangeState(_combatStateMachine.ChaseState);
            return;
        }
        
        base.StateUpdate();
    }

    public override void OnStateExit()
    {
        
    }

    private void LookAtTarget()
    {
        GetInfo().AI.transform.LookAt(_target.transform);
    }
}
