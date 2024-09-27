using TPlus.StateMachine;
using UnityEngine;

namespace TPlus.AI
{
    public class AISM_Idle : StateMachine_AI
    {
        public State_AI_Idle_Wait WaitState {get; private set;}
        public State_AI_Idle_MoveToRandomPoint MoveToRandomPointState {get; private set;}
        
        public AISM_Idle(StateMachine_Base stateMachine, AISMInfo info) : base(stateMachine, info)
        {
            WaitState = new State_AI_Idle_Wait(this);
            MoveToRandomPointState = new State_AI_Idle_MoveToRandomPoint(this);
        }

        public override void OnStateEnter()
        {
            Info.VisionSensor.OnEnemySpotted += OnHostileScene;
            ChangeState(WaitState);
        }

        public override void OnStateExit()
        {
            Info.VisionSensor.OnEnemySpotted -= OnHostileScene;
        }

        public virtual void OnHostileScene(AI_Base hostile)
        {
            Info.AI.CombatStateMachine.SetTarget(hostile);
            Info.AI.ChangeStateMachine(Info.AI.CombatStateMachine);
        }
    }
}