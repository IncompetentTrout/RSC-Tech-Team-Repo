using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    public PlayerJumpingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitialiseSubState();
    }

    public override void EnterState() {
        Debug.Log("now Jumping");
        HandleJumping();
    }

    public override void UpdateState() {
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        HandleGravity();
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() {
        if (_context.Rigidbody.velocity.y < 0) {
            SwitchState(_factory.Falling());
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

    private void HandleJumping() {
        float jumpForce = _context.JumpHeight;
        _context.Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private void HandleGravity() {
        _context.Rigidbody.AddForce(Vector3.down * _context.AirborneGravity, ForceMode.Acceleration);
    }
}