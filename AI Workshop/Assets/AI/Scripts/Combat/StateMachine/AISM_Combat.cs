using TPlus.StateMachine;

namespace TPlus.AI
{
    public class AISM_Combat : StateMachine_AI
    {
        protected State_AI_Combat _combatState;
        protected AI_Base _target;
        public State_AI_Combat_Chase ChaseState { get; private set; }
        public State_AI_Combat_Attack AttackState { get; private set; }
        
        public AISM_Combat(StateMachine_Base stateMachine, AISMInfo info) : base(stateMachine, info)
        {
            ChaseState = new State_AI_Combat_Chase(this);
            AttackState = new State_AI_Combat_Attack(this);
        }

        public override void OnStateEnter()
        {
            ChaseState.SetTarget(_target);
            ChangeState(ChaseState);
        }

        public override void OnStateExit()
        {
            ChangeState(null);
        }

        public void OnTargetLost()
        {
            Info.AI.ChangeStateMachine(Info.AI.IdleStateMachine);
        }

        public void SetTarget(AI_Base target)
        {
            _target = target;
        }
    }
}

