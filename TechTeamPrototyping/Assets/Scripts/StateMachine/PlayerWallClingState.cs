using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClingState : PlayerBaseState {
    public PlayerWallClingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {
    }

    public override void EnterState() {
        Debug.Log("now Wall Clinging");
        _context.WallClingDirection = _context.MoveInput / Mathf.Abs(_context.MoveInput);
        _context.WallClingForce = _context.AirborneGravity;
    }

    public override void UpdateState() {
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        HandleMovement();
    }

    public override void ExitState() {
    }

    public override void CheckSwitchingState() {
        if (_context.MoveInput == 0) {
            SwitchState(_factory.Idle());
        }
        else if (_context.WallClingDirection * _context.MoveInput < 0 || !_context.IsMoveBlocked) {
            SwitchState(_factory.Moving());
        }
        else if (_context.IsJumpPressed) {
            HandleWallJumping();
            SwitchState(_factory.Jumping());
        }
    }

    public override void InitialiseSubState() {
    }

    private void HandleWallJumping() {
        _context.Rigidbody.AddForce(
            _context.transform.right * -_context.WallClingDirection * _context.CurrentWallJumpSpeed,
            ForceMode.VelocityChange);

        //Apply wall-jump penalty
        _context.CurrentWallJumpSpeed *= _context.WallJumpPenalty;
    }

    private void HandleMovement() {
        _context.Rigidbody.AddForce(_context.transform.right * _context.MoveInput * _context.WallClingForce,
            ForceMode.Acceleration);

        _context.WallClingForce *= _context.WallJumpPenalty;

        _context.IsMoveBlocked = Mathf.Abs(_context.Rigidbody.velocity.x) == 0 ? true : false;
    }
}