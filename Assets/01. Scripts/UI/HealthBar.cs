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
    private readonly float _maxAmount = 1f;
    
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _currentAmount = _startAmount;
    }

    private void Update()
    {
        _image.fillAmount = _currentAmount / _maxAmount;
    }
}
