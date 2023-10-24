public class PlayerStateFactory
{
    PlayerStateMachine _ctx;

    public PlayerStateFactory(PlayerStateMachine curContext)
    {
        _ctx = curContext;
    }

    public PlayerBaseState Idle() {
        return new PlayerIdleState(_ctx, this);
    }

    public PlayerBaseState Run()
    {
        return new PlayerRunState(_ctx, this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_ctx, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_ctx, this);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_ctx, this);
    }

    public PlayerBaseState Fall()
    {
        return new PlayerFallState(_ctx, this);
    }


}
