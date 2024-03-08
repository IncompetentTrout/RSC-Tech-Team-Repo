using UnityEngine;

//This script holds and updates the context of the players
public class PlayerStateMachine : MonoBehaviour
{
    #region Variables
    //Private members
    //  Inspector editable
    //    Jumping
    [Tooltip("Small force to keep the player grounded")]
    [SerializeField] private float _groundedGravity;

    [Tooltip("Acceleration when falling under normal gravity")]
    [SerializeField] private float _airborneGravity;

    [Tooltip("Gravity multiplier, 1 = normal")]
    [SerializeField] private float _gravityMagnitude;

    [Tooltip("Terminal velocity")]
    [SerializeField] private float _maxFallSpeed;

    [Tooltip("How high the player can jump in units")]
    [SerializeField] private float _baseJumpHeight;

    [Tooltip("Horizontal speed after a wall jump")]
    [SerializeField] private float _baseWallJumpSpeed;

    [Tooltip("Reduction in consecutive wall jump force")]
    [SerializeField] private float _wallJumpPenalty;

    //    Movement
    [Tooltip("Acceleration when manually moving")]
    [SerializeField] private float _moveAcceleration;

    [Tooltip("Top Speed")]
    [SerializeField] private float _maxMoveSpeed;

    //  Hidden
    private PlayerStateFactory _states;
    private PlayerBaseState _currentState;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Vector3 _gravityDirection;
    private float _currentWallJumpSpeed;
    private float _currentJumpHeight;
    private float _wallClingDirection;
    private float _wallClingForce;
    private float _moveInput = 0f;
    private bool _canDoubleJump;
    private bool _isJumpPressed;
    private bool _isMoveBlocked;
    private bool _isGrounded;

    // Public accessors
    public PlayerStateFactory States { get {return _states; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public Vector3 GravityDirection { get { return _gravityDirection; } set { _gravityDirection = value; } }
    public float GroundedGravity { get { return _groundedGravity; } }
    public float AirborneGravity { get { return _airborneGravity; } }
    public float GravityMagnitude { get { return _gravityMagnitude; } }
    public float MaxFallSpeed { get { return _maxFallSpeed; } }
    public float BaseJumpHeight { get { return _baseJumpHeight; } }
    public float BaseWallJumpSpeed { get { return _baseWallJumpSpeed; } }
    public float WallJumpPenalty { get { return _wallJumpPenalty; } }
    public float MoveAcceleration { get { return _moveAcceleration; } }
    public float MaxMoveSpeed { get { return _maxMoveSpeed; } }
    public float CurrentWallJumpSpeed { get { return _currentWallJumpSpeed; } set { _currentWallJumpSpeed = value; } }
    public float CurrentJumpHeight { get { return _currentJumpHeight; } set { _currentJumpHeight = value; } }
    public float WallClingDirection { get { return _wallClingDirection; } set { _wallClingDirection = value; } }
    public float WallClingForce { get { return _wallClingForce; } set { _wallClingForce = value; } }
    public float MoveInput { get { return _moveInput; } }
    public bool CanDoubleJump {  get { return _canDoubleJump; } set { _canDoubleJump = value; } }
    public bool IsJumpPressed {  get { return _isJumpPressed; } set { _isJumpPressed = value; } }
    public bool IsMoveBlocked {  get { return _isMoveBlocked; } set { _isMoveBlocked = value; } }
    public bool IsGrounded {  get { return _isGrounded; } }

    #endregion

    #region Monobehaviours

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _gravityDirection = Vector3.down;
        CheckIsGrounded();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Update()
    {
        IsJumpPressed = Input.GetKeyDown(KeyCode.Space);
        _moveInput = Input.GetAxisRaw("Horizontal");
        CheckIsGrounded();
        CurrentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
    }

    //private void OnDrawGizmos()
    //{
    //    const float OFFSET = 0.01f;
    //    float radius = _collider.bounds.extents.x - OFFSET;
    //    float maxDistance = (_collider.bounds.extents.y / 2) + (OFFSET * 10);
    //    _isGrounded = Physics.SphereCast(_collider.bounds.center, radius, -transform.up, out RaycastHit hitInfo, maxDistance);

    //    if (_isGrounded)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawRay(_collider.bounds.center, -transform.up * hitInfo.distance);
    //        Gizmos.DrawWireSphere(_collider.bounds.center + -transform.up * hitInfo.distance, radius);
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawRay(_collider.bounds.center, -transform.up * maxDistance);
    //        Gizmos.DrawWireSphere(_collider.bounds.center + -transform.up * maxDistance, radius);
    //    }
    //}
    #endregion

    public void CheckIsGrounded() {
        const float OFFSET = 0.01f;
        float radius = _collider.bounds.extents.x - OFFSET;
        float maxDistance = (_collider.bounds.extents.y / 2) + (OFFSET * 10);
        _isGrounded = Physics.SphereCast(_collider.bounds.center, radius, -transform.up, out RaycastHit hitInfo, maxDistance);
    }

    public void SetRotation(Quaternion rotation) {
        transform.rotation = rotation;
    }
}
