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
        Debug.Log("still Grounded, update");
        CheckSwitchingState();
    }

    public override void FixedUpdateState() {
        Debug.Log("still Grounded, fixedupdate");
        CheckSwitchingState();
    }

    public override void ExitState() {}

    public override void CheckSwitchingState() {}

    public override void InitialiseSubState() {
        SetSubState(_factory.Idle());
    }


    //private void HandleGravity() {
    //    _rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    //}
}
