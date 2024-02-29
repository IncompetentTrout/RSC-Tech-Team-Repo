using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

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
        Debug.Log("still Grounded");
        CheckSwitchingState();
    }
    public override void ExitState() {}
    public override void CheckSwitchingState() {}
    public override void InitialiseSubState() {}
}
