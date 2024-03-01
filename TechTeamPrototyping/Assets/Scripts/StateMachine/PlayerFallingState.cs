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

    public override void FixedUpdateState()
    {
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() { }

    public override void InitialiseSubState()
    {
        SetSubState(_factory.Idle());
    }


    //private void HandleGravity() {
    //    _rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    //}
}
