using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public HealthBar healthBar;
    private void Start()
    {
        GameManager.Add(this);
        healthBar = GetComponentInChildren<HealthBar>();
    }
}