using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {
        _isRootState = true;
        InitialiseSubState();
    }

    public override void EnterState() {
        Debug.Log("now Grounded");
        _context.VerticalComponent = Vector3.zero;
        ResetJumps();
    }

    public override void UpdateState() {
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        HandleGravity();
    }

    public override void ExitState() {}

    public override void CheckSwitchingState() {
        if (_context.IsGrounded && _context.IsJumpPressed) {
            SwitchState(_factory.Jumping());
        }
        else if (!_context.IsGrounded && !_context.IsJumpPressed) {
            SwitchState(_factory.Falling());
        }


    }

    public override void InitialiseSubState() {
        if (_context.MoveInput == 0) {
            SetSubState(_factory.Idle());
        } else {
            SetSubState(_factory.Moving());
        }
    }

    private void HandleGravity() {
        _context.Rigidbody.AddForce(_context.GravityDirection * _context.GravityMagnitude * _context.GroundedGravity, ForceMode.Acceleration);
    }

    private void ResetJumps() {
        _context.CurrentJumpHeight = _context.BaseJumpHeight;
        _context.CurrentWallJumpSpeed = _context.BaseWallJumpSpeed;
        _context.CanDoubleJump = true;
    }
}
