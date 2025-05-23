using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public float Health { get; private set; } = 100f;
    public float MaxHealth { get; private set; } = 100f;
    public float CurrentHealth { get; private set; }
    
    public float Stamina { get; private set; } = 20f;
    public float MaxStamina { get; private set; } = 20f;
    public float CurrentStamina { get; private set; }
    
    public event Action OnDamaged;

    private void Start()
    {
        CurrentHealth = Health;
        CurrentStamina = Stamina;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        OnDamaged?.Invoke();
    }
    
    public void Heal(float value)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + value, MaxHealth);
    }
}