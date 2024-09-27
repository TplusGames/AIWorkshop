using UnityEngine;

namespace TPlus.AI
{
    public class State_AI_Idle_Wait : State_AI_Idle
    {
        private float _timer;
        
        public State_AI_Idle_Wait(AISM_Idle stateMachine) : base(stateMachine)
        {
        }

        public override void OnStateEnter()
        {
            Debug.Log("Entered wait state");
            _aismIdle.Info.Agent.SetDestination(_aismIdle.Info.AI.transform.position);
            _timer = Random.Range(5, 10);
        }

        public override void StateUpdate()
        {
            _aismIdle.Info.VisionSensor.PerformVisionScan();
            
            _timer -= Time.deltaTime * 100f;

            if (_timer <= 0)
            {
                _aismIdle.ChangeState(_aismIdle.MoveToRandomPointState);
            }
        }

        public override void OnStateExit()
        {
            
        }
    }
}