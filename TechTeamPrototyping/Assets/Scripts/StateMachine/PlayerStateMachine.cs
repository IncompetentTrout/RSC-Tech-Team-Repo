using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // private members
    private PlayerInputActions _playerInputActions;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private Rigidbody _rigidbody;
    private float _moveAcceleration = 5f;
    private float _gravity;

    // public accessors

    public PlayerInputActions PlayerInputActions { 
        get { return _playerInputActions; }  
        private set { _playerInputActions = value; } 
    }

    public PlayerBaseState CurrentState {  
        get { return _currentState; } 
        set { _currentState = value; } 
    }

    public PlayerStateFactory States { 
        get {return _states; } 
        private set { _states = value; } 
    }

    public Rigidbody Rigidbody { 
        get { return _rigidbody; } 
        private set { _rigidbody = value; } 
    }

    public float MoveAcceleration { 
        get { return _moveAcceleration; } 
        private set { _moveAcceleration = value; } 
    }

    public float Gravity { 
        get { return _gravity; } 
        private set { _gravity = value; } 
    }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
        //HandleGravity();
        //Movement();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
    }

    //private void HandleGravity()
    //{
    //    _rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    //}

    //private void Movement()
    //{
    //    float moveInput = _playerInputActions.Player.Movement.ReadValue<float>();
    //    _rigidbody.AddForce(Vector3.right * moveInput * _moveAcceleration, ForceMode.Acceleration);

    //}
}
