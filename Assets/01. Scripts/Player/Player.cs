using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _rigidbody;
    private PlayerCondition _condition;
    private PlayerController _controller;
    
    private void Awake()
    {
        if (GameManager.Player == null)
        {
            GameManager.Add(this);
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _condition = GetComponent<PlayerCondition>();
        _controller = GetComponent<PlayerController>();
    }
}