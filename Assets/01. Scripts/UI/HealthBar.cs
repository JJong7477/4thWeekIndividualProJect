using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _currentAmount;
    private float _startAmount = 1f;
    private float _maxAmount = 1f;
    
    private Image _health;

    private void Start()
    {
        _health = this.transform.Find("Health").GetComponent<Image>();
        _currentAmount = _startAmount;
    }

    private void Update()
    {
        _health.fillAmount = _currentAmount / _maxAmount;
    }
    
    public void Add(float value)
    {
        _currentAmount = Mathf.Min(_currentAmount + value, _maxAmount);
    }

    public void Subtract(float value)
    {
        _currentAmount = Mathf.Max(_currentAmount - value, 0);
    }
}
