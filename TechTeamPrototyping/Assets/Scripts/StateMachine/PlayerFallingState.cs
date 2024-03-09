using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitialiseSubState();
    }

    public override void EnterState()
    {
        Debug.Log("now Falling");
    }

    public override void UpdateState()
    {
        CheckSwitchingState();
    }

    public override void FixedUpdateState(){
        HandleGravity();
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() {
        if (_context.IsGrounded) {
            SwitchState(_factory.Grounded());
        }
        else if (_context.IsJumpPressed && _context.CanDoubleJump && !_context.IsMoveBlocked) {
            _context.CanDoubleJump = false;
            SwitchState(_factory.Jumping());
        }
    }

    public override void InitialiseSubState() {
        if (_context.MoveInput == 0) {
            SetSubState(_factory.Idle());
        }
        else {
            SetSubState(_factory.Moving());
        }
    }

    private void HandleGravity() {
        if (_context.Rigidbody.velocity.y <= -_context.MaxFallSpeed) return;

        _context.Rigidbody.AddForce(_context.GravityDirection * _context.GravityMagnitude * _context.AirborneGravity, ForceMode.Acceleration);
    }
}
