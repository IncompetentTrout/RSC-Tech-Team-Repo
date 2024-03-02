using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public PlayerMovingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
        Debug.Log("now Moving");
    }

    public override void UpdateState() {
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        HandleMovement();
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() { 
        if(_context.MoveInput == 0) {
            SwitchState(_factory.Idle());
        }
    }

    public override void InitialiseSubState() { }


    private void HandleMovement() {
        if (Mathf.Abs(_context.Rigidbody.velocity.x) >= _context.MaxMoveSpeed) return;

        _context.Rigidbody.AddForce(Vector3.right * _context.MoveInput * _context.MoveAcceleration, ForceMode.Acceleration);
    }
}