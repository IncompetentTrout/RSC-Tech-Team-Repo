using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {}

    public override void EnterState() {
        Debug.Log("now Idle");
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
        if (Mathf.Abs(_context.Rigidbody.velocity.x) == 0) return;
        Debug.Log("slowing");
        _context.Rigidbody.AddForce(Vector3.right * -_context.Rigidbody.velocity.normalized.x * _context.MoveAcceleration
                                    , ForceMode.Acceleration);
    }
}
