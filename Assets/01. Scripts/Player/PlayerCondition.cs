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
    public event Action OnDamaged;
    
    private HealthBar HealthBar
    {
        get { return GameManager.UICondition.healthBar; }
    }
    
    public void TakeDamage(int damage)
    {
        HealthBar.Subtract(damage);
        OnDamaged?.Invoke();
    }
}