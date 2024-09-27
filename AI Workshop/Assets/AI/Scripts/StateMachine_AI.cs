using TPlus.StateMachine;
using UnityEngine.AI;

namespace TPlus.AI
{
    public abstract class StateMachine_AI : StateMachine_Base
    {
        public AISMInfo Info;
        protected StateMachine_AI(StateMachine_Base stateMachine, AISMInfo info) : base(stateMachine)
        {
            Info = info;
        }

        public void CloseStateMachine()
        {
            ChangeState(null);
        }
    }
    public class AISMInfo
    {
        public AI_Base AI;
        public VisionSensor VisionSensor;
        public NavMeshAgent Agent;
        public Inventory Inventory;
        public AIConfig Config;
    }
}



