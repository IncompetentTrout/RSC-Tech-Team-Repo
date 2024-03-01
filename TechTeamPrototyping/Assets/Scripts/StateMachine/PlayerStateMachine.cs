using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //
    // Private members
    //
    [SerializeField] private float _groundedGravity;
    [SerializeField] private float _moveAcceleration;

    private PlayerInputActions _playerInputActions;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private Rigidbody _rigidbody;
    private float _moveInput = 0f;

    //
    // Public accessors
    //
    public PlayerInputActions PlayerInputActions { 
        get { return _playerInputActions; }  
    }

    public PlayerBaseState CurrentState {  
        get { return _currentState; } 
        set { _currentState = value; } 
    }

    public PlayerStateFactory States { 
        get {return _states; }
    }

    public Rigidbody Rigidbody { 
        get { return _rigidbody; } 
    }

    public float GroundedGravity { 
        get { return _groundedGravity; } 
    }

    public float MoveAcceleration { 
        get { return _moveAcceleration; } 
    }

    public float MoveInput {
        get { return _moveInput; }
    }

    #region Monobehaviours

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Update()
    {
        _moveInput = _playerInputActions.Player.Movement.ReadValue<float>();
        CurrentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }
    #endregion
}
