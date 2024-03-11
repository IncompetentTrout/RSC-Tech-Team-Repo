public abstract class PlayerBaseState {
	protected PlayerStateMachine _context;
	protected PlayerBaseState _currentSubState;
	protected PlayerBaseState _currentSuperState;
	protected PlayerStateFactory _factory;
	protected bool _isRootState = false;

	public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) {
		_context = currentContext;
		_factory = playerStateFactory;
	}

	//What to do when entering a new state
	public abstract void EnterState();

	//What to do in the Update method while in this state
	public abstract void UpdateState();

	//What to do in the FixedUpdate method while in this state
	public abstract void FixedUpdateState();

	//What to do when exiting this state
	public abstract void ExitState();

	//Checking the conditions to change state
	public abstract void CheckSwitchingState();

	//For root states to initialise with the correct sub-state
	public abstract void InitialiseSubState();

	//Update all active states
	public void UpdateStates() {
		UpdateState();
		if (_currentSubState != null) _currentSubState.UpdateStates();
	}

	//Fixed Update for all active states
	public void FixedUpdateStates() {
		FixedUpdateState();
		if (_currentSubState != null) _currentSubState.FixedUpdateStates();
	}

	protected void SwitchState(PlayerBaseState newState) {
		//Exit the current state
		ExitState();

		//Enter the new State
		newState.EnterState();

		if (_isRootState)
			_context.CurrentState = newState;
		else if (_currentSuperState != null) _currentSuperState.SetSubState(newState);
	}

	protected void SetSuperState(PlayerBaseState newSuperState) {
		_currentSuperState = newSuperState;
	}

	protected void SetSubState(PlayerBaseState newSubState) {
		_currentSubState = newSubState;
		newSubState.SetSuperState(this);
	}
}