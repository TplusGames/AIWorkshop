namespace TPlus.StateMachine
{
    public abstract class StateMachine_Base : State_Base
    {
        protected State_Base _currentState;

        protected StateMachine_Base(StateMachine_Base stateMachine) : base(stateMachine)
        {
        }

        public override void StateUpdate()
        {
            _currentState?.StateUpdate();
        }

        public virtual void ChangeState(State_Base state)
        {
            _currentState?.OnStateExit();
            _currentState = state;
            _currentState?.OnStateEnter();
        }
    }
}

