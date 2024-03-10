using UnityEngine;

//This script holds and updates the context of the players
public class PlayerStateMachine : MonoBehaviour {
    public void CheckIsGrounded() {
        const float OFFSET = 0.01f;
        var radius = _collider.bounds.extents.x - OFFSET;
        var maxDistance = _collider.bounds.extents.y / 2 + OFFSET * 10;
        IsGrounded = Physics.SphereCast(_collider.bounds.center, radius, -transform.up, out var hitInfo, maxDistance);
    }

    public void SetRotation(Quaternion rotation) {
        transform.rotation = rotation;
    }

    #region Variables

    //Private members
    //  Inspector editable
    //    Jumping
    [Tooltip("Small force to keep the player grounded")] [SerializeField]
    private float _groundedGravity;

    [Tooltip("Acceleration when falling under normal gravity")] [SerializeField]
    private float _airborneGravity;

    [Tooltip("Gravity multiplier, 1 = normal")] [SerializeField]
    private float _gravityMagnitude;

    [Tooltip("Terminal velocity")] [SerializeField]
    private float _maxFallSpeed;

    [Tooltip("How high the player can jump in units")] [SerializeField]
    private float _baseJumpHeight;

    [Tooltip("Horizontal speed after a wall jump")] [SerializeField]
    private float _baseWallJumpSpeed;

    [Tooltip("Reduction in consecutive wall jump force")] [SerializeField]
    private float _wallJumpPenalty;

    //    Movement
    [Tooltip("Acceleration when manually moving")] [SerializeField]
    private float _moveAcceleration;

    [Tooltip("Top Speed")] [SerializeField]
    private float _maxMoveSpeed;

    //  Hidden
    private Collider _collider;

    // Public accessors
    public PlayerStateFactory States { get; private set; }

    public PlayerBaseState CurrentState { get; set; }

    public Rigidbody Rigidbody { get; private set; }

    public Vector3 GravityDirection { get; set; }

    public float GroundedGravity => _groundedGravity;
    public float AirborneGravity => _airborneGravity;
    public float GravityMagnitude => _gravityMagnitude;
    public float MaxFallSpeed => _maxFallSpeed;
    public float BaseJumpHeight => _baseJumpHeight;
    public float BaseWallJumpSpeed => _baseWallJumpSpeed;
    public float WallJumpPenalty => _wallJumpPenalty;
    public float MoveAcceleration => _moveAcceleration;
    public float MaxMoveSpeed => _maxMoveSpeed;

    public float CurrentWallJumpSpeed { get; set; }

    public float CurrentJumpHeight { get; set; }

    public float WallClingDirection { get; set; }

    public float WallClingForce { get; set; }

    public float MoveInput { get; private set; }

    public bool CanDoubleJump { get; set; }

    public bool IsJumpPressed { get; set; }

    public bool IsMoveBlocked { get; set; }

    public bool IsGrounded { get; private set; }

    #endregion

    #region Monobehaviours

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        GravityDirection = Vector3.down;
        CheckIsGrounded();

        States = new PlayerStateFactory(this);
        CurrentState = States.Grounded();
        CurrentState.EnterState();
    }

    private void Update() {
        IsJumpPressed = Input.GetKeyDown(KeyCode.Space);
        MoveInput = Input.GetAxisRaw("Horizontal");
        CheckIsGrounded();
        CurrentState.UpdateStates();
    }

    private void FixedUpdate() {
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
}