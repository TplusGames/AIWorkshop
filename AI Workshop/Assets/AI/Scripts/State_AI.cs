using TPlus.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace TPlus.AI
{
    public abstract class State_AI : State_Base
    {
        protected StateMachine_AI _aiStateMachine;
        protected State_AI(StateMachine_AI stateMachine) : base(stateMachine)
        {
            _aiStateMachine = stateMachine;
        }

        protected Vector3 GetPosition()
        {
            return _aiStateMachine.Info.AI.transform.position;
        }

        protected AISMInfo GetInfo()
        {
            return _aiStateMachine.Info;
        }

        protected NavMeshAgent GetAgent()
        {
            return _aiStateMachine.Info.Agent;
        }
    }
}
