using UnityEngine;

namespace TPlus.StateMachine
{
    public abstract class HFSM_Base : MonoBehaviour
    {
        protected StateMachine_Base _currentStateMachine;
        protected float _tickSpeed = 0.1f;

        public virtual void Start()
        {
            InvokeRepeating(nameof(SMUpdate), _tickSpeed, _tickSpeed);
        }
        
        protected virtual void SMUpdate()
        {
            _currentStateMachine?.StateUpdate();
        }

        public void ChangeStateMachine(StateMachine_Base stateMachine)
        {
            _currentStateMachine?.OnStateExit();
            _currentStateMachine = stateMachine;
            _currentStateMachine?.OnStateEnter();
        }
    }
}

