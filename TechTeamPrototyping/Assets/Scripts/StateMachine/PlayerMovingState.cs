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
        if (!_context.IsGrounded && _context.IsMoveBlocked) {
            SwitchState(_factory.WallCling());
        }
    }

    public override void InitialiseSubState() { }


    private void HandleMovement() {
        _context.HorizontalComponent = _context.Rigidbody.velocity - _context.VerticalComponent;

        //Stop accelerating at top speed
        if (_context.HorizontalComponent.magnitude >= _context.MaxMoveSpeed) return;

        //Apply moving force
        Vector3 movingForce = _context.transform.right * _context.MoveInput * _context.MoveAcceleration;
        _context.Rigidbody.AddForce(movingForce, ForceMode.Acceleration);
        _context.HorizontalComponent += movingForce * Time.fixedDeltaTime;

        //check if the player is running into a wall
        _context.IsMoveBlocked = (_context.HorizontalComponent == movingForce * Time.fixedDeltaTime && _context.MoveInput != 0) ? true: false;
    }
}