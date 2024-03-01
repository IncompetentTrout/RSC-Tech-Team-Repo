using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {
        _isRootState = true;
        InitialiseSubState();
    }

    public override void EnterState() {
        Debug.Log("now Grounded");
    }

    public override void UpdateState() {
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        HandleGravity();
    }

    public override void ExitState() {}

    public override void CheckSwitchingState() {}

    public override void InitialiseSubState() {
        if (_context.MoveInput == 0) {
            SetSubState(_factory.Idle());
        } else {
            SetSubState(_factory.Moving());
        }
    }


    private void HandleGravity() {
        _context.Rigidbody.AddForce(Vector3.down * _context.GroundedGravity, ForceMode.Acceleration);
    }
}
