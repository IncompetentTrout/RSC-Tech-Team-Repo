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
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() { 
        if (_context.MoveInput != 0) {
            SwitchState(_factory.Moving());
        }
    }

    public override void InitialiseSubState() { }
}
