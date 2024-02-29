using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {}

    public override void EnterState() {
        Debug.Log("now Idle");
    }

    public override void UpdateState() {
        Debug.Log("still Idle");
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        Debug.Log("still idle, fixedupdate");
        CheckSwitchingState();
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() { }

    public override void InitialiseSubState() { }
}
