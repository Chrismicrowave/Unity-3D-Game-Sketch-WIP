using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine curContext,
        PlayerStateFactory playerStateFactory)
        : base(curContext, playerStateFactory)
    {

    }



    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsRunHash, true);

    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.IsRunHash, false);
    }

    public override void CheckSwitchStates() {
        if (!Ctx.IsMovePressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovePressed && Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void InitializeSubState() { }
}
