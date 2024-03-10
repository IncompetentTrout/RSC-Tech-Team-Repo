public class PlayerStateFactory // Middleman to help with managing states
{
    private PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext) {
        _context = currentContext;
    }

    public PlayerBaseState Grounded() {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Jumping() {
        return new PlayerJumpingState(_context, this);
    }

    public PlayerBaseState Falling() {
        return new PlayerFallingState(_context, this);
    }

    public PlayerBaseState Idle() {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Moving() {
        return new PlayerMovingState(_context, this);
    }

    public PlayerBaseState WallCling() {
        return new PlayerWallClingState(_context, this);
    }
}