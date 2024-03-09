using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {}

    public override void EnterState() {
        Debug.Log("now Idle");
        _context.IsMoveBlocked = false;
    }

    public override void UpdateState() { 
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        HandleMovement();
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() { 
        if (_context.MoveInput != 0) {
            SwitchState(_factory.Moving());
        }
    }

    public override void InitialiseSubState() { }

    private void HandleMovement() {
        if (_context.HorizontalComponent.magnitude == 0) return;

        //Slow down horizontal movement
        _context.Rigidbody.AddForce(-_context.HorizontalComponent.normalized * _context.MoveAcceleration, ForceMode.Acceleration);
        _context.HorizontalComponent = _context.Rigidbody.velocity - _context.VerticalComponent;
        _context.HorizontalComponent -= _context.HorizontalComponent.normalized * _context.MoveAcceleration * Time.fixedDeltaTime;

        //minimize sliding
        if (_context.HorizontalComponent.magnitude <= 1f) {
            _context.Rigidbody.AddForce(-_context.HorizontalComponent, ForceMode.VelocityChange);
            _context.HorizontalComponent = Vector3.zero;
        }
    }
}
