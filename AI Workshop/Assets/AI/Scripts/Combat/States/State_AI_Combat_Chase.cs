using TPlus.AI;
using UnityEngine;

public class State_AI_Combat_Chase : State_AI_Combat
{
    public State_AI_Combat_Chase(AISM_Combat stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateEnter()
    {
        GetAgent().speed = GetInfo().Config.runSpeed;
        GetAgent().SetDestination(_target.transform.position);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        var targetPos = _target.transform.position;
        GetAgent().SetDestination(targetPos);
        var distance = Vector3.Distance(GetPosition(), targetPos);

        if (distance <= GetInfo().Config.AttackRange)
        {
            _combatStateMachine.AttackState.SetTarget(_target);
            _combatStateMachine.ChangeState(_combatStateMachine.AttackState);
        }
    }

    public override void OnStateExit()
    {
        GetAgent().SetDestination(GetPosition());
    }
}
