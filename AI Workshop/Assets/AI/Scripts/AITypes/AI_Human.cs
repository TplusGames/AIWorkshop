using TPlus.AI;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class AI_Human : AI_Base
{
    [SerializeField] protected SkinnedMeshRenderer meshRenderer;
    [SerializeField] protected Material zombieMaterial;
    [SerializeField] protected AIConfig zombieConfig;
    [SerializeField] protected AnimatorController zombieAnimationController;
    
    protected bool _infected;
    protected float _infectionTimer;
    protected float _turnTimer;
    protected bool _turned;

    protected override void SMUpdate()
    {
        if (IsDead() && _infected)
        {
            _turnTimer -= Time.deltaTime * 100f;

            if (_turnTimer <= 0 && !_turned)
            {
                _turned = true;
                TurnToZombie();
            }
        }
        else if (IsDead()) return;
        
        base.SMUpdate();

        if (_infected)
        {
            _infectionTimer -= Time.deltaTime * 100f;
            if (_infectionTimer <= 0 && !IsDead())
            {
                _characterStatus.HealthComponent.Die();
            }
        }
    }
    
    public override bool IsHostile(AI_Base ai)
    {
        if (ai is AI_Human human)
        {
            return false;
        }

        return true;
    }

    public override void TakeDamage(int damage, MonoBehaviour attacker)
    {
        base.TakeDamage(damage, attacker);
        if (attacker is AI_Zombie zombie && !_infected)
        {
            _infected = true;
            _infectionTimer = 120;
        }
    }

    public void CureInfection()
    {
        _infected = false;
    }

    protected override void OnDeath()
    {
        if (_infected)
        {
            BeginTurnToZombieCountdown();
        }
        base.OnDeath();
    }

    private void BeginTurnToZombieCountdown()
    {
        _turnTimer = 30;
    }

    private void TurnToZombie()
    {
        UnregisterAI();
        _infected = false;
        
        if (gameObject.GetComponent<AI_Zombie>() != null)
        {
            Debug.LogWarning("AI_Zombie component already exists, not adding again.");
            return;
        }

        try
        {
            var newAgent = gameObject.AddComponent<NavMeshAgent>();
            newAgent.baseOffset = 1;
            _aiAnimation.SetAgent(newAgent);
            // Add the AI_Zombie component
            var zombieAI = gameObject.AddComponent<AI_Zombie>();
            Debug.Log("Successfully added AI_Zombie component.");
            zombieAI.AISettings = AISettings;
            zombieAI.Config = zombieConfig;

            // Register the new zombie AI
            zombieAI.RegisterAI();

            // Manually call Start on the zombie AI to ensure it initializes properly
            zombieAI.Start();

            Debug.Log("AI_Human successfully turned into AI_Zombie.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error while adding AI_Zombie: " + ex.Message);
        }

        meshRenderer.material = zombieMaterial;
        _ragDollToggle.ToggleRagdoll(false);
        _aiAnimation.SetAnimationController(zombieAnimationController);
        _aiAnimation.Revive();
        
        // Attempt to destroy this AI_Human component
        Debug.Log("Destroying AI_Human component...");
        Destroy(this);
    }
}
