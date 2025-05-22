using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBall : MonoBehaviour
{
    private int _damage = 3;
    private float _damageRate = 2f;
    
    private List<IDamageable> _things = new List<IDamageable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0 , _damageRate);
    }
    
    private void DealDamage()
    {
        for (int i = 0; i < _things.Count; i++)
        {
            _things[i].TakeDamage(_damage);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            _things.Add(damageable);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            _things.Remove(damageable);
        }
    }
}
