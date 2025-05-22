using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _rigidbody;
    public PlayerCondition Condition { get; private set; }
    public PlayerController Controller { get; private set; }
    
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
        Condition = GetComponent<PlayerCondition>();
        Controller = GetComponent<PlayerController>();
    }
}