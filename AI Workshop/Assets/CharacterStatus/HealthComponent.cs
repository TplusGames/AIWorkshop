using UnityEngine;

[System.Serializable]
public class HealthComponent
{
    public delegate void Death();
    public event Death OnDeath;
    
    private int _currentHealth;
    private int _maxHealth;
    private bool _dead;
    private bool _canRevive;

    public void InitializeComponent(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_dead)
            return;
        
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
            _currentHealth = 0;
        }
    }

    public void Heal(int amount)
    {
        if (_dead)
            return;

        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void Revive(int amount)
    {
        if (!_dead && !_canRevive)
            return;

        _currentHealth = amount;
        _dead = false;
    }

    public void Die()
    {
        _dead = true;
        OnDeath?.Invoke();
    }

    public bool IsDead()
    {
        return _dead;
    }

    public float GetHealthPercentage()
    {
        return 1.0f * _currentHealth / _maxHealth;
    }

    public int GetDifferenceFromCurrentToMaxHealth()
    {
        return _maxHealth - _currentHealth;
    }

    public void SetCanRevive(bool canRevive)
    {
        _canRevive = canRevive;
    }
}
