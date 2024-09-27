using TPlus.StateMachine;
using TPlus.Status;
using UnityEngine;
using UnityEngine.AI;

namespace TPlus.AI
{
    [RequireComponent(typeof(CharacterStatus))]
    [RequireComponent(typeof(RagDollToggle))]
    [RequireComponent(typeof(VisionSensor))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(AIAnimation))]
    public class AI_Base : HFSM_Base
    {
        public AISettings AISettings;
        public AIConfig Config;
        public AISM_Idle IdleStateMachine { get; private set; }
        public AISM_Combat CombatStateMachine { get; private set; }

        protected NavMeshAgent _agent;
        protected VisionSensor _visionSensor;
        protected Inventory _inventory;
        protected CharacterStatus _characterStatus;
        protected RagDollToggle _ragDollToggle;
        protected AIAnimation _aiAnimation;

        public override void Start()
        {
            RegisterAI();
            SetTickSpeed();
            InitializeReferences();
            InitializeStateMachines();
            base.Start();
        }
        
        public virtual void InitializeReferences()
        {
            _agent = GetComponent<NavMeshAgent>();
            _visionSensor = GetComponent<VisionSensor>();
            _visionSensor.InitializeVisionSensor(this);
            _inventory = GetComponent<Inventory>();
            _ragDollToggle = GetComponent<RagDollToggle>();
            _ragDollToggle.ToggleRagdoll(false);
            _aiAnimation = GetComponent<AIAnimation>();
            _aiAnimation.SetAgent(_agent);
            _characterStatus = GetComponent<CharacterStatus>();
            _characterStatus.InitializeCharacterStatus();
            _characterStatus.HealthComponent.OnDeath += OnDeath;
        }
        
        protected virtual void InitializeStateMachines()
        {
            var info = new AISMInfo()
            {
                AI = this,
                Agent = _agent,
                VisionSensor = _visionSensor,
                Inventory = _inventory,
                Config = Config
            };
            IdleStateMachine = new AISM_Idle(null, info);
            CombatStateMachine = new AISM_Combat(null, info);
            ChangeStateMachine(IdleStateMachine);
        }

        protected virtual void OnDisable()
        {
            UnregisterAI();
            _characterStatus.HealthComponent.OnDeath -= OnDeath;
        }

        protected override void SMUpdate()
        {
            if (_characterStatus.HealthComponent.IsDead())
                return;
            _aiAnimation.SynchAnimationsToVelocity();
            base.SMUpdate();
        }

        public void RegisterAI()
        {
            DetectableObjectManager.Instance.RegisterDetectableObject(this);
        }

        protected void UnregisterAI()
        {
            DetectableObjectManager.Instance.UnregisterDetectableObject(this);
        }

        private void SetTickSpeed()
        {
            _tickSpeed = AISettings.TickSpeed;
        }

        public virtual bool IsHostile(AI_Base ai)
        {
            return false; 
        }

        public virtual void TakeDamage(int damage, MonoBehaviour attacker)
        {
            if (_characterStatus.HealthComponent.IsDead())
                return;
            
            _characterStatus.HealthComponent.TakeDamage(damage);
        }

        public virtual void Heal(int amount, MonoBehaviour healer)
        {
            if (_characterStatus.HealthComponent.IsDead())
                return;
            
            _characterStatus.HealthComponent.Heal(amount);
        }

        protected virtual void OnDeath()
        {
            _currentStateMachine.ChangeState(null);
            ChangeStateMachine(null);
            Debug.Log(gameObject.name + " has died");
            TEST_ShowDeath();
        }

        private void TEST_ShowDeath()
        {
            Destroy(_agent);
            _aiAnimation.Die();
            _ragDollToggle.ToggleRagdoll(true);
        }

        public bool IsDead()
        {
            return _characterStatus.HealthComponent.IsDead();
        }
    }
}

