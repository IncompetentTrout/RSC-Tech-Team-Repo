public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Grounded() {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Idle() {
        return new PlayerIdleState(_context, this);
    }
}

