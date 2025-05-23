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
    private float _cameraX;
    private float _maxLookX = 85f;
    private float _minLookX = -80f;
    private Vector3 _lookDirection;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    public LayerMask groundLayerMask;
    public GameObject PlayerCamera { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PlayerCamera = GameObject.FindGameObjectWithTag("GodEyes");
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
        if (context.started && IsJumping()) _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookDirection = context.ReadValue<Vector2>();
    }

    private void CameraLook()
    {
        _cameraX += _lookDirection.y * _lookSpeed;
        _cameraX = Mathf.Clamp(_cameraX, _minLookX, _maxLookX);
        PlayerCamera.transform.localEulerAngles = new Vector3(-_cameraX, 0, 0);
        transform.eulerAngles += new Vector3 (0, _lookDirection.x * _lookSpeed, 0);
    }

    private bool IsJumping()
    {
        Ray[] ray = new Ray[4];
        ray[0] = new Ray(transform.position + transform.up * 0.01f + (transform.forward * 0.2f), Vector3.down);
        ray[1] = new Ray(transform.position + transform.up * 0.01f + (-transform.forward * 0.2f), Vector3.down);
        ray[2] = new Ray(transform.position + transform.up * 0.01f + (transform.right * 0.2f), Vector3.down);
        ray[3] = new Ray(transform.position + transform.up * 0.01f + (-transform.right * 0.2f), Vector3.down);

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 1.1f, groundLayerMask))
            {
                return true;
            }
        }
        
        return false;
    }
}
