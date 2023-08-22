using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    public PlayerInput.OnFootActions OnFoot;
    
    private PlayerMotor _motor;
    private PlayerLook _look;
    private PlayerHealth _health;
    
    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerInput();
        OnFoot = _playerInput.OnFoot;

        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();
        _health = GetComponent<PlayerHealth>();
        
        OnFoot.Jump.performed += ctx => _motor.Jump();
        OnFoot.Crouch.performed += ctx => _motor.Crouch();
        OnFoot.Sprint.performed += ctx => _motor.Sprint();
        OnFoot.TakeDamage.performed += ctx => _health.TakeDamage(Random.Range(5f, 10f));
        OnFoot.Heal.performed += ctx => _health.RestoreHealth(5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }

    private void OnDisable()
    {
        OnFoot.Disable();
    }
}
