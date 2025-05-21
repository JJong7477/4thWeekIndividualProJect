using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float _moveSpeed = 3f;
    private float _jumpSpeed = 5f;
    private float _lookSpeed = 0.3f;
    private float cameraX;
    private float maxLookX = 85f;
    private float minLookX = -80f;
    private Vector3 _lookDirection;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    public GameObject Camera; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Camera = GameObject.FindGameObjectWithTag("GodEyes");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraLook();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = _moveInput.y * transform.forward + _moveInput.x * transform.right;
        moveDirection *= _moveSpeed;
        moveDirection.y = _rigidbody.velocity.y;
        _rigidbody.velocity = moveDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookDirection = context.ReadValue<Vector2>();
    }

    private void CameraLook()
    {
        cameraX += _lookDirection.y * _lookSpeed;
        cameraX = Mathf.Clamp(cameraX, minLookX, maxLookX);
        Camera.transform.localEulerAngles = new Vector3(-cameraX, 0, 0);
        transform.eulerAngles += new Vector3 (0, _lookDirection.x * _lookSpeed, 0);
    }
}
