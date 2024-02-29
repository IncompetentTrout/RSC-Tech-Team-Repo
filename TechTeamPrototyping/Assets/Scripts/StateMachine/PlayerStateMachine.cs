using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private float gravity;

    private PlayerInputActions _playerInputActions;
    private Rigidbody _rigidbody;
    private float _moveAcceleration = 5f;


    private PlayerBaseState _currentState;
    public PlayerBaseState CurrentState {  get { return _currentState; } set { _currentState = value; } }

    private PlayerStateFactory _states;
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
        CurrentState.UpdateStates();
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
