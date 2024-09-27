using TPlus.AI;
using UnityEngine;

public abstract class State_AI_Combat : State_AI
{
    protected AISM_Combat _combatStateMachine;
    protected AI_Base _target;

    protected State_AI_Combat(AISM_Combat stateMachine) : base(stateMachine)
    {
        _combatStateMachine = stateMachine;
    }

    public override void StateUpdate()
    {
        CheckIfTargetIsInRange();
    }

    protected void CheckIfTargetIsInRange()
    {
        var distance = Vector3.Distance(GetPosition(), _target.transform.position);
        if (distance > _aiStateMachine.Info.Config.CombatVisionRange)
        {
            _combatStateMachine.OnTargetLost();
        }
    }

    public virtual void SetTarget(AI_Base target)
    {
        Debug.Log("Target set");
        _target = target;
    }
}
