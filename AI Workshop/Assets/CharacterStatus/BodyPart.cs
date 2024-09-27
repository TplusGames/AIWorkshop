using TPlus.AI;
using UnityEngine;

public class BodyPart : MonoBehaviour, ITakeDamage, IAIAttachment
{
    public delegate void BodyPartCrippled();
    public event BodyPartCrippled OnBodyPartCrippled;
    
    [SerializeField] private AI_Base ai;
    [SerializeField] private float damageMultiplier = 1;
    [SerializeField] private int bodyPartMaxHealth = 15;

    private HealthComponent _bodyPartHealthComponent;

    private void Start()
    {
        _bodyPartHealthComponent = new HealthComponent();
        _bodyPartHealthComponent.InitializeComponent(bodyPartMaxHealth);
        _bodyPartHealthComponent.SetCanRevive(true);
        _bodyPartHealthComponent.OnDeath += Die;
    }

    private void OnDisable()
    {
        _bodyPartHealthComponent.OnDeath -= Die;
    }
    
    public void TakeDamage(int damage, MonoBehaviour attacker)
    {
        _bodyPartHealthComponent.TakeDamage(damage);
        var appliedDamage = Mathf.RoundToInt(damage * damageMultiplier);
        ai.TakeDamage(appliedDamage, attacker);
    }

    public void Heal(int amount, MonoBehaviour healer)
    {
        var difference = _bodyPartHealthComponent.GetDifferenceFromCurrentToMaxHealth();

        if (_bodyPartHealthComponent.IsDead())
        {
            _bodyPartHealthComponent.Revive(amount);
        }
        else
        {
            _bodyPartHealthComponent.Heal(amount);
        }

        var leftOvers = amount - difference;
        if (leftOvers > 0)
        {
            ai.Heal(leftOvers, healer);
        }
    }

    public void Die()
    {
        OnBodyPartCrippled?.Invoke();
        Debug.Log($"{gameObject.name} has been crippled");
    }

    public AI_Base GetAttachedAI()
    {
        return ai;
    }
}
