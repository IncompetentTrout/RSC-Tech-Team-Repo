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
    }

    public override void ExitState() { }

    public override void CheckSwitchingState() { 
        if(_context.MoveInput == 0) {
            SwitchState(_factory.Idle());
        }
    }

    public override void InitialiseSubState() { }


    //private void Movement()
    //{
    //    float moveInput = _playerInputActions.Player.Movement.ReadValue<float>();
    //    _rigidbody.AddForce(Vector3.right * moveInput * _moveAcceleration, ForceMode.Acceleration);
    //}
}