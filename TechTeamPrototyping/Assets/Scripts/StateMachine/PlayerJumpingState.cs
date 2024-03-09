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
        HandleJumping(); //Apply jump force
        
        if (_context.IsGrounded || !_context.IsMoveBlocked) return;

        //Apply wall jump penalty
        _context.CurrentJumpHeight = (_context.CurrentJumpHeight - _context.WallJumpPenalty > 0) ? 
            _context.CurrentJumpHeight - _context.WallJumpPenalty : 0;
    }

    public override void UpdateState() {
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        HandleGravity();
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() {
        if (_context.VerticalComponent.magnitude == 0) {
            SwitchState(_factory.Falling());
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

    private void HandleJumping() {
        _context.IsJumpPressed = false;
        
        //calculate the force needed to reach jump height under normal gravity
        float jumpForce = Mathf.Sqrt(2 * _context.AirborneGravity * _context.CurrentJumpHeight);
        jumpForce = (!_context.CanDoubleJump) ? jumpForce * 1.3f : jumpForce;
        _context.VerticalComponent = -_context.GravityDirection * jumpForce;
        _context.Rigidbody.AddForce(-_context.GravityDirection * jumpForce, ForceMode.VelocityChange);
    }

    private void HandleGravity() {
        _context.VerticalComponent += _context.GravityDirection * _context.GravityMagnitude * _context.AirborneGravity * Time.fixedDeltaTime;
        _context.Rigidbody.AddForce(_context.GravityDirection * _context.GravityMagnitude * _context.AirborneGravity, ForceMode.Acceleration);

        if (_context.VerticalComponent.magnitude > .5f) {
            _context.VerticalComponent = Vector3.zero;
        }
    }
}