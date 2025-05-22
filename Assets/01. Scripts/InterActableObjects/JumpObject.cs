using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    private float jumpForce = 14f;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
