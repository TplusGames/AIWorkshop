using TPlus.StateMachine;

namespace TPlus.AI
{
    public abstract class State_AI_Idle : State_AI
    {
        protected AISM_Idle _aismIdle;
        
        protected State_AI_Idle(AISM_Idle stateMachine) : base(stateMachine)
        {
            _aismIdle = stateMachine;
        }
    }
}