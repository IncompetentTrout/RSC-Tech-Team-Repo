public abstract class PlayerBaseState
{
    protected PlayerStateMachine _context;
    protected PlayerStateFactory _factory;
    protected PlayerBaseState _currentSuperState;
    protected PlayerBaseState _currentSubState;
    protected bool _isRootState = false;

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) {
        _context = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchingState();
    public abstract void InitialiseSubState();

    public void UpdateStates() {
        UpdateState();
        if (_currentSubState != null) {
            _currentSubState.UpdateStates();
        }
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.FixedUpdateStates();
        }
    }

    protected void SwitchState(PlayerBaseState newState) {
        
        ExitState();

        newState.EnterState();

        if (_isRootState) {
            _context.CurrentState = newState;
        } else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState) {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState) {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
