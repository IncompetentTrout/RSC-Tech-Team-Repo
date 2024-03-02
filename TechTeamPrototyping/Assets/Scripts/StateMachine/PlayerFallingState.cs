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
        _context.Rigidbody.AddForce(Vector3.down * _context.AirborneGravity * _context.GravityModifier, ForceMode.Acceleration);
    }
}
