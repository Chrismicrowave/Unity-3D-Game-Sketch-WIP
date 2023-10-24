public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _curSubState;
    private PlayerBaseState _curSuperState;

    protected bool IsRootState { set { _isRootState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }


    public PlayerBaseState(PlayerStateMachine curContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = curContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();


    public void UpdateStates() {
        UpdateState();
        if(_curSubState != null)
        {
            _curSubState.UpdateStates();
        }
    }

    protected void SwitchState(PlayerBaseState newState) {
        ExitState();

        newState.EnterState();

        if (_isRootState)
        {
            // switch current state of context
            Ctx.CurState = newState;
        }else if(_curSuperState != null)
        {
            // set the current super states sub state to new state
            _curSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState) {
        _curSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState) {
        _curSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
