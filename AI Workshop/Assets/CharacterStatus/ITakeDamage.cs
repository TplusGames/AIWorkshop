using UnityEngine;

public interface ITakeDamage
{
    public abstract void TakeDamage(int damage, MonoBehaviour attacker);
    public abstract void Heal(int amount, MonoBehaviour healer);
    public abstract void Die();
}
