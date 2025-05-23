using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _currentAmount;
    private float _maxAmount;
    
    private Image _health;

    private void Start()
    {
        _maxAmount = GameManager.Player.Condition.MaxHealth;
        _health = this.transform.Find("Health").GetComponent<Image>();
    }

    private void Update()
    {
        _currentAmount = GameManager.Player.Condition.CurrentHealth;
        _health.fillAmount = _currentAmount / _maxAmount;
    }
}
