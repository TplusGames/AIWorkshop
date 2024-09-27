namespace TPlus.StateMachine
{
    public abstract class State_Base
    {
        protected StateMachine_Base _stateMachine;

        public State_Base(StateMachine_Base stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void OnStateEnter();
        public abstract void StateUpdate();
        public abstract void OnStateExit();
    }
}

