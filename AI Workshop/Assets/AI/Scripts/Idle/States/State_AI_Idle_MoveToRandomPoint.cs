using UnityEngine;

namespace TPlus.AI
{
    public class State_AI_Idle_MoveToRandomPoint : State_AI_Idle
    {
        private float _timer;
        private Vector3 _destination;
        
        public State_AI_Idle_MoveToRandomPoint(AISM_Idle stateMachine) : base(stateMachine)
        {
        }

        public override void OnStateEnter()
        {
            GetAgent().speed = GetInfo().Config.walkSpeed;
            Debug.Log("Moving to random point");
            MoveToRandomPoint();
            _timer = 20f;
        }

        public override void StateUpdate()
        {
            _aismIdle.Info.VisionSensor.PerformVisionScan();
            
            _timer -= Time.deltaTime * 100f;
            var distance = Vector3.Distance(_aismIdle.Info.AI.transform.position, _destination);
            if (distance < 0.1f || _timer <= 0)
            {
                _aismIdle.ChangeState(_aismIdle.WaitState);
            }
        }

        public override void OnStateExit()
        {
            
        }

        private void MoveToRandomPoint()
        {
            var randomX = Random.Range(-10, 10);
            var randomY = Random.Range(-10, 10);
            var offset = new Vector3(randomX, 0, randomY);
            _destination = _aismIdle.Info.AI.transform.position + offset;
            _aismIdle.Info.Agent.SetDestination(_destination);
        }
    }
}