using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //Private members
    //  Inspector editable
    //    Jumping
    [SerializeField] private float _groundedGravity;
    [SerializeField] private float _airborneGravity;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private float _jumpHeight;

    //    Movement
    [SerializeField] private float _moveAcceleration;
    [SerializeField] private float _maxMoveSpeed;

    //  Hidden
    private PlayerInputActions _playerInputActions;
    private PlayerStateFactory _states;
    private PlayerBaseState _currentState;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private float _moveInput = 0f;
    private bool _isJumpPressed;
    private bool _isGrounded;

    // Public accessors
    public PlayerInputActions PlayerInputActions { get { return _playerInputActions; } }
    public PlayerStateFactory States { get {return _states; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public float GroundedGravity { get { return _groundedGravity; } }
    public float AirborneGravity { get { return _airborneGravity; } }
    public float GravityModifier { get { return _gravityModifier; } }
    public float JumpHeight { get { return _jumpHeight; } }
    public float MoveAcceleration { get { return _moveAcceleration; } }
    public float MaxMoveSpeed { get { return _maxMoveSpeed; } }
    public float MoveInput { get { return _moveInput; } }
    public bool IsJumpPressed {  get { return _isJumpPressed; } set { _isJumpPressed = value; } }
    public bool IsGrounded {  get { return _isGrounded; } }

    #region Monobehaviours

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        CheckIsGrounded();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Jump.performed += (Action) => { _isJumpPressed = true; }; 
        _playerInputActions.Player.Jump.canceled += (Action) => { _isJumpPressed = false; }; 

    }

    private void Update()
    {
        _moveInput = _playerInputActions.Player.Movement.ReadValue<float>();
        CurrentState.UpdateStates();
        CheckIsGrounded();
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

    //private void OnDrawGizmos()
    //{
    //    const float OFFSET = 0.01f;
    //    float radius = _collider.bounds.extents.x - OFFSET;
    //    float maxDistance = (_collider.bounds.extents.y / 2) + (OFFSET * 10);
    //    _isGrounded = Physics.SphereCast(_collider.bounds.center, radius, Vector3.down, out RaycastHit hitInfo, maxDistance);

    //    if (_isGrounded)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawRay(_collider.bounds.center, Vector3.down * hitInfo.distance);
    //        Gizmos.DrawWireSphere(_collider.bounds.center + Vector3.down * hitInfo.distance, radius);
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawRay(_collider.bounds.center, Vector3.down * maxDistance);
    //        Gizmos.DrawWireSphere(_collider.bounds.center + Vector3.down * maxDistance, radius);
    //    }
    //}
    #endregion



    public void CheckIsGrounded() {
        const float OFFSET = 0.01f;
        float radius = _collider.bounds.extents.x - OFFSET;
        float maxDistance = (_collider.bounds.extents.y / 2) + (OFFSET * 10);
        _isGrounded = Physics.SphereCast(_collider.bounds.center, radius, Vector3.down, out RaycastHit hitInfo, maxDistance);
    }
}
