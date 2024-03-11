using UnityEngine;

public class PlayerMovingState : PlayerBaseState {
	public PlayerMovingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {
	}

	public override void EnterState() {
	}

	public override void UpdateState() {
		CheckSwitchingState();
	}

	public override void FixedUpdateState() {
		HandleMovement();
	}

	public override void ExitState() {
	}

	public override void CheckSwitchingState() {
		if (_context.MoveInput == 0) SwitchState(_factory.Idle());
		if (!_context.IsGrounded && _context.IsMoveBlocked) SwitchState(_factory.WallCling());
	}

	public override void InitialiseSubState() {
	}


	private void HandleMovement() {
		//Stop accelerating at top speed
		if (Mathf.Abs(_context.Rigidbody.velocity.x) >= _context.MaxMoveSpeed) return;

		//Apply moving force
		_context.Rigidbody.AddForce(_context.transform.right * _context.MoveInput * _context.MoveAcceleration,
			ForceMode.Acceleration);

		//check if the player is running into a wall
		_context.IsMoveBlocked = Mathf.Abs(_context.Rigidbody.velocity.x) == 0 ? true : false;
	}
}